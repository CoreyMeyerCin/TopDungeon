using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    private string currentAnimationState;
    public Rigidbody2D rb2D;

<<<<<<< HEAD
=======
    public readonly string PLAYER_IDLE = "Idle";
    public readonly string PLAYER_WALK = "Walk";
    public readonly string PLAYER_RUN = "Run";
    public readonly string PLAYER_DASH = "Dash";
    public readonly string PLAYER_ATTACK = "Attack";

>>>>>>> a7e17aaf426cfad08fcc67acfe00c97eb8a03360
    public Dictionary<int, string> skinIds = new Dictionary<int, string>()
    {
        {1,"WindArcherElfFemale"},
        {2,"WindArcherElfMale"}
    };
<<<<<<< HEAD
    //Animation States
    public string PLAYER_IDLE = "Idle";
    public string PLAYER_WALK = "Walk";
    public string PLAYER_RUN = "Run";
    public string PLAYER_DASH = "Dash";
    public string PLAYER_ATTACK = "Attack";
=======

>>>>>>> a7e17aaf426cfad08fcc67acfe00c97eb8a03360
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void ChangeAnimationState(string newAnimationState)
    {
<<<<<<< HEAD
                //These next 3 lines only work if we have titled the animations starting with the INT skinId.
        string currentSkin = skinIds[player.skinId];
        string weaponName = player.weapon.weaponName;
        currentState = newState;
                /*Example of what the next string should look like:
                * WindArcherFemaleThrowingDaggerIdle
                */
        string currentAnimation = currentSkin + weaponName + newState;
=======
        //These next 3 lines only work if we have titled the animations starting with the INT skinId.
        string currentSkin = skinIds[Player.instance.skinId];
        string weaponName = Player.instance.weapon.weaponName;
        currentAnimationState = newAnimationState;
        /*Example of what the next string should look like:
        * WindArcherFemaleThrowingDaggerIdle
        */
        string currentAnimation = currentSkin + weaponName + newAnimationState;
>>>>>>> a7e17aaf426cfad08fcc67acfe00c97eb8a03360

        if (currentAnimationState == currentAnimation) return; //stops the animation form interrupting itself

        animator.Play(currentAnimation);
<<<<<<< HEAD
                //reassigns the current state
        currentState = newState;
        //Debug.Log(currentAnimation);
    }

=======
        currentAnimationState = newAnimationState;
        //Debug.Log(currentAnimation);
    }

    public void RefreshSprite()
	{
        ChangeAnimationState(currentAnimationState);
	}

>>>>>>> a7e17aaf426cfad08fcc67acfe00c97eb8a03360
    public void FlipX()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
    
}
