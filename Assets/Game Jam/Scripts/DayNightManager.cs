using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.Universal;

public class DayNightManager : MonoBehaviour
{

    public Light2D globalLight; // Light2D 컴포넌트 참조
    public Color dayColor = Color.white; // 낮 색
    public Color nightColor = new(30 / 255f, 130 / 255f, 255 / 255f); // 밤 색
    public float startTimeToChangeColor;

    void Update() {
        UpdateLightingTransition();
    }

    private void UpdateLightingTransition() {
        float gameTime = GameManager.instance.gameTime;
        bool isDay = GameManager.instance.isDay;
        float dayLength = GameManager.instance.dayLength;
        float nightLength = GameManager.instance.nightLength;

        float lerpFactor;

        if (isDay) {
            if (gameTime > dayLength - startTimeToChangeColor) {
                lerpFactor = (gameTime - dayLength + startTimeToChangeColor) / startTimeToChangeColor;
                globalLight.color = Color.Lerp(dayColor, nightColor, lerpFactor);
            } else {
                globalLight.color = dayColor;
            }
        } else {
            if (gameTime > nightLength - startTimeToChangeColor) {
                lerpFactor = (gameTime - nightLength + startTimeToChangeColor) / startTimeToChangeColor;
                globalLight.color = Color.Lerp(nightColor, dayColor, lerpFactor);
            } else {
                globalLight.color = nightColor;
            }
        }
    }
}
