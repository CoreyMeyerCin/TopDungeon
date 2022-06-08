using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;//put our camera focus on a specific item

    public float boundX = 0.15f;//these two are how far our player can move before it snaps back to them(0.15f with our scene settings is 15 pixels)
    public float boundY = 0.05f;

    private void Start(){
        lookAt = GameObject.Find("Player").transform;
        GameManager.instance.player.transform.position= GameObject.Find("SpawnPoint").transform.position;
    }


    private void LateUpdate()//we do a late update for this(Order of operations FixedUpdate => Update => LateUpdate) we use this here to move the camera AFTER the player moves
    {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;//this is the center of the camera. So we look at the difference between the center of the player and this position

        if(deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }

            if(transform.position.x > lookAt.position.x)
            {
                delta.x = deltaX + boundX;
            }
        }


        //do the same thing here but for the y-axis

        float deltaY = lookAt.position.y - transform.position.y;//this is the center of the camera. So we look at the difference between the center of the player and this position

        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }

            if (transform.position.y > lookAt.position.y)
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);//we update this according to our last 2 if statements
    }
}
