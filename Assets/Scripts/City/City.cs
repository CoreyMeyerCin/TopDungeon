using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City: MonoBehaviour
{
    public static City instance { get; private set; }
    public static Inventory cityInventory;
    public static Building[] buildings;
    public static int availableBuildingSlots;
    public State state;
    public enum State
    {
        Calm,
        Panic,
        UnderAttack
    }

    public State GetCityState()
    {
        return state;
    }
}
