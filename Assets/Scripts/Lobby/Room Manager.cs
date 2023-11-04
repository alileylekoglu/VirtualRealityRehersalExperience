using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;
    
    public TextMeshProUGUI OccupancyRateText_Game;
    public TextMeshProUGUI OccupancyRateText_Practice;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
#region UI Callback Methods
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    public void OnEnteredButtonClicked_Practice()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_PRACTICE;
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };
        PhotonNetwork.JoinRandomRoom(customRoomProperties , 0);
    }

    public void OnEnteredButtonClicked_Game()
    {
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_GAME;
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };
        PhotonNetwork.JoinRandomRoom(customRoomProperties , 0);
    }
    #endregion
    
#region Photon Callback Methods

public override void OnJoinRandomFailed(short returnCode, string message)
{
    Debug.Log(message);
    CreateAndJoinRoom();
}

public override void OnConnectedToMaster()
{
    Debug.Log("Connected to servers again");
    PhotonNetwork.JoinLobby();
}

public override void OnCreatedRoom()
{
    Debug.Log("Created room successfully by: " + PhotonNetwork.NickName);
}

public override void OnJoinedRoom()
{
    Debug.Log("The Local player: " + PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name+ " player count:" + PhotonNetwork.CurrentRoom.PlayerCount);

    if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
    {
        object mapType;
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out  mapType))
        {
            Debug.Log("Joined to room with map: " + (string)mapType);
            if ((string)mapType == (MultiplayerVRConstants.MAP_TYPE_VALUE_GAME))
            {
                PhotonNetwork.LoadLevel("GameScene");
            }
            else if ((string)mapType == (MultiplayerVRConstants.MAP_TYPE_VALUE_PRACTICE))
            {
                PhotonNetwork.LoadLevel("PracticeScene");
            }
      
        }
    
}
}

public override void OnPlayerEnteredRoom(Player newPlayer)
{
   Debug.Log(newPlayer.NickName+ "joined to:" + " Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
}

public override void OnRoomListUpdate(List<RoomInfo> roomList)
{
    if (roomList.Count == 0)
    {
        OccupancyRateText_Game.text = 0 + "/" + 10;
        OccupancyRateText_Practice.text = 0 + "/" + 10;
    }

    foreach (RoomInfo room in roomList)
    {
        Debug.Log(room.Name);
        if (room.Name.Contains((MultiplayerVRConstants.MAP_TYPE_VALUE_PRACTICE)))
        {
            Debug.Log("Room is a practice room." +room.PlayerCount);
            OccupancyRateText_Practice.text = room.PlayerCount + "/" + 10;
        }
        else if (room.Name.Contains((MultiplayerVRConstants.MAP_TYPE_VALUE_GAME)))
        {
            Debug.Log("Room is a game room." +room.PlayerCount);
            OccupancyRateText_Game.text = room.PlayerCount + "/" + 10;
        }
        
    }
}

public override void OnJoinedLobby()
{
    Debug.Log("joined to lobby");
}

#endregion

#region Private Methods

private void CreateAndJoinRoom()
{
    string randomRoomName = "Room" +mapType + Random.Range(0, 10000);
    RoomOptions roomOptions = new RoomOptions();
    roomOptions.MaxPlayers = 10;
    
    string[] roomPropsInLobby = {MultiplayerVRConstants.MAP_TYPE_KEY};
    
    ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { {MultiplayerVRConstants.MAP_TYPE_KEY, mapType} };
    roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
    roomOptions.CustomRoomProperties = customRoomProperties;
    
    PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
}


#endregion


}
