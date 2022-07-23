using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    public Player player;
    public Rigidbody2D rb2D;
    private Vector3 moveDirection;
    public Stats stats;
    
    private void Start()
    {
        player = GameManager.instance.player;
        rb2D=GameManager.instance.player.GetComponent<Rigidbody2D>();
        stats = GameManager.instance.playerStats;
    }
    private void Update()
    {
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
        //moveY *= player.stats.speed;
        //moveX *= player.stats.speed;
        moveDirection = new Vector3(moveX, moveY).normalized;//normalized makes it so diagnals doesnt moves absuardly fast
    }
    private void FixedUpdate()
    {
        rb2D.velocity = moveDirection *stats.speed;
    }
}
