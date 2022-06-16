using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : MonoBehaviour
{

    // while player is collided with healing fountain:
    // every .3 seconds -> this.HealthManager.HealByPercentageOfMax(5);
    // interval can change, personally I think multiple ticks/sec feels better.
    // input an int into the method which is the % of max hp healing per tick

}
