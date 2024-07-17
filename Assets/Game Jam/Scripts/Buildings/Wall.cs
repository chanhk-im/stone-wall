using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    float lastRegenerateTime = 0f;
    readonly float regenerateSpeed = 1f;
    public int healthRegeneration;
    private int originalHealthRegeneration;

    private Building building;
    void Awake()
    {
        building = GetComponent<Building>();
    }

    void Start() {
        StartCoroutine(RegenerateHealth());
        originalHealthRegeneration = healthRegeneration;
    }

    // Update is called once per frame
    private IEnumerator RegenerateHealth() {
        while (true) {
            yield return new WaitForSeconds(regenerateSpeed);
            Regenerate();
        }
    }

    private void Regenerate() {
        if (Time.time >= lastRegenerateTime + regenerateSpeed)
        {
            if (building.health < building.maxHealth) {
                if (building.health + healthRegeneration > building.maxHealth) {
                    building.health = building.maxHealth;
                } else {
                    building.health += healthRegeneration;
                }
                lastRegenerateTime = Time.time;
            }
        }
    }

    private void OnDisable()
    {
        healthRegeneration = originalHealthRegeneration;
    }
}
