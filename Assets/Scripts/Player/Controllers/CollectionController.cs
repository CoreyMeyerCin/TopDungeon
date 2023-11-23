using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // lets us look at in the inspector

public class CollectionController : MonoBehaviour
{
    //public string name; //for ui
    //public string description; // for ui
    //public int skinId;

    //public Sprite itemSprite;
    //private HealthService healthService;
    //public Weapon? weapon;
    //public Projectile? projectile;
    //public Transform trans;
    //public float projectileLifespan; //used for projectile range
    //public BoxCollider2D coll;
    //public Vector3 holdPosition;

    //public void Start()
    //{
    //    GetComponent<SpriteRenderer>().sprite = itemSprite;
    //    coll = gameObject.AddComponent<BoxCollider2D>();
    //    coll.enabled = true;
    //    coll.isTrigger = true;
    //    if (skinId == 0)
    //    {
    //        skinId = GameManager.instance.player.skinId;
    //    }
    //}

    //void Update()
    //{
    //    coll.enabled = true;

    //}

    //private void OnTriggerEnter2D(Collider2D coll)
    //{
    //    if (coll.tag.Equals("Player"))
    //    {
    //        Player.collectedAmount++;
    //        GameManager.instance.player.ChangeSkinId(skinId);
    //        ChangeAllSkinValuesOfItemsOnTheFloor();

    //        Debug.Log("Collected object!");

    //        if (weapon != null && projectile ==null)
    //        {
    //            GameManager.instance.player.ChangeCurrentWeapon(weapon);
    //        }
    //        if (weapon != null && projectile != null)
    //        {
    //            GameManager.instance.player.ChangeCurrentProjectile(projectile, weapon);
    //        }

    //        Destroy(gameObject);
    //    }
    //}

    //public void ChangeAllSkinValuesOfItemsOnTheFloor()
    //{
    //    var items = GameObject.FindGameObjectsWithTag("ItemOnFloor");

    //    foreach(GameObject item in items)
    //    {
    //            item.GetComponent<CollectionController>().skinId = GameManager.instance.player.skinId;
    //    }

    //}
}
