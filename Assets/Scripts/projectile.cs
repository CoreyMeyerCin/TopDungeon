using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 2f;
    public Rigidbody2D rb;
    public Player player;
    public float lifespan;
    public float spawnTime;

    private void Start()
    {
        player = GetComponent<Player>();
        SetProjectileDirection();
        spawnTime = Time.time;
    }

    public void Update()
    {
        TimeOutCheck();
    }
    private void TimeOutCheck()
    {
        if(Time.time - spawnTime > lifespan)
        {
            Destroy(this.gameObject);
        }
    }
    private void SetProjectileDirection()
    {

        if (GameManager.instance.player.playerDirection == 0)
        {
            rb.velocity = transform.right * speed;
            transform.rotation = Quaternion.Euler(0,0,-90);
        }
        else if (GameManager.instance.player.playerDirection == 0.5)
        {
            rb.velocity = transform.right * speed + -transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, -135);
        }
        else if (GameManager.instance.player.playerDirection == 1)
        {
            rb.velocity = -transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, -180);
        }
        else if (GameManager.instance.player.playerDirection == 1.5)
        {
            rb.velocity = -transform.right * speed + -transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, -225);
        }
        else if (GameManager.instance.player.playerDirection == 2)
        {
            rb.velocity = -transform.right * speed;
            transform.rotation = Quaternion.Euler(0, 0, -270);
        }
        else if (GameManager.instance.player.playerDirection == 2.5)
        {
            rb.velocity = -transform.right * speed + transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, -315);
        }
        else if (GameManager.instance.player.playerDirection == 3)
        {
            rb.velocity = transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (GameManager.instance.player.playerDirection == 3.5)
        {
            rb.velocity = transform.right * speed + transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
    }
}
