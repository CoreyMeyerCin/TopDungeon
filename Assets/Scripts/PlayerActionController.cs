using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    public enum State
    {
        Normal,
        Dashing,
    }

    public Player player;
    public Rigidbody2D rb2D;
    public LayerMask dashLayerMask;
    private Vector3 moveDirection;
    private Vector3 dashDirection;
    private float dashSpeed;
    private float xAxis;
    private float yAxis;
    public Stats stats;
    public bool isDashButtonDown;
    public float dashCooldownTimer;
    public bool dashAvailable;
    public bool isAttackPressed;
    public State state;
    private void Start()
    {
        player = GameManager.instance.player;
        rb2D=GameManager.instance.player.GetComponent<Rigidbody2D>();
        stats = GameManager.instance.playerStats;
        dashAvailable = true;
        state = State.Normal;
    }
    private void Update()
    {
        stats = GameManager.instance.playerStats;
        switch (state)
        {
            case State.Normal:

                stats = GameManager.instance.playerStats;
                float moveX = 0f;
                float moveY = 0f;
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
                    moveX = -1f;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    moveX = 1f;
                }
                if (Input.GetKey(KeyCode.LeftAlt))
                {

                }
                if (Input.GetKeyDown(KeyCode.RightControl))//Basic Attack
                {
                    isAttackPressed = true;

                }

             moveDirection = new Vector3(moveX, moveY).normalized;//normalized makes it so diagnals doesnt moves absuardly fast

                if (Input.GetKeyDown(KeyCode.Space) && dashAvailable)
                {
                    
                    isDashButtonDown = true;
                    dashAvailable = false;
                    dashDirection = moveDirection;
                    dashSpeed = 4f;
                    dashCooldownTimer = Time.time + stats.dashCooldown;
                    state = State.Dashing;
                    Debug.Log($"DashAvaiable, now dashing\nDashCooldownTimer:{dashCooldownTimer}Time:{Time.time}player.stats.dashCooldown:{stats.dashCooldown}");
                }
                break;

            case State.Dashing:
                if (dashCooldownTimer<Time.time)
                {
                    Debug.Log("Dash is now avaiable again");
                    dashAvailable = true;
                    state = State.Normal;
                }
                break;//this means ignore the input when we are dashing

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
                Debug.Log("Fixed Update Dashing now");
                rb2D.velocity = dashDirection * dashSpeed;
                break;
        }
    }
}
