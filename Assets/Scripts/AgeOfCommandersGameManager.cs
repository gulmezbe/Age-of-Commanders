using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class AgeOfCommandersGameManager : MonoBehaviourPunCallbacks
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
    GameObject playerPrefab;

    public static AgeOfCommandersGameManager instance;

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
            if (playerPrefab != null)
            {
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
            }           
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("GameLauncherScene");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

}
