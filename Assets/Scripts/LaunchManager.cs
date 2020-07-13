using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
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
        EnterGamePanel.SetActive(false);
        ConnectionStatusPanel.SetActive(false);
        SingleMultiPanel.SetActive(false);
        SinglePlayerPanel.SetActive(false);
        MultiPlayerPanel.SetActive(false);
        SearchingGamePanel.SetActive(false);
        PlayerProfilePanel.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {
            SingleMultiPanel.SetActive(true);
        }
        else
        {
            EnterGamePanel.SetActive(true);
        }      
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
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();
        Hashtable setBot = new Hashtable() { { "bot", "easy" } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(setBot);

        SceneManager.LoadScene("GameSceneSinglePlayer");
    }

    public void VersusNormal()
    {
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();
        Hashtable setBot = new Hashtable() { { "bot", "normal" } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(setBot);

        SceneManager.LoadScene("GameSceneSinglePlayer");
    }

    public void VersusHard()
    {
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();
        Hashtable setBot = new Hashtable() { { "bot", "hard" } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(setBot);

        SceneManager.LoadScene("GameSceneSinglePlayer");
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
        PhotonNetwork.LocalPlayer.CustomProperties.Clear();
        Hashtable setColor = new Hashtable() { { "color", "red" } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(setColor);

        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {       
        Debug.Log(PhotonNetwork.NickName + " joined to" + PhotonNetwork.CurrentRoom.Name + " " + (string)PhotonNetwork.LocalPlayer.CustomProperties["color"] + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        newPlayer.CustomProperties.Clear();
        Hashtable setColor2 = new Hashtable() { { "color", "blue" } };
        newPlayer.SetCustomProperties(setColor2);

        Debug.Log(newPlayer.NickName + " joined to" + PhotonNetwork.CurrentRoom.Name + " " + (string)newPlayer.CustomProperties["color"] + " " + PhotonNetwork.CurrentRoom.PlayerCount);

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
