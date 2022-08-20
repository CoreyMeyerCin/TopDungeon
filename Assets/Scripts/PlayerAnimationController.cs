using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    private string currentAnimationState;
    public Rigidbody2D rb2D;

    public readonly string PLAYER_IDLE = "Idle";
    public readonly string PLAYER_WALK = "Walk";
    public readonly string PLAYER_RUN = "Run";
    public readonly string PLAYER_DASH = "Dash";
    public readonly string PLAYER_ATTACK = "Attack";

    public Dictionary<int, string> skinIds = new Dictionary<int, string>()
    {
        {1,"WindArcherElfFemale"},
        {2,"WindArcherElfMale"}
    };

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void ChangeAnimationState(string newAnimationState)
    {
        //These next 3 lines only work if we have titled the animations starting with the INT skinId.
        string currentSkin = skinIds[Player.instance.skinId];
        string weaponName = Player.instance.weapon.weaponName;
        currentAnimationState = newAnimationState;
        /*Example of what the next string should look like:
        * WindArcherFemaleThrowingDaggerIdle
        */
        string currentAnimation = currentSkin + weaponName + newAnimationState;

        if (currentAnimationState == currentAnimation) return; //stops the animation form interrupting itself

        animator.Play(currentAnimation);
        currentAnimationState = newAnimationState;
        //Debug.Log(currentAnimation);
    }
    public void TestingChangeAnimationState()//HardCode the values into here
    {
        //These next 3 lines only work if we have titled the animations starting with the INT skinId.
        string currentSkin = skinIds[2];
        Debug.Log($"Current skin name:{currentSkin}");
        string weaponName = Player.instance.weapon.weaponName;
        currentAnimationState = PLAYER_IDLE;
        /*Example of what the next string should look like:
        * WindArcherFemaleThrowingDaggerIdle
        */
        string currentAnimation = currentSkin + weaponName + PLAYER_IDLE;

        if (currentAnimationState == currentAnimation) return; //stops the animation form interrupting itself

        animator.Play(currentAnimation);
        //Debug.Log(currentAnimation);
    }



    public void RefreshSprite()
	{
        ChangeAnimationState(currentAnimationState);
	}

    public void FlipX()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
    
}
