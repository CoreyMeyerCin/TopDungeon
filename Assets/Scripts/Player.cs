using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    private BoxCollider2D boxCollider;

    private Vector3 moveDelta; //holds our position

    private RaycastHit2D hit;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    //since were using physics we are going to use FixedUpdate instead of Update for movement
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");// this will give us -1,1,or 0 depending if we are ising a,d, or no input.
        float y = Input.GetAxisRaw("Vertical");// same thing but with the y axis with w,s, or no input
        //Reset MoveDelta
        moveDelta = new Vector3 (x, y, 0);//sets out position to previous position plus x,y,0

        //Swap sprite directions wether you're going right pr left
        if(moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;//this sets Vecotor3 to (1,1,1) when using Vecor3.one it uses less memory
        }else if(moveDelta.x <0){
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //this will check to see if our Player is able to walk throough certain layers
                               //current position|| this.BoxCollider||angle||where were trying to move||how far we are trying to move|| which masks are we not allowed to move through
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0,new Vector2(0,moveDelta.y),Mathf.Abs(moveDelta.y * Time.deltaTime),LayerMask.GetMask("Actor","Blocking"));
        if(hit.collider == null)
        {
        transform.Translate(0,(moveDelta.y * Time.deltaTime),0);// this will adjust with framerate and move only on the y-axis if there is no collision detected
        }

        //x-axis same thing as above
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate((moveDelta.x * Time.deltaTime),0, 0);// this will adjust with framerate and move only on the x-axis if there is no collision detected
        }

    }
}