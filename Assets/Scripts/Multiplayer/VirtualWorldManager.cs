using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class VirtualWorldManager : MonoBehaviourPunCallbacks
{
    
    /*
        //access methods & classes from outside of scipt
    public static VirtualWorldManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
            Instance = this;
        }
     
     */

    public void LeaveRoomdLoadHomeScene()
    {
        PhotonNetwork.LeaveRoom();
    }

    #region Photon Callback Methods

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName+ "joined to:" + " Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion
    
    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel("HomeScene");
    }
}
