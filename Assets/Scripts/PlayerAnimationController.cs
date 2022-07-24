using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerAnimationController : MonoBehaviour
{

    public Player player;
    public Animator animator;
    private string currentState;
    public Rigidbody2D rb2D;

    public Dictionary<int, string> skinIds = new Dictionary<int, string>()
    {
        {1,"WindArcherElfFemale"},
        {2,"WindArcherElfMale"}
    };
    //Animation States
    public string PLAYER_IDLE = "Idle";
    public string PLAYER_WALK = "Walk";
    public string PLAYER_RUN = "Run";
    public string PLAYER_DASH = "Dash";
    public string PLAYER_ATTACK = "Attack";
    private void Start()
    {
        player = GameManager.instance.player;
        animator = player.GetComponent<Animator>();
        rb2D = player.GetComponent<Rigidbody2D>();
    }

    public void ChangeAnimationState(string newState)
    {
                //These next 3 lines only work if we have titled the animations starting with the INT skinId.
        string currentSkin = skinIds[player.skinId];
        string weaponName = player.weapon.weaponName;
        currentState = newState;
                /*Example of what the next string should look like:
                * WindArcherFemaleThrowingDaggerIdle
                */
        string currentAnimation = currentSkin + weaponName + newState;

                //This stops the animation form interrupting itself
        if (currentState == currentAnimation) return;

                //plays the animation we need
        animator.Play(currentAnimation);
                //reassigns the current state
        currentState = newState;
        //Debug.Log(currentAnimation);
    }

    public void FlipX()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
    
}
