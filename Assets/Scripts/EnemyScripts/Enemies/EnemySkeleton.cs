using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : EnemyController
{
    protected override void FixedUpdate()
    {
        //Debug.Log($"Current EnemyState: {currState}");

        currentPosition = transform.position;
        if (IsPlayerInRange(range))
        {
            UnityEngine.Debug.Log(" Hit A");
            currState = EnemyState.Follow;
            Follow();
        }
        else if (IsAwayFromHome(range))
        {
            UnityEngine.Debug.Log(" Hit B");
            currState = EnemyState.Idle;
            Idle();
        }
        else if (!IsAwayFromHome(range) && currentPosition == homePosition)
        {
            UnityEngine.Debug.Log(" Hit C");
            currState = EnemyState.Wander;
            Wander();
        }
        else if (!IsAwayFromHome(range))
        {
            UnityEngine.Debug.Log(" Hit D");
            Wander();
        }
    }
}
