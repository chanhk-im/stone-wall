using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    float timer;
    int level;
    public SpawnData[] spawnData;

    void Awake() {
        level = 0;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButtonDown("Jump")) {
            level = 1;
        }

        if (timer > spawnData[level].spawnTime) {
            timer = 0;
            Spawn(Random.Range(0, 2));
        }
    }

    void Spawn(int spawnIndex) {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = transform.position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
        enemy.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

[System.Serializable]
public class SpawnData {
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
    public int reward;
}
