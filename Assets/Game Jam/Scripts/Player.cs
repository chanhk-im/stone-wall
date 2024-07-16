using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 moveVector;
    public float speed;
    public EnemyScanner enemyScanner;

    Rigidbody2D rigidbody2D;
    SpriteRenderer sprite;
    Animator animator;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyScanner = GetComponent<EnemyScanner>();    
    }

    void FixedUpdate() {
        Vector2 nextVector = moveVector.normalized * speed * Time.fixedDeltaTime;
        rigidbody2D.MovePosition(rigidbody2D.position + nextVector);
    }

    void OnMove(InputValue input) {
        moveVector = input.Get<Vector2>();
    }

    void LateUpdate() {
        animator.SetFloat("Speed", moveVector.magnitude);
        if (moveVector.x != 0) {
            sprite.flipX = moveVector.x <= 0;
        }
    }
}
