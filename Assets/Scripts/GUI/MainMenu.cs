using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

using Hashtable = ExitGames.Client.Photon.Hashtable;
public class MainMenu : MonoBehaviourPunCallbacks
{
     [Header("Global Variables")]
     public GameObject characterPrefab;
     [Header("Lobby Variables")]
     public Transform lobby;
     public GameObject serverBarPrefab;
     public Transform serverListContainer;

     private List<GameObject> currentRooms = new List<GameObject>();

     [Header("Room Variables")]
     public Transform room;
     public Transform readyButton;
     public Transform roomPlayerContainer;

     public GameObject playerBarPrefab;
     private List<GameObject> currentPlayers = new List<GameObject>();
     private List<GameObject> currentPhysicalPlayers = new List<GameObject>();
     public List<Transform> roomCharacterSpawns = new List<Transform>();

     public Transform roomStartGame;
     public bool isReady = false;

     public enum MenuType
     {
          None,
          Lobby,
          Room
     }

     public MenuType menuType;
     private MenuType oldMenuType;

     void Awake()
     {
          ResetTransforms();
     }

     private void Update()
     {
          CheckMenus();
     }

     private void CheckMenus()
     {
          if (oldMenuType == menuType) return;

          ResetTransforms();
          Debug.Log("Menu SET");
          if (menuType == MenuType.Lobby)
          {
               lobby.gameObject.SetActive(true);
          }
          else if (menuType == MenuType.Room)
          {
               room.gameObject.SetActive(true);
          }

          oldMenuType = menuType;
     }

     public void ResetTransforms()
     {
          lobby.gameObject.SetActive(false);
          room.gameObject.SetActive(false);
          roomStartGame.GetComponent<Button>().interactable = false;
          roomStartGame.gameObject.SetActive(false);
     }

     public void OnClickCreateRoom()
     {
          RoomOptions roomOptions = new RoomOptions();
          roomOptions.IsVisible = true;
          roomOptions.MaxPlayers = 5;

          PhotonNetwork.JoinOrCreateRoom("Main", roomOptions, TypedLobby.Default);
     }

     public void SetupRoom()
     {
         
          Room room = PhotonNetwork.CurrentRoom;
          
          for (int i = 0; i < room.PlayerCount; i++)
          {
               room.Players.TryGetValue(i + 1, out Player newPlayer);
               Debug.Log("Setup Player: " + newPlayer.NickName);
               SpawnPlayerBar(newPlayer);
               SpawnLobbyCharacter(i);
          } 

     }

     public void SpawnPlayerBar(Player player)
     {
          GameObject playerBar = Instantiate(playerBarPrefab, roomPlayerContainer);

          MenuPlayerBar newPlayerBar = playerBar.GetComponent<MenuPlayerBar>();

          currentPlayers.Add(playerBar);
          newPlayerBar.update(player);
          Debug.Log("playerbar loaded");
     }

     public void SpawnLobbyCharacter(int spawnID)
     {
          Debug.Log("Character id: " + spawnID + " spawned");
          GameObject player = Instantiate(characterPrefab, roomCharacterSpawns[spawnID].position, roomCharacterSpawns[spawnID].rotation);
          currentPhysicalPlayers.Add(player);
     }

     public void ResetServerList()
     {
          for(int i = 0; i < currentRooms.Count; i++)
          {
               currentRooms.RemoveAt(i);
               Destroy(currentRooms[i]);
          }
     }

     public void ResetLobbyCharacter()
     {
          for(int i = 0; i < currentPhysicalPlayers.Count;i++)
          {
               Destroy(currentPhysicalPlayers[i]);
          }

          currentPhysicalPlayers.Clear();
          ResetPlayerList();
     }


     public void ResetPlayerList()
     {
          
          for (int i = 0; i < currentPlayers.Count; i++)
          {
               Destroy(currentPlayers[i]);
          }

          currentPlayers.Clear();
     }

