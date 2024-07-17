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
    public int damage;
    public float attackRange;
    public float attackSpeed;
    public RuntimeAnimatorController[] animatorControllers;
    public Rigidbody2D target;

    bool isLive;
    bool isKnockBack;

    Rigidbody2D rigidbody;
    Collider2D collider2D;
    Animator animator;
    SpriteRenderer sprite;
    WaitForFixedUpdate wait;
    Transform playerTransform;
    float lastAttackTime = 0f; 

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        playerTransform = GameManager.instance.player.GetComponent<Transform>();
    }

    void FixedUpdate() {
        if (!isLive || isKnockBack) return;

        Vector2 directionVector = target.position - rigidbody.position;
        Vector2 nextVector = speed * Time.fixedDeltaTime * directionVector.normalized;

        rigidbody.MovePosition(rigidbody.position + nextVector);
        rigidbody.velocity = Vector2.zero;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackSpeed)
        {
            Attack();
            lastAttackTime = Time.time; // 마지막 공격 시간 업데이트
        }
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

    public void Init(EnemyData data) {
        animator.runtimeAnimatorController = animatorControllers[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
        reward = data.reward;
        damage = data.damage;
        attackRange = data.attackRange;
        attackSpeed = data.attackSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Bullet")) {
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(HitColor());
        StartCoroutine(KnockBack());

        if (health > 0) {
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

    void Attack() {
        Debug.Log("attack!");
        animator.SetBool("Attack", true);
        StartCoroutine(GameManager.instance.player.HitByEnemy(damage));
    }

    public void DisableAttack() {
        animator.SetBool("Attack", false);
    }

    // void LateUpdate() {
    //     sprite.flipX = target.position.x < rigidbody.position.x;
    // }
}
