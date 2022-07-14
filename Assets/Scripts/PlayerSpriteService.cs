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
    private enum Sprite
    {
    Idle,//When standing still
    Walking,//when speed is > 3.0
    Running, //when speed is <=3.0 or "speedUp" is happening(not made yet)
    MeleeOneHanded,//when a 1 handed weapon is equiped(not made yet)
    MeleeTwoHanded,//When a 2 handed weapon is equiped(not made yet)
    RangedThrownOneHanded,//When a 1 handed thrown weapon is equiped(not made yet)
    RandedThrownTwoHanded,//When a 2 handed thrown weapon is equiped(not made yet)
    RangedBow,//When a bow is equiped(not made yet)
    Climbing,//when 'climbing' is true(not made yet)
    ExhaustedIdle,//when playerHealth is below 20% and player is idle(not made yet)
    Dashing,//when player uses their dash
    Damaged//When player takes damage
    }
}
