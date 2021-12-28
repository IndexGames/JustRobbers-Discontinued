using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
     public Enemy enemy;

     public float maxAngle = 50;
     public float viewDistance = 20;
     public float resolution = 1;

     public float maxVisionDetectionTimming = 1.5f;
     public float minVisionDetectionTimming = .2f;

     private float visionTiming = 0f;
     private float visionTimingCache = 0f;


     private float targetFollowTimeoutCache = 0f;

     

     private void Start()
     {
          enemy = GetComponent<Enemy>();
     }

     private void Update()
     {
          if (resolution < 1.1) resolution = 1;

          if(visionTimingCache > visionTiming)
          {
               VisionDetection();
               visionTimingCache = 0f;
          }


          //Reset state
          if(targetFollowTimeoutCache > enemy.targetFollowTimeout)
          {
               enemy.brainState = Enemy.BrainState.Patrolling;
               enemy.currentTarget = null;
               targetFollowTimeoutCache = 0;
          }

          visionTimingCache += Time.deltaTime;
          targetFollowTimeoutCache += Time.deltaTime;
     }

     private void VisionDetection()
     {
          
          float quantityOfRays = Mathf.Abs(maxAngle) + Mathf.Abs(-maxAngle);
          float counter = -maxAngle;

          List<GameObject> CurrentObjectDetected = new List<GameObject>();

          for (int i = 0; i < quantityOfRays / resolution; i++)
          {
               RaycastHit hit;

               Vector3 ray = Quaternion.AngleAxis(counter, transform.up) * transform.forward * viewDistance;
              
               if (Physics.Raycast(transform.position + Vector3.up, ray, out hit)) {


                    if (!CurrentObjectDetected.Contains(hit.collider.gameObject)) { 
                         CurrentObjectDetected.Add(hit.collider.gameObject);
                    };

                   // StaticUtils.DrawLine(transform.position, hit.collider.transform.position, 2f);
               }

               counter += resolution;
          }

          visionTiming = Random.Range(minVisionDetectionTimming, maxVisionDetectionTimming);
          SerializeVisionDetection(CurrentObjectDetected);
     }

     private void SerializeVisionDetection(List<GameObject> spotTargets)
     {
          for(int i = 0; i < spotTargets.Count; i++)
          {
               if(spotTargets[i].CompareTag("Player")) {

                    
                    enemy.brainState = Enemy.BrainState.Attacking;
                    enemy.currentTarget = spotTargets[i].transform;
               }
          }
     }

     private void OnDrawGizmos()
     {

          Gizmos.color = Color.blue;

          float quantityOfRays = Mathf.Abs(maxAngle) + Mathf.Abs(-maxAngle);
          float counter = -maxAngle;
          for (int i = 0; i < quantityOfRays / resolution; i++)
          {
               Vector3 rayLine = Quaternion.AngleAxis(counter, transform.up) * transform.forward * viewDistance;

               counter += resolution;
               Gizmos.DrawRay(transform.position + Vector3.up, rayLine);
          }
     }

}
