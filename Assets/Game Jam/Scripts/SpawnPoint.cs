using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    float timer;
    int level;
    int spawnIndex;
    public SpawnData[] spawnData;

    void Awake() {
        level = 0;
        spawnIndex = 0;
    }
    void Update()
    {
        timer += Time.deltaTime;
        float gameTime = GameManager.instance.gameTime;
        float remainTime = GameManager.instance.remainTime;
        bool isDay = GameManager.instance.isDay;

        level = GameManager.instance.days - 1;

        if (!isDay && remainTime - gameTime > 30f && timer > spawnData[level].spawnTime) {
            timer = 0;
            foreach (EnemyData enemy in spawnData[level].enemies) {
                for (int i = 0; i < enemy.mobCount; i++)
                    Spawn(enemy);
            }
        }
    }

    void Spawn(EnemyData enemyData) {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = transform.position;
        enemy.GetComponent<Enemy>().Init(enemyData);
        enemy.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

[System.Serializable]
public class SpawnData {
    public float spawnTime;
    public EnemyData[] enemies;
}

[System.Serializable]
public class EnemyData {
    public int spriteType;
    public int health;
    public float speed;
    public int reward;
    public int mobCount;
}