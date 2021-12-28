using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLamp : MonoBehaviour
{
     public bool isActivated = false;

     Animator lampAnimator;

     private void Start()
     {
          lampAnimator = GetComponent<Animator>();
     }

     public void ToggleAlarm(bool state)
     {
          isActivated = state;
          lampAnimator.SetBool("AlarmActivated", isActivated);
     }

}
