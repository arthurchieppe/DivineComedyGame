using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerMovement : MonoBehaviour
{
    public float velocity = 0;
    private Rigidbody2D rb;
    private float moveX;
    private float moveZ;
    private bool isFacingRight = true;
    private bool isGrounded = true;

    // private int count;
    // public TextMeshProUGUI countText;
    // public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // count = 0;

        // SetCountText();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        moveX = movementVector.x;
        moveY = movementVector.y;
        Debug.Log(movementVector);

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
        if (moveY > 0 && canJump) {
            // Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    void FixedUpdate()
    {
        Vector2 force = new Vector2(moveX*velocity, 0.0f);
        rb.AddForce(force);
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


