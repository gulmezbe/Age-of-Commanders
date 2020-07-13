using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class AgeOfCommandersGameManagerSinglePlayer : MonoBehaviourPunCallbacks
{
    public Dictionary<string, float> soldierMoveSpeed = new Dictionary<string, float>()
    {
        {"Golem(Clone)", 1.0f},
        {"Goblin(Clone)", 1.0f}
    };

    public Dictionary<string, float> soldierHealth = new Dictionary<string, float>()
    {
        {"Golem(Clone)", 100f},
        {"Goblin(Clone)", 150f}
    };

    public Dictionary<string, float> soldierRange = new Dictionary<string, float>()
    {
        {"Golem(Clone)", 1.25f},
        {"Goblin(Clone)", 1.25f}
    };

    public Dictionary<string, float> soldierDamage = new Dictionary<string, float>()
    {
        {"Golem(Clone)", 25f},
        {"Goblin(Clone)", 50f}
    };

    [SerializeField]
    GameObject singlePlayerPrefab;

    [SerializeField]
    GameObject easyBotPrefab;

    [SerializeField]
    GameObject normalBotPrefab;

    [SerializeField]
    GameObject hardBotPrefab;

    public static AgeOfCommandersGameManagerSinglePlayer instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (singlePlayerPrefab != null)
            {
                Instantiate(singlePlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }

            if ((string)PhotonNetwork.LocalPlayer.CustomProperties["bot"] == "easy" && easyBotPrefab != null)
            {
                Instantiate(easyBotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }

            if ((string)PhotonNetwork.LocalPlayer.CustomProperties["bot"] == "normal" && normalBotPrefab != null)
            {
                Instantiate(normalBotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }

            if ((string)PhotonNetwork.LocalPlayer.CustomProperties["bot"] == "hard" && hardBotPrefab != null)
            {
                Instantiate(hardBotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }

    public void LeaveRoom()
    {
        SceneManager.LoadScene("GameLauncherScene");
    }

}
