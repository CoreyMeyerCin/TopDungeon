using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    public static Player instance; 
    private SpriteRenderer spriteRenderer;

    public double playerDirection;
    public int level = 1;
    public int gold;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //instance = this;
    //DontDestroyOnLoad(gameObject);
    //SceneManager.sceneLoaded += LoadState;

    private void Update()
    {
        GetPlayerDirection();
    }
    private void FixedUpdate()
    {   
        float x = Input.GetAxisRaw("Horizontal"); // this will give us -1,1,or 0 depending if we are ising a,d, or no input.
        float y = Input.GetAxisRaw("Vertical"); // same thing but with the y axis with w,s, or no input
        //Reset MoveDelta
        UpdateMotor(new Vector3(x,y,0));

        
    }

    public void GetPlayerDirection()
    {

         if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            playerDirection = 0.5;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            playerDirection = 1.5; ;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            playerDirection = 2.5; ;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            playerDirection = 3.5;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerDirection = 0;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerDirection = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerDirection = 2;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            playerDirection = 3;
        }

       
        
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player has collided with something");
    }

    //************************************************
    //ACCESSOR METHODS
    //************************************************
    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int levelToSet)
    {
        level = levelToSet;
    }

}
