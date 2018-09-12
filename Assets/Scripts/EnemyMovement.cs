using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] float moveSpeed = 2f;

    Rigidbody2D enemyRigidbody2D;

    void Start () {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        if(IsFacingRight()) {
            enemyRigidbody2D.velocity = new Vector2(moveSpeed, 0f);
        }
        else {
            enemyRigidbody2D.velocity = new Vector2(-moveSpeed, 0f);
        }
        
	}

    bool IsFacingRight() {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidbody2D.velocity.x)), 1f);
    }
}
