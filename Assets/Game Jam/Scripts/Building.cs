using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int cost;
    public bool isBuilded;
    public bool isInsideNoBuildZone;

    private SpriteRenderer sprite;

    private void Awake() {
        health = maxHealth;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        isInsideNoBuildZone = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        isInsideNoBuildZone = false;
    }

    public IEnumerator HitByEnemy(int damage) {
        sprite.color = Color.red;
        health -= damage;
        if (health < 0) {
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
