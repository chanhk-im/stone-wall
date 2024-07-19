using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Object/BuildingData")]
public class BuildingData : ScriptableObject
{
    public enum BuildingType {Wall, Laboratory, MiningBuilding}
    [Header("# Main Information")]
    public BuildingType buildingType;
    public int id;
    public int maxHealth;
    public int healthRegeneration;
    public int cost;
    public bool isRestrictedCount;
    public int maxCount;
}
