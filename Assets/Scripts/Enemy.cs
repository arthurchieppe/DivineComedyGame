using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    public int attackDamage = 40;
    private bool isFacingRight = true;
    public Image powerBar;
    public AudioSource damageSound;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    
    public float moveSpeed = 1f;
    public float followDistance = 4f;
    Transform playerTransform;
    private bool attackCooldown = false;
    private float nextAttackTime;
    float attackRate = 2f;



    void Start()
    {
        currentHealth = maxHealth;
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth<=0){
            Die();

        }

        if(Time.time>=nextAttackTime)
        {
            attackCooldown = false;
        } 

        // Calculate the distance between the player and enemy
        float distanceToPlayerX = Mathf.Abs(transform.position.x -  playerTransform.position.x);
        float distanceToPlayerY = Mathf.Abs(transform.position.y -  playerTransform.position.y);

        // If the player is within the follow distance, move towards the player
        if (distanceToPlayerX < followDistance && distanceToPlayerY < 1)
        {
            animator.SetFloat("Speed", 1);

            Vector2 movement = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);


            if (movement[0]<playerTransform.position.x && !isFacingRight){
                Flip();
            }
            else if (movement[0]>playerTransform.position.x && isFacingRight){
                Flip();
            }

            transform.position = new Vector2(movement[0], transform.position.y);
        
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        animator.SetTrigger("Hurt");
        damageSound.Play();

        // Play Hit animation

    }
    void Die(){
        Debug.Log("Enemy Died!");
        powerBar.GetComponent<PowerBarController>().power +=1;

        // Die Animation
        animator.SetBool("IsDead", true);

        // Disable the Enemy
        // GetComponent<Collider2D>().enabled = false;
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders) {
            collider.enabled = false;
        }          
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    
        // Disable the other collider
        this.enabled = false;

        
    }

    void OnCollisionStay2D(Collision2D hit) {
        if(hit.gameObject.CompareTag("Player") && attackCooldown == false){
            nextAttackTime = Time.time + 1f/attackRate;
            attackCooldown = true;
            animator.SetTrigger("Attack");
        }
    }

    void OnEndAttackAnimation(){
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        Debug.Log(hitPlayer);
        foreach(Collider2D player in hitPlayer){
            player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected(){
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		isFacingRight = !isFacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
