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

    private void OnTriggerEnter2D(Collider2D other) {
        isInsideNoBuildZone = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        isInsideNoBuildZone = false;
    }
}
