using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMindless : EnemyController
{

    protected override void FixedUpdate()
    {
        currentPosition = transform.position;
        if (IsAwayFromHome(sightRange))
        {
            //UnityEngine.Debug.Log(" Hit B");
            state = EnemyState.Idle;
            Idle();
        }
        else if (!IsAwayFromHome(sightRange) && currentPosition == homePosition)
        {
            //UnityEngine.Debug.Log(" Hit C");
            state = EnemyState.Wander;
            Wander();
        }
        else if (!IsAwayFromHome(sightRange))
        {
            //UnityEngine.Debug.Log(" Hit D");
            Wander();
        }
    }
}