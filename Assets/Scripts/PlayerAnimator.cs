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

    private void Awake()
    {
        if(!TryGetComponent(out IPlayerController player))
        {
            Destroy(this);
            return;
        }
        _player = player;
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        _player.isIdle = true;
    }
    private void Update()
    {
        var state = GetState();
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
