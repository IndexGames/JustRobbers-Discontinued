using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmBox : MonoBehaviour
{
     public List<AlarmBox> linkedAlarmBoxes = new List<AlarmBox>();

     public List<AlarmLamp> linkedAlarmLamps = new List<AlarmLamp>();
     public List<AlarmSpeakers> linkedAlarmSpeakers = new List<AlarmSpeakers>();
     public List<AlarmSwitch> linkedAlarmSwitch = new List<AlarmSwitch>();
     public List<AlarmSensor> linkedAlarmSensor = new List<AlarmSensor>();

     public bool isAlarmArmed = true;
     public bool isAlarmRunning = false;

     bool AlarmStateCache = false;

     private void Start()
     {
          SetConnections();
     }

     private void SetConnections()
     {
          for(int i = 0; i < linkedAlarmSwitch.Count; i++)
          {
               linkedAlarmSwitch[i].RegisterParentAlarmBox(this);
          }

          for (int i = 0; i < linkedAlarmSensor.Count; i++)
          {
               linkedAlarmSensor[i].RegisterAlarmBox(this);
          }
     }

     private void Update()
     {
          if (isAlarmRunning == AlarmStateCache) return;

          ToggleAlarm(isAlarmRunning);
     }

     public void ToggleAlarm(bool state)
     {
          if (!isAlarmArmed) return;

          for (int i = 0; i < linkedAlarmLamps.Count; i++)
          {
               linkedAlarmLamps[i].ToggleAlarm(state);

          }

          if(linkedAlarmBoxes.Count > 0)
          {
               for (int x = 0; x < linkedAlarmBoxes.Count; x++)
               {
                    linkedAlarmBoxes[x].ToggleAlarm(state);
               }
          }

          if(linkedAlarmSensor.Count > 0)
          {
               for (int x = 0; x < linkedAlarmSensor.Count; x++)
               {
                    linkedAlarmSensor[x].sensorTriggered = state;
               }
          }

          if (linkedAlarmSwitch.Count > 0)
          {
               for (int x = 0; x < linkedAlarmSwitch.Count; x++)
               {
                    linkedAlarmSwitch[x].switchState = state;
               }
          }
          Debug.Log("Alarm turned: " + state);
          isAlarmRunning = state;
          AlarmStateCache = state;
     }

     private void OnDrawGizmos()
     {
          for(int i = 0; i < linkedAlarmLamps.Count; i++)
          {
               Gizmos.color = Color.green;

               StaticUtils.DrawLine(gameObject.transform.position, linkedAlarmLamps[i].transform.position, 3f);
              
          }

          for (int i = 0; i < linkedAlarmBoxes.Count; i++)
          {
               Gizmos.color = Color.red;

               StaticUtils.DrawLine(gameObject.transform.position, linkedAlarmBoxes[i].transform.position, 4f);

          }

          for (int i = 0; i < linkedAlarmSpeakers.Count; i++)
          {
               Gizmos.color = Color.cyan;

               StaticUtils.DrawLine(gameObject.transform.position, linkedAlarmSpeakers[i].transform.position, 1f);

          }

          for (int i = 0; i < linkedAlarmSwitch.Count; i++)
          {
               Gizmos.color = Color.blue;

               StaticUtils.DrawLine(gameObject.transform.position, linkedAlarmSwitch[i].transform.position, 3f);

          }

          for (int i = 0; i < linkedAlarmSensor.Count; i++)
          {
               Gizmos.color = Color.yellow;

               StaticUtils.DrawLine(gameObject.transform.position, linkedAlarmSensor[i].transform.position, 3f);

          }
     }
}
