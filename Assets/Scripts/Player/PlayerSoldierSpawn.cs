using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSoldierSpawn : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject golemPrefab;

    [SerializeField]
    GameObject goblinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //if((string)PhotonNetwork.LocalPlayer.CustomProperties["color"] == "red")
        //{
        //    golemPrefab.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        //    PhotonNetwork.Instantiate(golemPrefab.name, new Vector3(-7, -5, 0), Quaternion.identity);

        //    goblinPrefab.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        //    PhotonNetwork.Instantiate(goblinPrefab.name, new Vector3(-7, 4, 0), Quaternion.identity);
        //}
        //else
        //{
        //    golemPrefab.transform.localScale = new Vector3(-0.3f, 0.3f, 1f);
        //    PhotonNetwork.Instantiate(golemPrefab.name, new Vector3(7, -5, 0), Quaternion.identity);

        //    goblinPrefab.transform.localScale = new Vector3(-0.3f, 0.3f, 1f);
        //    PhotonNetwork.Instantiate(goblinPrefab.name, new Vector3(7, 4, 0), Quaternion.identity);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
