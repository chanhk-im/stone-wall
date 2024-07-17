using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public bool isBuilding;
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

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        // Debug.Log(gameTime);
    }

    public void GetMoney(int gainMoney) {
        money += gainMoney;
    }
}
