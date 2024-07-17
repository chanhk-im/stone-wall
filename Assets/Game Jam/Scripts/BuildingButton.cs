using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    public GameObject prefab;
    public Image buttonImage;
    public Text costText;

    Building building;
    SpriteRenderer buildingSprite;

    void Awake() {
        building = prefab.GetComponent<Building>();
        buildingSprite = prefab.GetComponent<SpriteRenderer>();
    }

    void Start() {
        costText.text = String.Format("{0:D}G", building.cost);
        buttonImage.sprite = buildingSprite.sprite;
    }
}
