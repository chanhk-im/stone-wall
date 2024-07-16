using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animatorControllers;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigidbody;
    Animator animator;
    SpriteRenderer sprite;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        if (!isLive) return;

        Vector2 directionVector = target.position - rigidbody.position;
        Vector2 nextVector = directionVector.normalized * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + nextVector);
        rigidbody.velocity = Vector2.zero;
    }

    void OnEnable() {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data) {
        animator.runtimeAnimatorController = animatorControllers[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Bullet")) {
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0) {

        } else {
            Dead();
        }
    }

    IEnumerator KnockBack() {
        yield return null;
    }

    void Dead() {
            gameObject.SetActive(false);
        }

    // void LateUpdate() {
    //     sprite.flipX = target.position.x < rigidbody.position.x;
    // }
}
