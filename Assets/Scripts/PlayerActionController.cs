using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    public enum State
    {
        Normal,
        Dashing,
        Attacking
    }

    public Player player;
    public Rigidbody2D rb2D;
    public PlayerAnimationController animationController;
    public LayerMask dashLayerMask;
    public Vector3 moveDirection;
    public Vector3 dashDirection;
    public Vector3 lastMoveDirection;
    public float dashSpeed;
    private float xAxis;
    private float yAxis;
    public Stats stats;
    public bool isFacingRight;
    public bool isFacingLeft;
    public bool isDashButtonDown;
    public float dashCooldown;
    public float dashTimeLength;
    public bool dashAvailable;
    public bool isAttackPressed;
    public float attackTimeLength;
    public float attackCooldown;
    public bool attackAvailable;
    public bool isAttacking;
    public State state;
    public double playerDirection;
    private void Start()
    {
        player = GameManager.instance.player;
        rb2D=GameManager.instance.player.GetComponent<Rigidbody2D>();
        stats = GameManager.instance.playerStats;
        animationController = GameManager.instance.player.animationController;
        dashAvailable = true;
        attackAvailable = true;
        state = State.Normal;
        playerDirection = GameManager.instance.player.GetPlayerDirection() ;
        isFacingRight = true;
    }
    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;
        stats = GameManager.instance.playerStats;
        playerDirection = GameManager.instance.player.GetPlayerDirection();
        //Cooldown checker//
        if (Time.time > dashCooldown && state==State.Dashing) { dashAvailable = true; }
        if(Time.time > attackCooldown && state ==State.Attacking) {attackAvailable = true; state = State.Normal;}
        /////////////////////

        stats = GameManager.instance.playerStats;
        switch (state)
        {
            case State.Normal:

                if (Input.GetKey(KeyCode.W))
                {
                    moveY = 1f;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    moveY = -1f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    if (isFacingRight) { animationController.FlipX();}
                    isFacingLeft = true;
                    isFacingRight = false;
                    moveX = -1f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    if (isFacingLeft) { animationController.FlipX(); }
                    isFacingLeft = false;
                    isFacingRight = true;
                    moveX = 1f;
                }

             moveDirection = new Vector3(moveX, moveY).normalized;//normalized makes it so diagnals doesnt moves absuardly fast
                
                if (moveX != 0 || moveY != 0)
                {
                    lastMoveDirection = moveDirection;
                    animationController.ChangeAnimationState(animationController.PLAYER_WALK);


                }
                if(moveX == 0 && moveY == 0)
                {
                    animationController.ChangeAnimationState(animationController.PLAYER_IDLE);
                }
                if (Input.GetKeyDown(KeyCode.Mouse0) && attackAvailable)//Basic Attack
                {
                    //Debug.Log("Start the attack animation");
                    isAttackPressed = true;
                    attackAvailable = false;
                    attackTimeLength = Time.time + stats.attackSpeed;
                    attackCooldown = Time.time + stats.attackSpeed;
                    state = State.Attacking;
                    animationController.ChangeAnimationState(animationController.PLAYER_ATTACK);
                }

                if (Input.GetKeyDown(KeyCode.Space) && dashAvailable)
                {
                    isDashButtonDown = true;
                    dashAvailable = false;
                    dashDirection = lastMoveDirection; 
                    dashSpeed = 4f;
                    dashTimeLength = Time.time + stats.dashTimeLength;
                    dashCooldown = Time.time + stats.dashCooldown;
                    state = State.Dashing;
                    Debug.Log("Dashing Started");
                }
                break;

            case State.Dashing:
                
                    animationController.ChangeAnimationState(animationController.PLAYER_DASH);
                if (dashTimeLength < Time.time)//this is how long the dash lasts
                {
                    Debug.Log("Dashing Ended");
                    state = State.Normal;
                }
                break;//this means ignore the input when we are dashing

            case State.Attacking:
                moveY = 0f;
                moveX = 0f;
                if (Input.GetKey(KeyCode.W))
                {
                    moveY = 1f;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    moveY = -1f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    if (isFacingRight) { animationController.FlipX(); }
                    isFacingLeft = true;
                    isFacingRight = false;
                    moveX = -1f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    if (isFacingLeft) { animationController.FlipX(); }
                    isFacingLeft = false;
                    isFacingRight = true;
                    moveX = 1f;
                }

                moveDirection = new Vector3(moveX, moveY).normalized;//normalized makes it so diagnals doesnt moves absuardly fast

                if (moveX != 0 || moveY != 0)
                {
                    lastMoveDirection = moveDirection;
                }
                //if (Input.GetKeyDown(KeyCode.Mouse0) && attackAvailable)//Basic Attack
                //{
                //    Debug.Log("Start the attack animation");
                //    isAttackPressed = true;
                //    attackAvailable = false;
                //    attackTimeLength = Time.time + stats.attackSpeed;
                //    attackCooldown = Time.time + stats.attackSpeed;
                //    state = State.Attacking;
                //    animationController.ChangeAnimationState(animationController.PLAYER_ATTACK);
                //}

                if (Input.GetKeyDown(KeyCode.Space) && dashAvailable)
                {
                    isDashButtonDown = true;
                    dashAvailable = false;

                    if (moveX == 0 && moveY == 0)
                    {
                        dashDirection = lastMoveDirection;
                    }
                    else
                    {
                        dashDirection = moveDirection;
                    }
                    dashSpeed = 4f;
                    dashTimeLength = Time.time + stats.dashTimeLength;
                    dashCooldown = Time.time + stats.dashCooldown;
                    state = State.Dashing;
                }
                break;

        }
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                rb2D.velocity = moveDirection * stats.speed;

                //if (isDashButtonDown)
                //{
                //    float dashAmount = .3f * stats.speed;
                //    Vector3 dashPosition = transform.position + moveDirection * dashAmount;

                //    RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, moveDirection, dashAmount, dashLayerMask);
                //    if (raycastHit2d.collider != null)
                //    {
                //        dashPosition = raycastHit2d.point;

                //    }

                //    rb2D.MovePosition(transform.position + moveDirection * dashAmount);
                //    isDashButtonDown = false;
                //}
                break;

            case State.Dashing:
                //Debug.Log("Fixed Update Dashing now");
                rb2D.velocity = dashDirection * dashSpeed;
                break;

            case State.Attacking:
                rb2D.velocity = moveDirection * stats.speed;
                break;
        }
    }
}
