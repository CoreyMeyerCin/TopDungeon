using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class PlayerAnimator : MonoBehaviour
{
    [SerializedField] private float _attackSpeed = 0.2f; // TODO: this needs to be able to be changed with Player.Stats.attackSpeed
    [SerializedField] private float _recoverySpeed = 0.2f; // this will also need to be changed from there


    private IPlayerController _player;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private float _lockedTill;
    private bool _idle;
    private bool _walking;
    private bool _attackingSword;
    private bool _throwingDagger;
    private bool _damaged;
    private bool _running;

    private void Start()
    {
        _player.isIdle = true;
        SetWeaponAnimationTree();
        GetAnimation();
    }
    private void Update()
    {
        var state = GetState();
    }
    public Enum GetWeaponFromPlayer
    {
        get
        {
            return Player.instance.transform.GetChild(0).GetComponent<Weapon>().animHoldType;
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void GetAnimation()
    {
        Player.instance.animator.SetInteger("SkinInt", Player.instance.skinId);
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
        //Debug.Log($"Hitting animation A {Player.instanceStats.speed}");
            Player.instance.animator.SetFloat("Speed", Mathf.Abs(Player.instance.stats.speed));
        }
        else if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
                && !Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.W))
        {
            Player.instance.animator.SetFloat("Speed", 0);
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Player.instance.animator.SetBool("isDashing", true);
        }
    }
    
   
    public static void SetWeaponAnimationTree()
    {
        foreach (AnimatorControllerParameter parameter in Player.instance.animator.parameters)
        {
            Player.instance.animator.SetBool(parameter.name, false);
        }
        var currentWeapon = Player.instance.weapon.GetWeaponType();
        switch (currentWeapon)
        {
            case WeaponAnimTypeHold.IsSword:
                {
                    Player.instance.animator.SetBool("IsSword", true);
                    break;
                }
            case WeaponAnimTypeHold.IsDagger:
                {
                    Player.instance.animator.SetBool("IsDagger", true);
                    break;
                }
            case WeaponAnimTypeHold.IsBow:
                {
                    Player.instance.animator.SetBool("IsBow", true);
                    break;
                }
            case WeaponAnimTypeHold.IsStaff:
                {
                    Player.instance.animator.SetBool("IsStaff", true);
                    break;
                }
            case WeaponAnimTypeHold.IsFist:
                {
                    Player.instance.animator.SetBool("IsFist", true);
                    break;
                }
            case WeaponAnimTypeHold.IsMagic:
                {
                    Player.instance.animator.SetBool("IsMagic", true);
                    break;
                }

            default:
                break;
        }
    }


    public static void SetAttackAnimation()
    {
        SetWeaponAnimationTree();
        var currentWeapon = Player.instance.weapon.GetWeaponType();
        switch (currentWeapon)
        {
            case WeaponAnimTypeHold.IsSword:
                {
                    Player.instance.animator.SetTrigger("SwingSword");
                    break;
                }
            case WeaponAnimTypeHold.IsDagger:
                {
                    Player.instance.animator.SetTrigger("ThrowDagger");
                    break;
                }
            case WeaponAnimTypeHold.IsBow:
                {
                    Player.instance.animator.SetTrigger("ShootBow");
                    break;
                }
            case WeaponAnimTypeHold.IsStaff:
                {
                    break;
                }
            case WeaponAnimTypeHold.IsFist:
                {
                    break;
                }
            case WeaponAnimTypeHold.IsMagic:
                {
                    break;
                }

            default:
                break;
        }
    }

    private int GetState()
    {
        if(Time.time < _lockedTill) //make sure current animation finishes before moving onto the next
        {
            return _currentState;
        }
        //Priorities
        if(_attackingSword)return LockState(AttackingSword, _attackSpeed); // this needs to get faster with attack speed
        if(_throwingDagger) return LockState(ThrowingDagger, _attackSpeed);
        if(_damaged) return LockState(Damaged, _recoverySpeed);
        if (_running) return Running;
        if (_walking) return Walking;
        else return Idle;


        int LockState(int s, float t) //how long the attack is from animation(I think, I stole some of this code and reqrote most of it to make sense IT WAS WRITTEN LIKE AN ASSHOLE AT FIRST)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    private int _currentState;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walking = Animator.StringToHash("Walk");
    private static readonly int AttackingSword = Animator.StringToHash("AttackSword");
    private static readonly int ThrowingDagger = Animator.StringToHash("ThrowingDagger");
    private static readonly int Damaged = Animator.StringToHash("Damaged");
    private static readonly int Running = Animator.StringToHash("Running");

    public interface IPlayerController
    {
        public Vector2 Input { get; }
        public Vector2 Speed { get; }
        public bool isIdle { get; set; }
        public bool isWalking { get; set; }
        public bool isAttackingSword { get; set; }
        public bool isThrowingDagger { get; set; }
        public bool isDamaged { get; set; }
        public bool isRunning { get; set; }
    }
}
