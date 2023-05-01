using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Image lifeBar;
    public AudioSource damageSound;


    private Keyboard keyboard;

    float attackRate = 2f;
    float hurtRate = 2f;
    float nextAttackTime = 0f;
    float nextHurtTime = 0f;
    public int LifeHeartIncrease = 10;

    public int initialAttackDamage;
    public static int attackPowerup;
    private int attackDamage;

    public static int maxHealth = 100;
    public static int currentHealth = 100;


    void Start(){
        // currentHealth = maxHealth;
        attackDamage = initialAttackDamage + attackPowerup;
        animator.SetBool("IsDead",false);

    }


    void Update()
    {
        attackDamage = initialAttackDamage + attackPowerup;
        // Debug.Log(attackDamage);
        
        if(Time.time>=nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space)){
                OnMelee();
                nextAttackTime = Time.time + 1f/attackRate;
            }
        }

        if(Time.time>=nextHurtTime)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color =  new Color(255f, 255f, 255f, 1f);
        }

        if (currentHealth<=0){
            animator.SetBool("IsDead",true);
            Debug.Log("Player Died!");

        }

        if (Camera.main.transform.position.y > transform.position.y+5){
            animator.SetBool("IsDead",true);
            Debug.Log("Player Died!");
        }
        
    }
    void OnMelee(){
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // If len of hitEnemies > 0, then we hit an enemy
        if (hitEnemies.Length > 0) {
            hitEnemies[0].GetComponent<Enemy>().TakeDamage(attackDamage);
            // Log de attackDamage eith string
            Debug.Log("Hit enemy with attack damage: " + attackDamage);

        }

        // foreach(Collider2D enemy in hitEnemies){
        //     enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        // }

    }

    void OnDrawGizmosSelected(){
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // void OnCollisionEnter2D(Collision2D hit) {
    //     if(hit.gameObject.CompareTag("Enemy")){
    //         component = hit.gameObject.GetComponent<Enemy>();
    //         component.InflictDamage();

    //         currentHealth-= 
            
    //     }
    // }
    
    public void TakeDamage(int damage){
        
        animator.SetTrigger("Hurt");
        nextHurtTime = Time.time + 1f/hurtRate;
        currentHealth -= damage;

        lifeBar.GetComponent<LifeBarController>().health = currentHealth;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color =  new Color(255f, 0f, 0f, 1f);
        damageSound.Play();
        // Debug log player current health
        Debug.Log("Player current health: " + currentHealth);

        
    }

    public void OnEndDieAnimation() {
        // Destroy(gameObject);
        SceneManager.LoadScene("EndGame");
    }

    void OnCollisionEnter2D(Collision2D hit) {
        if(hit.gameObject.CompareTag("LifeHeart")){
            currentHealth += LifeHeartIncrease;
            lifeBar.GetComponent<LifeBarController>().health = currentHealth;
            hit.gameObject.SetActive(false);
        }
            
    }

    public static void ResetPlayerStats() {
        currentHealth = maxHealth; // defaultHealth is a static field that holds the default value for currentHealth
        attackPowerup = 0; // defaultAttack is a static field that holds the default value for attackpowerup

    }
}
