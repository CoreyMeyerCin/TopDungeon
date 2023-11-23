using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingPrefab : Collidable
{
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;
    private bool hasCollided;
    
    public float lifespan = 1f;
    //public float projectileSpeed;
    private float spawnTime;
    public Player player;

    private void Start()
    {
        player = GameManager.instance.player;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = false;//Might need to be true since we are melee
        spawnTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

}