     #region Network Callbacks

     public override void OnJoinedLobby()
     {

          menuType = MenuType.Lobby;

          
     }

     public override void OnRoomListUpdate(List<RoomInfo> roomList)
     {
          Debug.Log("Room Updated ! " + roomList.Count);
          ResetLobbyCharacter();
          for (int i = 0; i < roomList.Count; i++)
          {
               GameObject serverBar = Instantiate(serverBarPrefab, serverListContainer);

               MenuServerBar menuBar = serverBar.GetComponent<MenuServerBar>();
               menuBar.update(roomList[i]);

               currentRooms.Add(serverBar);
          }
     }

     public override void OnPlayerEnteredRoom(Player newPlayer)
     {
          ResetLobbyCharacter();
          SetupRoom();
     }

     public override void OnPlayerLeftRoom(Player otherPlayer)
     {
          ResetLobbyCharacter();
          SetupRoom();
     }


     public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
     {
          if(changedProps.ContainsKey("Room_Ready"))
          {
               if(PhotonNetwork.InRoom)
               {
                    int playerIndex = currentPlayers.FindIndex(x => x.GetComponent<MenuPlayerBar>().player == targetPlayer);

                    if(playerIndex != -1)
                    {
                         int state = (int)StaticUtils.GetCustomNetworkProperty(targetPlayer, "Room_Ready");
              
                         currentPlayers[playerIndex].GetComponent<MenuPlayerBar>().ToogleReady(state);
                    }

                    if(PhotonNetwork.IsMasterClient)
                    {
                         Room currentRoom = PhotonNetwork.CurrentRoom;
                    
                         if (CheckPlayersAreReady(currentRoom))
                         {
                              roomStartGame.GetComponent<Button>().interactable = true;
                              roomStartGame.gameObject.SetActive(true);
                         } else
                         {
                              roomStartGame.GetComponent<Button>().interactable = false;
                              roomStartGame.gameObject.SetActive(false);
                         }
                    } else
                    {
                         roomStartGame.GetComponent<Button>().interactable = false;
                         roomStartGame.gameObject.SetActive(false);
                    }
               }
          }
     }

     private bool CheckPlayersAreReady(Room room)
     {
          bool allReady = false;

          for(int i = 0; i < currentPlayers.Count; i++)
          {
               Player currentPlayer = currentPlayers[i].gameObject.GetComponent<MenuPlayerBar>().player;

               int state = (int)StaticUtils.GetCustomNetworkProperty(currentPlayer, "Room_Ready");

               Debug.Log(currentPlayer.NickName + " STATE: " + state);
               if (state != 2) {
                    allReady = false; 
                    break;
               }
               allReady = true;
          }
          Debug.Log("ALL READY ? " + allReady);
          return allReady;
     }

     public override void OnJoinedRoom()
     {
          Debug.Log("Joined Room ");

          menuType = MenuType.Room;
          StaticUtils.SetCustomNetworkProperty("Room_Ready", 1);

          ResetLobbyCharacter();
          SetupRoom();

     }

     public void ToggleReady()
     {
          isReady = !isReady;

          TMP_Text buttonText = readyButton.GetChild(0).GetComponent<TMP_Text>();
          Image texture = readyButton.GetComponent<Image>();
          if (isReady)
          {
               StaticUtils.SetCustomNetworkProperty("Room_Ready", 2);
               buttonText.text = "Estou pronto !";
               texture.color = Color.green;
          } else
          {
               StaticUtils.SetCustomNetworkProperty("Room_Ready", 1);
               buttonText.text = "Não estou pronto !";
               texture.color = Color.red;
          }
     }

     public override void OnLeftRoom()
     {
          ResetLobbyCharacter();
          StaticUtils.SetCustomNetworkProperty("Room_Ready", 0);
     }

     #endregion

     public void ExitGame()
     {
          Application.Quit();
     }
}
