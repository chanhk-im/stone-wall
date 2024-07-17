using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Research", menuName = "Scriptable Object/ResearchData")]
public class ResearchData : ScriptableObject
{
    public enum ResearchType {Attack, PlayerHealth, PlayerHealthRegeneration, Income, IncomeTic, WallHealth, WallHealthRegeneration }
    [Header("# Main Information")]
    public ResearchType researchType;
    public int researchId;
    public string researchName;
    [TextArea]
    public string researchDesc;
    public Sprite researchIcon;
    public int maxLevel;
    public int baseCost;
    public GameObject researchTarget;

    [Header("# Level Data")]
    public bool isIncreaseValueStatic;
    public bool isCostValueStatic;
    public int[] costs;
    public int cost;
    public int[] increaseValues;
    public int increaseValue;
}
