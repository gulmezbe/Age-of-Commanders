using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInformation : MonoBehaviourPunCallbacks
{
    public TMPro.TextMeshProUGUI player_name;
    public TMPro.TextMeshProUGUI rank;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerName(string playerName)
    {
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Player name is empty.");
            return;
        }

        PhotonNetwork.NickName = playerName;
        player_name.text = playerName;
    }

    public void SetPlayerRank()
    {
        rank.text = "1500";
    }
}
