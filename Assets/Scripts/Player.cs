﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 19.5f;
    [SerializeField] float climbSpeed = 6f;
    [SerializeField] float deathDelay = 2f;
    [SerializeField] Vector2 deathKick = new Vector2(0, 25f);
    [SerializeField] AudioClip deathSFX;

    // State
    bool isAlive = true;
    SFXplayer sfxplayer;

    // Cached components
    Rigidbody2D playerRigidbody2D;
    Animator animator;
    CapsuleCollider2D playerCollider2D;
    BoxCollider2D playerFeetCollider2D;
    float gravityScaleAtStart;

	void Start () {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider2D = GetComponent<CapsuleCollider2D>();
        playerFeetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = playerRigidbody2D.gravityScale;
        sfxplayer = FindObjectOfType<SFXplayer>();
    }
	
	void Update () {
        Cheat();

        if (!isAlive) { return; }

        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
        Die();
	}

    private void Run() {
        float controlThrow = Input.GetAxisRaw("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidbody2D.velocity.y);
        playerRigidbody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump() {
        if (!playerFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }

        if (Input.GetButtonDown("Jump") && playerRigidbody2D.velocity.y < 10) {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            playerRigidbody2D.velocity += jumpVelocityToAdd;
            animator.Play("Jumping");
        }
    }

    private void ClimbLadder() {
        if (!playerFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
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

    private void Die() {
        if (playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))) {
            sfxplayer.PlaySFX(deathSFX);
            StartCoroutine(DieAnimation());
        }
    }

    private void Cheat() {
        if (Input.GetButtonDown("Submit")) {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            SceneManager.LoadScene(sceneCount - 1);
        }
    }

    IEnumerator DieAnimation() {
        isAlive = false;
        animator.SetTrigger("Dying");
        playerRigidbody2D.velocity = deathKick;
        yield return new WaitForSecondsRealtime(deathDelay);
        
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}
