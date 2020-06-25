using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSoldierSpawn : MonoBehaviourPunCallbacks
{
    GameObject soldierPrefab;

    Dictionary<string, string> soldiers = new Dictionary<string, string>();

    private void Awake()
    {
        soldiers.Add("1", "Golem");
        soldiers.Add("2", "Goblin");
    }

    public void SoldierSpawn(int lane, string tag)
    {
        soldierPrefab = Resources.Load(soldiers[tag], typeof(GameObject)) as GameObject;

        if ((string)PhotonNetwork.LocalPlayer.CustomProperties["color"] == "red")
        {
            soldierPrefab.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
            PhotonNetwork.Instantiate(soldierPrefab.name, new Vector3(-7, (4f - (((float)lane - 1f) * (9f / 4f))), 0), Quaternion.identity);
        }
        else
        {
            soldierPrefab.transform.localScale = new Vector3(-0.3f, 0.3f, 1f);
            PhotonNetwork.Instantiate(soldierPrefab.name, new Vector3(7, (4f - (((float)lane - 1f) * (9f / 4f))), 0), Quaternion.identity);
        }
    }
}
