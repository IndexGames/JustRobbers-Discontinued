using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSwitch : MonoBehaviour
{
     public Animator buttonAnimator;

     public List<AlarmBox> alarmBoxes = new List<AlarmBox>();

     public bool switchState = false;

     bool AlarmStateCache = false;

     private void Start()
     {
          buttonAnimator = GetComponent<Animator>();
     }

     private void Update()
     {
          if (AlarmStateCache == switchState) return;
          ToggleSwitch(switchState);
     }

     public void RegisterParentAlarmBox(AlarmBox parentBox)
     {
          alarmBoxes.Add(parentBox);
     }

     public void ToggleSwitch(bool state)
     {
          switchState = state;
          for (int i = 0; i < alarmBoxes.Count; i++)
          {
               alarmBoxes[i].ToggleAlarm(switchState);
          }

          buttonAnimator.SetBool("Switch", state);
          
          AlarmStateCache = switchState;
     }
}
