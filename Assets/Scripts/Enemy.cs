using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    public int attackDamage = 40;


    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth<=0){
            Die();
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        animator.SetTrigger("Hurt");
        // Play Hit animation

    }
    void Die(){
        Debug.Log("Enemy Died!");
        // Die Animation
        animator.SetBool("IsDead", true);

        // Disable the Enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        
    }

    void OnCollisionEnter2D(Collision2D hit) {
        if(hit.gameObject.CompareTag("Player")){
            hit.gameObject.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
            animator.SetTrigger("Attack");
        }
    }

}
