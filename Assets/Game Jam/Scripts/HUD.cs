using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType {Money, Time, BuildButton, Health};
    public InfoType type;
    public GameObject contents;

    Text text;
    Slider slider;

    void Awake() {
        switch (type) {
            case InfoType.Money:
            case InfoType.Time:
                text = GetComponent<Text>();
                break;
            case InfoType.Health:
                slider = GetComponent<Slider>();
                break;
        }
        
        // slider = GetComponent<Slider>();
        
    }

    void Start() {
            
    }

    void LateUpdate() {
        switch (type) {
            case InfoType.Money:
                text.text = string.Format("{0:F0}", GameManager.instance.money);
                break;
            case InfoType.Time:
                float time = GameManager.instance.remainTime - GameManager.instance.gameTime;
                string dayOrNight = GameManager.instance.isDay ? "낮" : "밤";
                int min = Mathf.FloorToInt(time / 60);
                int sec = Mathf.FloorToInt(time % 60);
                text.text = string.Format("Day {0} {1} {2:D2}:{3:D2}",GameManager.instance.days, dayOrNight, min, sec);
                break;
            case InfoType.Health:
                int health = GameManager.instance.health;
                int maxHealth = GameManager.instance.maxHealth;
                float healthPer = (float)health / maxHealth;

                slider.value = healthPer;
                break;
        }
    }
}
