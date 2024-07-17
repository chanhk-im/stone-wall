using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool isLive;
    public int health;
    public int maxHealth = 100;

    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;
    public GameObject uiResult;

    void Awake()
    {
        // if (instance != null) {

        // }
        instance = this;
    }

    public void GameStart() {
        isDay = true;
        remainTime = dayLength;
        days = 1;
        money = 1000;
        isLive = true;
        health = maxHealth;
    }

    public void GameOver() {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.SetActive(true);
        Stop();
    }

    public void GameRetry() {
        Resume();
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLive) 
            return;

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

    public void Stop() {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume() {
        isLive = true;
        Time.timeScale = 1;
    }
}
