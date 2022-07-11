using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] sceneNames;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log(coll + " touched a portal");
        UnityEngine.Debug.Log($"Has made collision with {coll}");
        if(coll.tag.Equals("Player"))
        {
            //Teleport the player
            UnityEngine.Debug.Log($"22Has made collision wtih {coll}");
            GameManager.instance.SaveState();
            string sceneName = sceneNames[UnityEngine.Random.Range(0, sceneNames.Length)]; // this is going to assign the scene name we go to as a random scene that exists in our sceneName collection 
            SceneManager.LoadScene(sceneName);
            //Debug.Log("going to " + sceneName);

        }
    }

}
