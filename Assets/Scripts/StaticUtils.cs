using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hashtable = ExitGames.Client.Photon.Hashtable;

using Photon.Pun;
using Photon.Realtime;

public class StaticUtils : MonoBehaviour
{
     static Dictionary<string, object> listNetworkProperties = new Dictionary<string, object>();

     public static int GetNetworkPropertiesCounting()
     {
          return listNetworkProperties.Count;
     }

     public static void SetCustomNetworkProperty(string key, object value)
     {
          Hashtable hash = new Hashtable();
          hash.Add(key, value);
          PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

          if(!listNetworkProperties.ContainsKey(key))
          {
               listNetworkProperties.Add(key, value);
          } else
          {
               listNetworkProperties[key] = value;
          }
     }

     public static void SetCustomNetworkPropertyToTargetPlayer(Player targetPlayer, string key, object value)
     {
          Hashtable hash = new Hashtable();
          hash.Add(key, value);
          targetPlayer.SetCustomProperties(hash);
     }

     public static object GetCustomNetworkProperty(Player player, string key)
     {
          bool foundedResult = player.CustomProperties.TryGetValue(key, out object result);

          if (foundedResult == false)
          {
               Debug.Log("This network property doesn't exist !");
               return null;
          }

          return result;
     }

     public static object GetCustomNetworkProperty(string key)
     {
          object result = (object)PhotonNetwork.LocalPlayer.CustomProperties[key];

          if(result == null)
          {
               Debug.Log("This network property doesn't exist !"); 
               return null;
          }

          return result;
     }

     public static void DrawLine(Vector3 p1, Vector3 p2, float width)
     {
          int count = 1 + Mathf.CeilToInt(width); // how many lines are needed.
          if (count == 1)
          {
               Gizmos.DrawLine(p1, p2);
          }
          else
          {
               Camera c = Camera.current;
               if (c == null)
               {
                    Debug.LogError("Camera.current is null");
                    return;
               }
               var scp1 = c.WorldToScreenPoint(p1);
               var scp2 = c.WorldToScreenPoint(p2);

               Vector3 v1 = (scp2 - scp1).normalized; // line direction
               Vector3 n = Vector3.Cross(v1, Vector3.forward); // normal vector

               for (int i = 0; i < count; i++)
               {
                    Vector3 o = 0.99f * n * width * ((float)i / (count - 1) - 0.5f);
                    Vector3 origin = c.ScreenToWorldPoint(scp1 + o);
                    Vector3 destiny = c.ScreenToWorldPoint(scp2 + o);
                    Gizmos.DrawLine(origin, destiny);
               }
          }
     }
}
