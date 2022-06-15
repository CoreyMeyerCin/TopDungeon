using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 2f;
    public Player player;
    public float lifespan;
    public float spawnTime;
    public Transform pT;
    public Vector3 moveDelta;

    private void Start()
    {
        player = GetComponent<Player>();
        pT = GameManager.instance.player.firePoint;
        SetProjectileDirection();
        spawnTime = Time.time;
    }

    public void Update()
    {
        TimeOutCheck();
       // UpdateProjectilePosition();
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
        moveDelta = new Vector3();

        if (GameManager.instance.player.playerDirection == 0)
        {

            transform.rotation = Quaternion.Euler(0, 0, -90);
            transform.position += transform.forward * speed;//transform.right *speed*Time.deltaTime;
        }
        else if (GameManager.instance.player.playerDirection == 0.5)
        {
            pT.position = transform.right * speed + -transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, -135);
        }
        else if (GameManager.instance.player.playerDirection == 1)
        {
            pT.position = -transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, -180);
        }
        else if (GameManager.instance.player.playerDirection == 1.5)
        {
            pT.position = -transform.right * speed + -transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, -225);
        }
        else if (GameManager.instance.player.playerDirection == 2)
        {
            pT.position = -transform.right * speed;
            transform.rotation = Quaternion.Euler(0, 0, -270);
        }
        else if (GameManager.instance.player.playerDirection == 2.5)
        {
            pT.position = -transform.right * speed + transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, -315);
        }
        else if (GameManager.instance.player.playerDirection == 3)
        {
            pT.position = transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (GameManager.instance.player.playerDirection == 3.5)
        {
            pT.position = transform.right * speed + transform.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
    }
}
