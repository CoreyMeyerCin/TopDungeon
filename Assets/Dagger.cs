using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{

    public int[] damage = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; //amount of damage each weapon with upgrade does
    public float[] knockbackDistance = { 2.0f, 2.2f, 2.4f, 2.6f, 2.8f, 3.0f, 3.2f, 3.4f, 3.6f, 3.8f }; //how far you push enemy back for each rank

    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    public float speed = 2f;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;
    private bool hasCollided;
    
    // Start is called before the first frame update
    void Start()
    {

        rb.velocity = transform.right * speed;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
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

    }
    private void OnCollide(Collider2D coll)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (coll.tag == "Enemy" && !coll.name.Equals("Player"))
            {
                Damage dmg = new Damage()
                {
                    damageAmount = 3,
                    origin = transform.position,
                    pushForce = 2
                };
                coll.SendMessage("ReceiveDamage", dmg);

                // send the damage over to the enemy using ReceiveDamage()
                Debug.Log("Sending the damage: " + dmg);
                Destroy(this.gameObject);
            }
            if (coll.tag == "NPC")
            {
                Debug.Log(coll + "was hit");
                Destroy(this.gameObject);
            }
            //Debug.Log(hitInfo.name);
        }
    }
  
}
