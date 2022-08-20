using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAnimationController : MonoBehaviour
{
    public Animator animator;
    private string currentAnimationState;
    public Rigidbody2D rb2D;

    public readonly string BUILDING_BEINGBUILT = "BeingBuilt";
    public readonly string BUILDING_DEFAULT = "Default";
    public readonly string BUILDING_PLAYERNEAR = "PlayerNear";
    public readonly string BUILDING_DANGER = "Danger";
    public readonly string BUILDING_ONFIRE = "OnFire";
    public readonly string BUILDING_ATTACKING = "Attack";
    public readonly string BUILDING_DESTROYED = "Destroyed";

    public string buildingName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        buildingName = gameObject.GetComponent<Building>().name;
    }

    public void ChangeAnimationState(string newAnimationState)
    {
        string buildingSkin = buildingName;
        string buildingState = gameObject.GetComponent<Building>().actionController.state.ToString();
        currentAnimationState = newAnimationState;
        /*
         * Example of what the string should look like:
         * ChurchBeingBuilt || CurchOnFire || MarketOnFire
         * 
         */

        string currentAnimation = buildingSkin + buildingState;

        if (currentAnimationState == currentAnimation) return;
        
        animator.Play(currentAnimation) ;
        currentAnimationState = newAnimationState;
    }

    public void RefreshSprite()
    {
        ChangeAnimationState(currentAnimationState);
    }

}
