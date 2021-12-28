using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointGroup : MonoBehaviour
{

     public List<Transform> waypointList = new List<Transform>();

     private void Start()
     {
          for (int i = 0; i < transform.childCount; i++)
          {
               Transform child = transform.GetChild(i);
               waypointList.Add(child);
          }
     }



     private void OnDrawGizmos()
     {
          
               Vector3 lastpos = Vector3.zero;
          for(int i = 0; i < transform.childCount; i++)
          {
               Transform child = transform.GetChild(i);
               

               if (i == 0) lastpos = transform.GetChild(0).position;

               Gizmos.DrawLine(lastpos, child.position);
               lastpos = child.position;
          }
     }
}
