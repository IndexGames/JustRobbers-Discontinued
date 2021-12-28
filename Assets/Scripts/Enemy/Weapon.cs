using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{


     public SO_GameConfiguration gameconfig;
     
     public Transform leftHand;

     public SO_WeaponData currentWeapon;
    

     private float fireRateCache = 0f;
    // Start is called before the first frame update
    void Start()
    {
          EquipWeapon(gameconfig.weapons[Random.Range(0, gameconfig.weapons.Count)]);
    }

    // Update is called once per frame
    void Update()
    {
          fireRateCache += Time.deltaTime;
    }

    
     public void EquipWeapon(SO_WeaponData newWeapon)
     {
          Instantiate(newWeapon.weapon, leftHand);
          currentWeapon = newWeapon;
     }

     public bool AttackTarget(Transform target)
     {
         
          if (fireRateCache < currentWeapon.fireRate) return false; // Can't fire 
          float distanceToTarget = Vector3.Distance(transform.position, target.position);
          if (distanceToTarget > currentWeapon.fireRange) return false; // Can't fire outside weapon range

          Statistics targetStats = target.GetComponent<Statistics>();

       
          if (currentWeapon.weaponCategory == SO_WeaponData.WeaponCategory.Unarmed)
          {   
               fireRateCache = 0f;
               return true;
          } else if (currentWeapon.weaponCategory == SO_WeaponData.WeaponCategory.Knife)
          {
               fireRateCache = 0f;
               return true;
          } else if (currentWeapon.weaponCategory == SO_WeaponData.WeaponCategory.Pistol)
          {
               Debug.Log("Attacking base");

               targetStats.DealDamage(currentWeapon.weaponDamage);
               fireRateCache = 0f;
               return true;
          } else if (currentWeapon.weaponCategory == SO_WeaponData.WeaponCategory.Rifle)
          {
               Debug.Log("Attacking base");
               
               targetStats.DealDamage(currentWeapon.weaponDamage);
               fireRateCache = 0f;
               return true;
          }

          return false;
     }

     public IEnumerable Attack(Statistics targetStats)
     {
          Debug.Log("Attacking");
          targetStats.DealDamage(currentWeapon.weaponDamage);

          if (!targetStats.isAlive)
          {
               //Killing sequence
          }

          yield return new WaitForSeconds(currentWeapon.fireRate);
     }

}
