using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class AgeOfCommandersGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject playerPrefab;

    public static AgeOfCommandersGameManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Hashtable setBaseHealth = new Hashtable() { { "baseHealth", 100 } };
        PhotonNetwork.SetPlayerCustomProperties(setBaseHealth);

        if (PhotonNetwork.IsConnected)
        {
            if (playerPrefab != null)
            {
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, 0), Quaternion.identity);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }








}
