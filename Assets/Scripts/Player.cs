using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{

   private SpriteRenderer spriteRenderer;
   protected override void Start(){
      base.Start();
      spriteRenderer= GetComponent<SpriteRenderer>();
   }
   private void FixedUpdate(){
       
        float x = Input.GetAxisRaw("Horizontal");// this will give us -1,1,or 0 depending if we are ising a,d, or no input.
        float y = Input.GetAxisRaw("Vertical");// same thing but with the y axis with w,s, or no input
        //Reset MoveDelta
        UpdateMotor(new Vector3(x,y,0));
   }

   public void SwapSprite(int skinId){
      spriteRenderer.sprite= GameManager.instance.playerSprites[skinId];
   }
   public void OnLevelUp(){
      maxHitpoint++;
      hitpoint = maxHitpoint;
   }
   public void SetLevel(int Level){
      for(int i=0; i<Level; i++){
         OnLevelUp();
      }
   }
    
}