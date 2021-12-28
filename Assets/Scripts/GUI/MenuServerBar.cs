using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;
public class MenuServerBar : MonoBehaviour
{

     public RoomInfo room;

     public TMP_Text serverName;
     public TMP_Text currentPlayerCount;

     public Button serverJoinButton;

     public Color defaultColor;
     public Color uninteractableColor;
     // Start is called before the first frame update
     public void update(RoomInfo newRoom)
     {
          serverName = null;
          currentPlayerCount = null;

          if (newRoom.PlayerCount == newRoom.MaxPlayers)
          {
               serverJoinButton.interactable = false;
          } else
          {
               serverJoinButton.interactable = true;
       
          }

          room = newRoom;

          serverName.text = room.Name;
          currentPlayerCount.text = room.PlayerCount + "/" + room.MaxPlayers;
     }

     public void JoinRoom()
     {
          PhotonNetwork.JoinRoom(room.Name);
     }
}
