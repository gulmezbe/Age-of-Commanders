using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;
    public GameObject SingleMultiPanel;
    public GameObject SinglePlayerPanel;
    public GameObject MultiPlayerPanel;
    public GameObject SearchingGamePanel;
    public GameObject PlayerProfilePanel;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        SingleMultiPanel.SetActive(false);
        SinglePlayerPanel.SetActive(false);
        MultiPlayerPanel.SetActive(false);
        SearchingGamePanel.SetActive(false);
        PlayerProfilePanel.SetActive(false);
    }

    void Update()
    {

    }

    public void SinglePlayer()
    {
        SinglePlayerPanel.SetActive(true);
        SingleMultiPanel.SetActive(false);
    }

    public void MultiPlayer()
    {
        MultiPlayerPanel.SetActive(true);
        SingleMultiPanel.SetActive(false);
    }

    public void VersusEasy()
    {
        
    }

    public void VersusNormal()
    {
        
    }

    public void VersusHard()
    {
        
    }

    public void MainMenuSingle()
    {
        SingleMultiPanel.SetActive(true);
        SinglePlayerPanel.SetActive(false);
    }

    public void MainMenuMulti()
    {
        SingleMultiPanel.SetActive(true);
        MultiPlayerPanel.SetActive(false);
    }

    public void Profile()
    {
        PlayerProfilePanel.SetActive(true);
        MultiPlayerPanel.SetActive(false);
    }

    public void Back()
    {
        MultiPlayerPanel.SetActive(true);
        PlayerProfilePanel.SetActive(false);
    }

    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            ConnectionStatusPanel.SetActive(true);
            EnterGamePanel.SetActive(false);
        }
    }

    public void JoinRandomRoom()
    {
        SearchingGamePanel.SetActive(true);
        MultiPlayerPanel.SetActive(false);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + "Connected to photon server.");
        SingleMultiPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to internet.");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        Hashtable setColor = new Hashtable() { { "color", "red" } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(setColor);

        Debug.Log(PhotonNetwork.NickName + " joined to" + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Hashtable setColor = new Hashtable() { { "color", "blue" } };
        newPlayer.SetCustomProperties(setColor);

        Debug.Log(newPlayer.NickName + " joined to" + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene");
    }

    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room " + Random.Range(0, 10000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
}
