using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType {Money, Time, BuildButton};
    public InfoType type;
    public GameObject contents;

    Text text;
    Slider slider;
    Button button;
    bool isContentsActive;

    void Awake() {
        switch (type) {
            case InfoType.Money:
            case InfoType.Time:
                text = GetComponent<Text>();
                break;
            case InfoType.BuildButton:
                isContentsActive = false;
                break;
        }
        
        // slider = GetComponent<Slider>();
        
    }

    void Start() {
        if (type == InfoType.BuildButton) {
            button.onClick.AddListener(OnClickButton);
            contents.SetActive(false);
        }
            
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
            case InfoType.BuildButton:
                
                break;
        }
    }

    void OnClickButton() {
        isContentsActive = !isContentsActive;
        contents.SetActive(isContentsActive);
        Debug.Log(contents.activeSelf);
    }
}
