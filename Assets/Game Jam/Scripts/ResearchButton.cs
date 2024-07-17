using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResearchButton : MonoBehaviour
{
    public Image buttonImage;
    public Text costText;
    public Text levelText;
    public Text tooltipTitle;
    public Text tooltipContent;
    [Header("# Building Information")]
    public ResearchData data;
    public int level;

    void Awake() {
        buttonImage.sprite = data.researchIcon;
        tooltipTitle.text = data.researchName;
    }

    void Start() {

    }

    private void LateUpdate() {
        levelText.text = "Lv." + (level + 1);
        costText.text = data.baseCost + (data.isCostValueStatic ? data.cost * (level + 1) : data.costs[level]) + "G";

        tooltipContent.text = data.researchType switch
        {
            ResearchData.ResearchType.WallHealth => string.Format(data.researchDesc, data.isIncreaseValueStatic ? data.increaseValue : data.increaseValues[level]),
            ResearchData.ResearchType.WallHealthRegeneration => string.Format(data.researchDesc,  data.isIncreaseValueStatic ? data.increaseValue : data.increaseValues[level]),
            _ => data.researchDesc,
        };
    }

    public void OnClick() {
        switch (data.researchType) {
            case ResearchData.ResearchType.WallHealth:
                data.researchTarget.GetComponent<Building>().maxHealth += data.isIncreaseValueStatic ? data.increaseValue : data.increaseValues[level];
                foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Building").Where(building => building.GetComponent<Wall>() != null).ToArray()) {
                    wall.GetComponent<Building>().maxHealth += data.isIncreaseValueStatic ? data.increaseValue : data.increaseValues[level];
                    wall.GetComponent<Building>().health += data.isIncreaseValueStatic ? data.increaseValue : data.increaseValues[level];
                }
                break;
            case ResearchData.ResearchType.WallHealthRegeneration:
                data.researchTarget.GetComponent<Wall>().healthRegeneration += data.isIncreaseValueStatic ? data.increaseValue : data.increaseValues[level];
                foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Building").Where(building => building.GetComponent<Wall>() != null).ToArray()) {
                    wall.GetComponent<Wall>().healthRegeneration += data.isIncreaseValueStatic ? data.increaseValue : data.increaseValues[level];
                }
                break;
        }
        if (level + 1 < data.maxLevel)
            level++;
    }
}
