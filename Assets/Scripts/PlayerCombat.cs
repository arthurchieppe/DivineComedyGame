using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    private Keyboard keyboard;

    float attackRate = 2f;
    float nextAttackTime = 0f;

    void Update()
    {
        if(Time.time>=nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space)){
                OnMelee();
                nextAttackTime = Time.time + 1f/attackRate;
            }
        }
        
    }
    void OnMelee(){
        animator.SetTrigger("Attack");

        // else{
        //     animator.SetBool("IsAtacking",false);
        // }

    }


}
