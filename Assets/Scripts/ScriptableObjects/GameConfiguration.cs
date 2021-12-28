using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Create GameConfig", order = 1)]
public class GameConfiguration : ScriptableObject
{

     [Header("Weapons")]
     public List<WeaponData> weapons = new List<WeaponData>();

     

     [Header("Game Configuration")]
     public GameObject gameCharacter;


     [Header("Network Configuration")]
     
     public int tickRate = 25;
}
