using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public int reward;
    public RuntimeAnimatorController[] animatorControllers;
    public Rigidbody2D target;

    bool isLive;
    bool isKnockBack;

    Rigidbody2D rigidbody;
    Collider2D collider2D;
    Animator animator;
    SpriteRenderer sprite;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate() {
        if (!isLive || isKnockBack) return;

        Vector2 directionVector = target.position - rigidbody.position;
        Vector2 nextVector = directionVector.normalized * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + nextVector);
        rigidbody.velocity = Vector2.zero;
    }

    void OnEnable() {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        collider2D.enabled = true;
        rigidbody.simulated = true;
        sprite.sortingOrder = 2;
        isKnockBack = false;
        animator.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data) {
        animator.runtimeAnimatorController = animatorControllers[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
        reward = data.reward;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Bullet")) {
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(HitColor());
        StartCoroutine(KnockBack());

        if (health > 0) {
            Debug.Log("hit");
            isKnockBack = true;
        } else {
            isLive = false;
            collider2D.enabled = false;
            rigidbody.simulated = false;
            sprite.sortingOrder = 1;
            animator.SetBool("Dead", true);
            GameManager.instance.money += reward;
        }
    }

    IEnumerator HitColor() {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

    IEnumerator KnockBack() {
        yield return wait;
        Vector3 playerVector = GameManager.instance.player.transform.position;
        Vector3 directionVector = transform.position - playerVector;
        rigidbody.AddForce(directionVector.normalized * 3, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        isKnockBack = false;
    }

    void Dead() {
            gameObject.SetActive(false);
        }

    // void LateUpdate() {
    //     sprite.flipX = target.position.x < rigidbody.position.x;
    // }
}
