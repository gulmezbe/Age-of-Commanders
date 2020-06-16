using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            playerCamera.GetComponent<Camera>().enabled = true;
            playerCamera.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            playerCamera.GetComponent<Camera>().enabled = false;
            playerCamera.GetComponent<AudioListener>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
