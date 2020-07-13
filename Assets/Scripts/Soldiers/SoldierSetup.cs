using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class SoldierSetup : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            if (photonView.IsMine)
            {
                transform.GetComponent<SoldierMovementController>().enabled = true;
            }
            else
            {
                transform.GetComponent<SoldierMovementController>().enabled = false;
            }
        }
        else if(SceneManager.GetActiveScene().name == "GameSceneSinglePlayer")
        {
            transform.GetComponent<SoldierMovementControllerSinglePlayer>().enabled = true;
        }
    }
}
