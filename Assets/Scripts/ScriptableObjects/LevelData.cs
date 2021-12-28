using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Create Level Configuration", order = 1)]
public class LevelData : ScriptableObject
{
     public List<Transform> waypointGroups = new List<Transform>();
     
     public int maxEnemySpawn = 50;

     private void OnDisable()
     {
          waypointGroups.Clear();
     }

}
