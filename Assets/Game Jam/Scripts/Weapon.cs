using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    void Awake() {
        player = GetComponent<Player>();
    }

    void Start() {
        Init();
    }

    void Update() {
        switch (id) {
            case 0:
                timer += Time.deltaTime;
                if (timer > speed) {
                    timer = 0f;
                    Fire();
                }
                break;
            default:
                break;
        }

        // Test
        if (Input.GetKeyDown(KeyCode.LeftAlt)) {
            LevelUp(20, 5);
        }
    }

    public void LevelUp(float damage, int count) {
        this.damage = damage;
        this.count += count;
    }

    public void Init() {
        switch (id) {
            case 0:
                speed = 0.3f;
                break;
            default:
                break;
        }
    }

    void Fire() {
        if (!player.enemyScanner.nearstTarget) return;

        Vector3 targetPosition = player.enemyScanner.nearstTarget.position;
        Vector3 direction = targetPosition - transform.position;
        direction = direction.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.right, direction);
        bullet.GetComponent<Bullet>().Init(damage, count, direction);
    }
}
