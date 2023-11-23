using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingAnimationController animationController;
    public BuildingActionController actionController;

    public int price;
    public string name;
    public bool persistant;
    public int maxHp = 500;
    public int currentHp;
    public float buildTime;


    public void Start()
    {
        currentHp = maxHp;
    }
}

