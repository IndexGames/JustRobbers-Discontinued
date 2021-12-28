using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reward", menuName = "Create Reward", order = 1)]
public class RewardData : ScriptableObject
{
     public float minRewardPrice;
     public float maxRewardPrice;

     [Range(1, 5)] public int epicRewardMultiplier;

     [Range(0, 100)] public int epicRewardChance;
}
