using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraSetup : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            transform.GetComponent<CamScript>().enabled = true;
        }
        else
        {
            transform.GetComponent<CamScript>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
