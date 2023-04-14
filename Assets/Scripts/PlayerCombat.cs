using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private Keyboard keyboard;

    float attackRate = 2f;
    float nextAttackTime = 0f;
    int attackDamage = 40;



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

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    void OnDrawGizmosSelected(){
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
