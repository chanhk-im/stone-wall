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

    bool isLive;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyScanner = GetComponent<EnemyScanner>();    
    }

    void FixedUpdate() {
        isLive = GameManager.instance.isLive;
        if (!isLive) return;

        Vector2 nextVector = moveVector.normalized * speed * Time.fixedDeltaTime;
        rigidbody2D.MovePosition(rigidbody2D.position + nextVector);
    }

    void OnMove(InputValue input) {
        isLive = GameManager.instance.isLive;
        if (!isLive) return;

        moveVector = input.Get<Vector2>();
    }

    void LateUpdate() {
        isLive = GameManager.instance.isLive;
        if (!isLive) return;

        animator.SetFloat("Speed", moveVector.magnitude);
        if (moveVector.x != 0) {
            sprite.flipX = moveVector.x <= 0;
        }
    }

    public IEnumerator HitByEnemy(int damage) {
        sprite.color = Color.red;
        GameManager.instance.health -= damage;
        if (GameManager.instance.health < 0) {
            for (int i = 1; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }

        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
