using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]//This will make Unity automatically put a BoxCollider2D on any object that we put Collidable into.
public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    protected BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10]; // track everything we are making contact with in this frame

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.OverlapCollider(filter, hits); //take BoxCollider and look for other collision and put its into the hits[]
        if (hits.Where(x => x != null).Any())
        {
            var validHits = hits.Where(x => x != null).ToList();
            System.Array.Clear(hits, 0, hits.Length);
            foreach (var hit in validHits)
            {
                OnCollide(hit);
            }
        }

    }

    protected virtual void OnCollide(Collider2D coll){

        //Debug.Log("OnCollide want not implemented in" + this.name); //This check to see if our collision layer is being hit on an object
    }
}
