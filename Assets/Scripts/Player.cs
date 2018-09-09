using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Config
    [SerializeField] float runSpeed = 5f;

    // State
    bool isAlive = true;

    // Cached components
    Rigidbody2D rigidBody2D;
    Animator animator;

	// Use this for initialization
	void Start () {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Run();
        FlipSprite();
	}

    private void Run() {
        float controlThrow = Input.GetAxisRaw("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rigidBody2D.velocity.y);
        rigidBody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(rigidBody2D.velocity.x), 1f);
        }
    }
}
