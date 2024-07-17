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
        int increaseValueResult = data.isIncreaseValueStatic ? data.increaseValue : data.increaseValues[level];
        levelText.text = "Lv." + (level + 1);
        costText.text = data.baseCost + (data.isCostValueStatic ? data.cost * (level + 1) : data.costs[level]) + "G";

        tooltipContent.text = data.researchType switch
        {
            ResearchData.ResearchType.WallHealth => string.Format(data.researchDesc, increaseValueResult),
            ResearchData.ResearchType.WallHealthRegeneration => string.Format(data.researchDesc, increaseValueResult),
            ResearchData.ResearchType.Attack => string.Format(data.researchDesc, increaseValueResult),
            ResearchData.ResearchType.Income => string.Format(data.researchDesc, increaseValueResult),
            _ => data.researchDesc,
        };
    }

    public void OnClick() {
        if (GameManager.instance.money < data.baseCost + (data.isCostValueStatic ? data.cost * (level + 1) : data.costs[level])) {
            Debug.Log("돈없다");
            return;
        }
        GameManager.instance.money -= data.baseCost + (data.isCostValueStatic ? data.cost * (level + 1) : data.costs[level]);
        int increaseValueResult = data.isIncreaseValueStatic ? data.increaseValue : data.increaseValues[level];
        switch (data.researchType) {
            case ResearchData.ResearchType.WallHealth:
                data.researchTarget.GetComponent<Building>().maxHealth += increaseValueResult;
                foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Building").Where(building => building.GetComponent<Wall>() != null).ToArray()) {
                    wall.GetComponent<Building>().maxHealth += increaseValueResult;
                    wall.GetComponent<Building>().health += increaseValueResult;
                }
                break;
            case ResearchData.ResearchType.WallHealthRegeneration:
                data.researchTarget.GetComponent<Wall>().healthRegeneration += increaseValueResult;
                foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Building").Where(building => building.GetComponent<Wall>() != null).ToArray()) {
                    wall.GetComponent<Wall>().healthRegeneration += increaseValueResult;
                }
                break;
            case ResearchData.ResearchType.Attack:
                GameManager.instance.player.GetComponent<Weapon>().damage += increaseValueResult;
                break;
            case ResearchData.ResearchType.Income:
                data.researchTarget.GetComponent<MiningBuilding>().income += increaseValueResult;
                foreach (GameObject mining in GameObject.FindGameObjectsWithTag("Building").Where(building => building.GetComponent<MiningBuilding>() != null).ToArray()) {
                    mining.GetComponent<MiningBuilding>().income += increaseValueResult;
                }
                break;
        }
        if (level + 1 < data.maxLevel)
            level++;
    }
}
