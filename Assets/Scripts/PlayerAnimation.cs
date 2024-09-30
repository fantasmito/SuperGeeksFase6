using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimation : MonoBehaviour
{
   Animator anim;
   void Start()
   {
       anim = GetComponent<Animator>();    
   }
   public void ChangeAnimation(float inputX, float inputY)
   {
       if (inputX != 0)
       {
           anim.Play("Player Walk Horizontal");
           transform.localScale = new Vector3(-inputX, 1, 1);
       }
       else if (inputY > 0)
           anim.Play("Player Walk Up");
       else if (inputY < 0)
           anim.Play("Player Walk Down");
       else
           anim.Play("Player Idle");
   }
}