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
    public float lastRegenerateTime = 0f;
    [Header("# Player Info")]
    public int money;
    public bool isLive;
    public int health;
    public int healthRegeneration;
    public int maxHealth = 100;
    public int kills;
    public float regenerateSpeed = 1f;

    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;
    public GameObject hud;
    public GameObject uiResult;
    public GameObject laboratoryUI;


    void Awake()
    {
        // if (instance != null) {

        // }
        instance = this;
        isDay = true;
    }

    public void GameStart() {
        isDay = true;
        remainTime = dayLength;
        days = 1;
        money = 1000;
        isLive = true;
        health = maxHealth;
        healthRegeneration = 1;
        StartCoroutine(RegenerateHealth());
    }

    public void GameOver() {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        uiResult.SetActive(true);
        hud.SetActive(false);
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

    public void ActiveLaboratoryUI() {
        laboratoryUI.SetActive(true);
        Stop();
    }

    private IEnumerator RegenerateHealth() {
        while (true) {
            yield return new WaitForSeconds(regenerateSpeed);
            Regenerate();
        }
    }

    private void Regenerate() {
        if (Time.time >= lastRegenerateTime + regenerateSpeed)
        {
            if (health < maxHealth) {
                if (health + healthRegeneration > maxHealth) {
                    health = maxHealth;
                } else {
                    health += healthRegeneration;
                }
                lastRegenerateTime = Time.time;
            }
        }
    }
}
