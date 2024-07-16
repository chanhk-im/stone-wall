using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    float timer;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f) {
            timer = 0;
            Spawn(Random.Range(0, 2));
        }
    }

    void Spawn(int spawnIndex) {
        GameObject enemy = GameManager.instance.pool.Get(spawnIndex);
        enemy.transform.position = transform.position;
    }
}
