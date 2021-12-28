using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hashtable = ExitGames.Client.Photon.Hashtable;

using Photon.Pun;
using Photon.Realtime;

public class PhotonNetworkHandler : MonoBehaviourPunCallbacks
{
     public SO_GameConfiguration gameConfig;

     private void Start()
     {
          PhotonNetwork.ConnectUsingSettings();
          Debug.Log("Connecting Photon");
     }

     public override void OnConnectedToMaster()
     {
          Debug.Log("OnConnectedToMaster() was called by PUN.");
          PhotonNetwork.JoinLobby();
          PhotonNetwork.NickName = "User#" + Random.Range(0, 20);
          //Room ready || 0 - None | 1 - Not ready | 2 - Ready
          StaticUtils.SetCustomNetworkProperty("Room_Ready", 0);

     }

     


     public override void OnJoinedRoom()
     {
          Debug.Log("Joined Room ");

        
     }

}
