using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rigidbody;
    SpriteRenderer sprite;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        if (!isLive) return;

        Vector2 directionVector = target.position - rigidbody.position;
        Vector2 nextVector = directionVector.normalized * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + nextVector);
        rigidbody.velocity = Vector2.zero;
    }

    // void LateUpdate() {
    //     sprite.flipX = target.position.x < rigidbody.position.x;
    // }
}
