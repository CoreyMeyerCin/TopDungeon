using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]// lets us look at in the inspector

public class CollectionController : Collidable
{
    GameObject player;
    public string name; //for ui
    public string description;// for ui
    public Sprite itemSprite;
    public float healthChange;
    public float moveSpeed;
    public float attackSpeed;
    private HealthService healthService;
 

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<SpriteRenderer>().sprite = itemSprite;
        Destroy(GetComponent<BoxCollider2D>());
        gameObject.AddComponent<BoxCollider2D>();

    }

    protected override void OnCollide(Collider2D coll)
    {

        if (coll.name.Equals("Player"))
        {

            Player.collectedAmount++;
            UnityEngine.Debug.Log("Collsion here!");
            //GameManager.instance.player.healthService.HealByFlatAmount(healthChange);
            GameManager.instance.player.MoveSpeedChange(moveSpeed);
            //GameManager.instance.weapon.ChangeCooldown(attackSpeed);
            Destroy(gameObject);
        }
    }
}
