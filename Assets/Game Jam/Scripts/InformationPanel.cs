using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    public Text informationTextObject;
    Weapon weapon;

    void Awake() {
        weapon = GameManager.instance.player.GetComponent<Weapon>();
    }
    
    void Update() {
        int health = GameManager.instance.health;
        int maxHealth = GameManager.instance.maxHealth;
        int healthRegeneration = GameManager.instance.healthRegeneration;
        float damage = weapon.damage;
        int kills = GameManager.instance.kills;

        informationTextObject.text = string.Format("체력: {0} / {1}\n체력 재생력: {2}\n공격력: {3}\n처치 수: {4}", health, maxHealth, healthRegeneration, damage, kills);
    }
}
