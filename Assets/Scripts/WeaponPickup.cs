using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().weapon = weapon;
            collision.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = weapon.sprite;
            Destroy(gameObject);
        }
    }
}
