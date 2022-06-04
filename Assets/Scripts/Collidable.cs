using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]//This will make Unity automatically put a BoxCollider2D on any object that we put Collidable into.
public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;//uses Unity built in BoxCollider2D
    private Collider2D[] hits = new Collider2D[10];// this tracks what all we are making contact with in this frame

    protected virtual void Start()
    {

        boxCollider = GetComponent<BoxCollider2D>();//gets our BoxCollider

    }
    protected virtual void Update()
    {
        //Collision work
        boxCollider.OverlapCollider(filter, hits);//this takes the BoxCollider and looks for other collision and put its into the hits[]
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

        Debug.Log(coll.name);
    }
}
