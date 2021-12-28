using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MenuManager : MonoBehaviourPunCallbacks
{

     public MainMenu mainMenu;
    // Start is called before the first frame update
    void Awake()
    {
          mainMenu.ResetTransforms();
    }

     public override void OnJoinedLobby()
     {
          
          mainMenu.menuType = MainMenu.MenuType.Lobby;
     }
}
