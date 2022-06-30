using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindlessEnemy : EnemyController
{

    protected override void FixedUpdate() {
        currentPosition = transform.position;
        if (IsAwayFromHome(range))
        {
            //UnityEngine.Debug.Log(" Hit B");
            currState = EnemyState.Idle;
            Idle();
        }
        else if (!IsAwayFromHome(range) && currentPosition == homePosition)
        {
            //UnityEngine.Debug.Log(" Hit C");
            currState = EnemyState.Wander;
            Wander();
        }
        else if (!IsAwayFromHome(range))
        {
            //UnityEngine.Debug.Log(" Hit D");
            Wander();
        }
    }
}
