using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigidbody2D;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>(); 
    }
    public void Init(float damage, int per, Vector3 direction) {
        this.damage = damage;
        this.per = per;

        if (per > -1) {
            rigidbody2D.velocity = direction * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Enemy") || per == -1) {
            return;
        }

        per--;

        if (per == -1) {
            rigidbody2D.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
