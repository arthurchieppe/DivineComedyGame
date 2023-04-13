using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerMovement : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private LayerMask WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform CeilingCheck;	

    public float JumpForce = 400f;
    public float velocity = 0;

    private Rigidbody2D rb;
    private float moveX;
    private float moveY;
    private bool isFacingRight = true;
    private bool isGrounded = true;
    const float GroundedRadius = .2f;

    PlayerInputAsset playerInputAsset;

    // private int count;
    // public TextMeshProUGUI countText;
    // public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputAsset = new PlayerInputAsset();
    }

    void FixedUpdate()
	{
        Vector2 force = new Vector2(moveX*velocity, 0.0f);
        rb.AddForce(force);



        bool isSpaceKeyHeld = playerInputAsset.Player.SpaceKey.ReadValue<float>() > 0.1f;
        Debug.Log(playerInputAsset.Player.SpaceKey.ReadValue<float>());
        
        if(isSpaceKeyHeld){
            animator.SetBool("IsAtacking",true);

        }
        else{
            animator.SetBool("IsAtacking",false);
        }
		isGrounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				isGrounded = true;
                animator.SetBool("IsJumping",false);


			}
		}


	}
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        moveX = movementVector.x;
        moveY = movementVector.y;
        Debug.Log(movementVector);
        Debug.Log(isGrounded);
        
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
			isGrounded = false;
            animator.SetBool("IsJumping",true);

			rb.AddForce(new Vector2(0f, JumpForce));
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("PickUp"))
    //     {
    //         other.gameObject.SetActive(false);
    //         count++;
    //         SetCountText();
    //     }
    //     if (other.gameObject.CompareTag("PenaltyWall"))
    //     {
    //         UImanager ui = canvas.GetComponent<UImanager>();
    //         ui.deductTimer();
    //         count--;
    //     }
    // }
    // void SetCountText()
	// {
	// 	countText.text = "Count: " + count.ToString();
	// }


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


