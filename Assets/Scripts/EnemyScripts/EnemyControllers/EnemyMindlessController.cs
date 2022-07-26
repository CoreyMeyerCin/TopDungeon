using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMindlessController : EnemyController
{

    protected override void FixedUpdate()
    {
        currentPosition = transform.position;
        if (IsAwayFromHome(sightRange))
        {
            //Debug.Log(" Hit B specific");
            state = EnemyState.Idle;
            Idle();
        }
        else if (!IsAwayFromHome(sightRange) && currentPosition == homePosition)
        {
            //Debug.Log(" Hit C");
            state = EnemyState.Wander;
            Wander();
        }
        else if (!IsAwayFromHome(sightRange))
        {
            //Debug.Log(" Hit D");
            Wander();
        }
    }
}