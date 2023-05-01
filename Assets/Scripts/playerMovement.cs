using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Tilemaps;

public class playerMovement : MonoBehaviour
{
    public Animator animator;	

    public float JumpForce = 400f;
    public float velocity = 0;
    public Tilemap tilemap;

    private Rigidbody2D rb;
    private float moveX;
    private float moveY;
    private bool isFacingRight = true;
    private bool isGrounded = true;
    const float GroundedRadius = .1f;


    // private int count;
    // public TextMeshProUGUI countText;
    // public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
    }
    void Update() {
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
		// for (int i = 0; i < colliders.Length; i++)
		// {
		// 	if (colliders[i].gameObject != gameObject)
		// 	{
		// 		isGrounded = true;
        //         animator.SetBool("IsJumping",false);
		// 	}
		// }

    }

    void FixedUpdate()
	{
        // Vector2 force = new Vector2(moveX*velocity, 0.0f);
        // rb.AddForce(force);
        Vector2 horizontalMove = new Vector2(moveX*velocity, 0.0f) ;
        transform.Translate(horizontalMove * Time.deltaTime);

	}
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        moveX = movementVector.x;
        moveY = movementVector.y;
        //Debug.Log(movementVector);
        // Debug.Log(isGrounded);
        
        animator.SetFloat("Speed", Mathf.Abs(moveX));

        if (moveX > 0 && !isFacingRight)
			{
				// ... flip the player.
				Flip();
			}
        else if (moveX < 0 && isFacingRight)
        {
            // ... flip the player.
            Flip();
        }

        

        if (isGrounded && moveY>0) {
            // Add a vertical force to the player.
            //Debug.Log("Pulou!");
            isGrounded = false;
            animator.SetBool("IsJumping",true);

			rb.AddForce(new Vector2(0f, JumpForce));
        }
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        //Debug.Log("Pisou no chao!");
        // Get Y position of the player
        // float playerY = transform.position.y;
        // // Get Y position of the ground
        // float groundY = hit.transform.position.y;
        ContactPoint2D contact = hit.contacts[0];
        Vector3Int tilePosition = tilemap.WorldToCell(contact.point);
        Vector3 tileCenter = tilemap.GetCellCenterWorld(tilePosition);

        if(hit.gameObject.CompareTag("Ground") && tileCenter.y < transform.position.y){
            isGrounded = true;
            animator.SetBool("IsJumping",false);
        }
	}
    void OnCOllisionExit2D(Collision2D hit){
        if(hit.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
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


