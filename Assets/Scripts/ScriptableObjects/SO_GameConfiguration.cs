using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Create GameConfig", order = 1)]
public class SO_GameConfiguration : ScriptableObject
{

     [Header("Weapons")]
     public List<SO_WeaponData> weapons = new List<SO_WeaponData>();

     

     [Header("Game Configuration")]
     public GameObject gameCharacter;


     [Header("Network Configuration")]
     
     public int tickRate = 25;
}
