using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Config
    [SerializeField] float runSpeed = 6f;
    [SerializeField] float jumpSpeed = 6f;
    [SerializeField] float climbSpeed = 6f;

    // State
    bool isAlive = true;

    // Cached components
    Rigidbody2D playerRigidbody2D;
    Animator animator;
    Collider2D playerCollider2D;
    float gravityScaleAtStart;

	// Use this for initialization
	void Start () {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider2D = GetComponent<Collider2D>();
        gravityScaleAtStart = playerRigidbody2D.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
	}

    private void Run() {
        float controlThrow = Input.GetAxisRaw("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidbody2D.velocity.y);
        playerRigidbody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump() {
        if (!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }

        if (Input.GetButtonDown("Jump")) {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            playerRigidbody2D.velocity += jumpVelocityToAdd;
            animator.Play("Jumping");
        }
    }

    private void ClimbLadder() {
        if (!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            animator.SetBool("Climbing", false);
            playerRigidbody2D.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = Input.GetAxisRaw("Vertical");
        Vector2 climbVelocity = new Vector2(playerRigidbody2D.velocity.x, controlThrow * climbSpeed);
        playerRigidbody2D.velocity = climbVelocity;
        playerRigidbody2D.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidbody2D.velocity.y) > Mathf.Epsilon;
        animator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody2D.velocity.x), 1f);
        }
    }
}
