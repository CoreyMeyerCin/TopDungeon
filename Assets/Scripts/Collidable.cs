using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]//This will make Unity automatically put a BoxCollider2D on any object that we put Collidable into.
public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10]; // track everything we are making contact with in this frame

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
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

    protected virtual void OnCollide(Collider2D coll){

        //Debug.Log("OnCollide want not implemented in" + this.name); //This check to see if our collision layer is being hit on an object
    }
}
