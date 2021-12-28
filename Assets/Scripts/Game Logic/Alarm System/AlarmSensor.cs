using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSensor : MonoBehaviour
{
     public Transform laserBase;
     public Transform laser;
     public List<AlarmBox> alarmBoxes = new List<AlarmBox>();
     public Animator sensorAnimator;
     public bool sensorTriggered = false;

     public float laserDistance = 0f;

     private bool cacheSensorTrigger = false;
     
     void Start()
     {
          InitializeLaser();
        
          sensorAnimator = GetComponent<Animator>();
     }

     private void Update()
     {
          if (cacheSensorTrigger == sensorTriggered) return;

          TriggerAlarm(sensorTriggered);


     }

     private void InitializeLaser()
     {
          RaycastHit hit;

          if (Physics.Raycast(laserBase.position, laserBase.TransformDirection(Vector3.forward) * 250f, out hit))
          {
               laserDistance = hit.distance;
               Debug.Log("DISTANCE LASER: " + hit.distance + " object: " + hit.collider.name);
               
               laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, laserDistance);
          }
     }

     public void TriggerAlarm(bool state)
     {
          for (int i = 0; i < alarmBoxes.Count; i++)
          {
               alarmBoxes[i].ToggleAlarm(state);
          }

          sensorAnimator.SetBool("SensorTrigger", state);
          cacheSensorTrigger = state;

     }

     public void RegisterAlarmBox(AlarmBox alarmBox)
     {
          alarmBoxes.Add(alarmBox);
     }

     
}
