using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{

     public int maxHealth = 100;
     public int currentHealth = 100;

     public bool isAlive = true;

     public int currentMoney = 0;
     [Range(0, 20)] public int level = 0;
     [Range(0, 20)] public int speed = 0;


     public float thinkingDelayTime = 1f;
     public float minThinkingTime = .2f;
     public float maxThinkingTime = 2f;

     public void DealDamage(int amount)
     {
          currentHealth = currentHealth - amount;
          Debug.Log(amount + "Deal damage of: " + transform.gameObject.name + "  : " + currentHealth + " / " + maxHealth);

          if (currentHealth < 0)
          {
               isAlive = false;
               currentHealth = 0;
          }

     }

     public void Heal(int amount)
     {
          currentHealth += amount;
          if (currentHealth > maxHealth)
          {
               currentHealth = maxHealth;
          }
     }

     public void KillingTarget()
     {

     }

}
