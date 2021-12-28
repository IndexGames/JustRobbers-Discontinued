using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class MenuPlayerBar : MonoBehaviour
{
     public Player player;

     public TMP_Text playerNameText;
     public Transform statusPlayer;
     public bool isReady = false;

     public bool isSamePlayer(Player target)
     {
          return player == target;
     }

     public void update(Player newPlayer)
     {
          player = newPlayer;

          if (player == null) return;
          if (player.NickName == null)
          {
               playerNameText.text = "no name bitch"; return;
          }
          playerNameText.text = player.NickName;
     }


     public void ToogleReady(int state)
     {
          
          Image buttonColor = statusPlayer.GetComponent<Image>();
          TMP_Text buttonText = statusPlayer.GetChild(0).GetComponent<TMP_Text>();
          if (state == 2)
          {
               isReady = true;
               buttonColor.color = Color.green;
               buttonText.text = "ESTÁ PRONTO !";
          } else
          {
               isReady = false;
               buttonColor.color = Color.red;
               buttonText.text = "NÃO ESTÁ PRONTO !";
          }

          
     }

}
