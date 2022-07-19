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
        if(coll.CompareTag("Player"))
        {
			Debug.Log($"Player collided with portal {coll}");
            GameManager.instance.SaveState();
            string sceneName = sceneNames[UnityEngine.Random.Range(0, sceneNames.Length)]; //assign the scene name we go to as a random scene that exists in our sceneName collection 
            SceneManager.LoadScene(sceneName);
        }
    }

}
