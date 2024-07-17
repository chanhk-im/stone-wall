using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float remainTime;
    public bool isBuilding;
    public bool isDay; // true: day, false: night
    public int days;
    public float dayLength;
    public float nightLength;
    [Header("# Player Info")]
    public int money;

    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;

    void Awake()
    {
        // if (instance != null) {

        // }
        instance = this;
    }

    void Start() {
        isDay = true;
        remainTime = dayLength;
        days = 1;
        money = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        // Debug.Log(gameTime);

        if (gameTime >= remainTime) {
            gameTime = 0;
            if (!isDay) {
                days++;
            }
            isDay = !isDay;
            remainTime = isDay ? dayLength : nightLength;
        }
    }

    public void GetMoney(int gainMoney) {
        money += gainMoney;
    }
}
