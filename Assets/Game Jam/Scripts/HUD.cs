using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType {Money, Time};
    public InfoType type;

    Text text;
    Slider slider; 

    void Awake() {
        text = GetComponent<Text>();
        slider = GetComponent<Slider>();
    }

    void LateUpdate() {
        switch (type) {
            case InfoType.Money:
                text.text = string.Format("{0:F0}", GameManager.instance.money);
                break;
            case InfoType.Time:
                float time = GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(time / 60);
                int sec = Mathf.FloorToInt(time % 60);
                text.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;

        }
    }
}
