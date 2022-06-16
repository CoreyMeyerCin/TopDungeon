using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
		//Debug.Log(coll + " touched a portal");
    
        if(coll.name.Equals("Player"))
        {
            //Teleport the player
            
            GameManager.instance.SaveState();
            string sceneName = sceneNames[UnityEngine.Random.Range(0, sceneNames.Length)]; // this is going to assign the scene name we go to as a random scene that exists in our sceneName collection 
            SceneManager.LoadScene(sceneName);
            //Debug.Log("going to " + sceneName);

        }
    }

}
