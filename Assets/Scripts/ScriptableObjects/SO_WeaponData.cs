using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Create Weapon", order = 1)]
public class SO_WeaponData : ScriptableObject
{
     public string weaponName;
     public GameObject weapon;
     public WeaponCategory weaponCategory;
     public int weaponRounds = 15;
     public float fireRate = 1f;
     public float fireRange = 10f;

     [Range(0, 100)] public int weaponDamage;
   
     public enum WeaponCategory
     {
          Unarmed,
          Knife,
          Pistol,
          Rifle
     }
}
