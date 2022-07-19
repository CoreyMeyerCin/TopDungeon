using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D.Animation;

public class PlayerSpriteService : MonoBehaviour
{
    private SpriteResolver resolvers;
    private Sprite currentSprite;

    private void Awake()
    {
        resolvers = GetComponent<SpriteResolver>();
    }

    private void Update()
    {
        
    }

    private enum Sprite //TODO: finish making entries
    {
        Idle,
        Walking, //speed is > 3.0
        Running, //speed is <= 3.0 or "speedUp" is happening(not made yet)
        EquippedMeleeOneHanded, //(not made yet)
        EquippedMeleeTwoHanded, //(not made yet)
        EquippedRangedThrownOneHanded, //(not made yet)
        EquippedRandedThrownTwoHanded, //(not made yet)
        EquippedBow, //(not made yet)
        Climbing, //(not made yet)
        ExhaustedIdle, // playerHealth is below 20% and player is idle(not made yet)
        Dashing,
        Damaged
    }
}
