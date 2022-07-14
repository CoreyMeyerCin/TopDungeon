using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int collecteAmount = 0;
    public float attackSpeed = 1f;
    public float lifesteal;
    public float critChance = 5f;
    public float critMultiplier = 1.05f;
    public float cooldown = 1f;
    public int dropChanceModifier = 0;
    public float dashTime = 1f;
}