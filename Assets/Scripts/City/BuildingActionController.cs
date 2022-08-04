using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingActionController : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public BuildingAnimationController buildingAnimationController;
    public State state;
    public Building building;
    public string cityState;

    public float buildingCompleteTime;

    public bool isPlayerNear;
    public bool isOnFire;
    public float fireTickTime;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        buildingAnimationController = GetComponent<BuildingAnimationController>();
        state = State.BeingBuilt;
        building = gameObject.GetComponent<Building>();
        buildingCompleteTime = Time.time + building.buildTime;
    }

    private void Update() 
    {
        if(building.currentHp < building.maxHp && isPlayerNear)
        {
            int repairCost = GenerateBuildingRepairCost(); 
            //NEED hud pop up here...
            
        }

        cityState = City.instance.GetCityState().ToString();
        if (isOnFire && Time.time>= fireTickTime)
        {
            CalculateOnFireDamage();
        }

        switch (state)
        {
            case State.BeingBuilt:
                building.animationController.ChangeAnimationState(building.animationController.BUILDING_BEINGBUILT);
                if (Time.time >= buildingCompleteTime)
                {
                    this.state = State.Default;
                }
                break;
            
            case State.CityInDanger:
                if (cityState == "Panic")
                {
                    this.state = State.CityInDanger;
                }
                break;
            case State.isAttacking:
                break;
            case State.Destroyed:

                building.animationController.ChangeAnimationState(building.animationController.BUILDING_DESTROYED);
                break;

            case State.Default:
                building.animationController.ChangeAnimationState(building.animationController.BUILDING_DEFAULT);


                if(building.currentHp<=0)
                {
                    building.currentHp = 0;
                    this.state = State.Destroyed;
                }


                if (cityState == "Panic")
                {
                    this.state = State.CityInDanger;
                }

                if (isPlayerNear)
                {
                    building.animationController.ChangeAnimationState(building.animationController.BUILDING_PLAYERNEAR);
                }
                break;
        }
    }

    private int GenerateBuildingRepairCost()
    {
        return (building.maxHp - building.currentHp) / 5;
    }

    private void CalculateOnFireDamage()
    {
        DamageBuilding(10);
        float fireTickTime=Time.time+2f;
    }

    private void DamageBuilding(int damageAmount)
    {
        building.currentHp -= damageAmount;
    }

    public enum State
    {
        BeingBuilt,
        Default,
        CityInDanger,
        isAttacking,
        Destroyed
    }
}
