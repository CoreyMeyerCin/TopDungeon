using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Collidable
{

    //public int[] damage = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; //amount of damage each weapon with upgrade does
    //public float[] knockbackDistance = { 2.0f, 2.2f, 2.4f, 2.6f, 2.8f, 3.0f, 3.2f, 3.4f, 3.6f, 3.8f }; //how far you push enemy back for each rank
    //public int weaponLevel;

    public float speed = 2f;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;
    private bool hasCollided;
    public Weapon weapon; //TODO: make this an object that inherits Weapon instead of something completely separate
    public Player player;

    //public float lifespan;//MOVED TO PLAYER
    private float spawnTime;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        //rb.velocity = transform.right * speed;
        player = GameManager.instance.player;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = false;
        spawnTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        weapon = GameManager.instance.player.weapon;

        SetProjectileDirection(GameManager.instance.player.playerDirection);
    }

    private void Update()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        //Collision work
        boxCollider.OverlapCollider(filter, hits); //take BoxCollider and look for other collision and put its into the hits[]
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            //Debug.Log(hits[i].name);//this will check all 10 collision slots of our array

            OnCollide(hits[i]);

            //The array is not cleaned up, so we di it ourself
            hits[i] = null;

        }
        TimeCheckOut();

    }
    private void TimeCheckOut()
    {
        if (Time.time - spawnTime > player.lifespan)
        {
            Destroy(this.gameObject);
        }
    }


    private void SetProjectileDirection(double playerDirection)
    {
        //Debug.Log("Connected");

        if (playerDirection == 0)
        {
            //transform.position += transform.forward * speed * Time.deltaTime;
            rb.velocity = new Vector3(1,0,0) * speed;
            transform.rotation = Quaternion.Euler(0, 0, -90);
            //transform.right *speed*Time.deltaTime;
        }
        else if (playerDirection == 0.5)
        {
            rb.velocity = new Vector3(1, -1, 0) * speed;
            transform.rotation = Quaternion.Euler(0, 0, -135);
        }
        else if (playerDirection == 1)
        {
            rb.velocity = new Vector3(0, -1, 0) * speed;
            transform.rotation = Quaternion.Euler(0, 0, -180);
        }
        else if (GameManager.instance.player.playerDirection == 1.5)
        {
            rb.velocity = new Vector3(-1, -1, 0) * speed;
            transform.rotation = Quaternion.Euler(0, 0, -225);
        }
        else if (GameManager.instance.player.playerDirection == 2)
        {
            rb.velocity = new Vector3(-1, 0, 0) * speed;
            transform.rotation = Quaternion.Euler(0, 0, -270);
        }
        else if (GameManager.instance.player.playerDirection == 2.5)
        {
            rb.velocity = new Vector3(-1, 1, 0) * speed;
            transform.rotation = Quaternion.Euler(0, 0, -315);
        }
        else if (GameManager.instance.player.playerDirection == 3)
        {
            rb.velocity = new Vector3(0, 1, 0) * speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (GameManager.instance.player.playerDirection == 3.5)
        {
            rb.velocity = new Vector3(1, 1, 0) * speed;
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        //Debug.Log($"Collided with{coll}");
        if (coll.tag.Equals("Enemy"))
        {
            Debug.Log($"Projectile collided with {coll}");
            Damage dmg = new Damage()
            {
                damageAmount = weapon.CalculateDamage(),
                origin = transform.position,
                pushForce = weapon.knockBack
            };
            coll.SendMessage("ReceiveDamage", dmg);

            Destroy(gameObject);
        }
    }
  
}
