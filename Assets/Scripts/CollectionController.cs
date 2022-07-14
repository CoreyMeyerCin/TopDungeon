using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]// lets us look at in the inspector

public class CollectionController : MonoBehaviour
{
    public string name; //for ui
    public string description;// for ui
    public Sprite itemSprite;
    private HealthService healthService;
    public Weapon? weapon;
    public Projectile? projectile;
    public Transform trans;
    public float lifespan; //used for projectile range
    public BoxCollider2D coll;
    public Vector3 holdPosition;

    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = itemSprite;
        coll = gameObject.AddComponent<BoxCollider2D>();
        coll.enabled = true;
        coll.isTrigger = true;
    }

    void Update() 
    {
        coll.enabled = true;
    }
   
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag.Equals("Player"))          
        {
            Player.collectedAmount++;
            Debug.Log("Collected object!");
            
            if (weapon != null)
            {
                GameManager.instance.player.ChangeCurrentWeapon(weapon);
            }
            
            if (projectile != null)
            {
                GameManager.instance.player.ChangeCurrentProjectile(projectile, weapon);
            }
            
            Destroy(gameObject);
        }
    }
}
