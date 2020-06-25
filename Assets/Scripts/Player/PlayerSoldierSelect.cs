using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSoldierSelect : MonoBehaviourPunCallbacks
{
    public GameObject SoldierSpawnUI;

    public Image SoldierSelectImage1;
    public Image SoldierSelectImage2;

    public Image SoldierImage1;

    public Sprite golemImage;
    public Sprite goblinImage;

    GameObject[] SoldierSelectUIs;
    bool areSoldiersSelected = false;

    void Awake()
    {
        if (photonView.IsMine)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SoldierSelectUIs = GameObject.FindGameObjectsWithTag("SoldierSelectUI");

        foreach (GameObject SoldierSelectUI in SoldierSelectUIs)
        {
            if (!(SoldierSelectUI.GetComponent<PlayerSoldierSelect>().areSoldiersSelected))
            {
                return;
            }
        }

        if (photonView.IsMine)
        {
            SoldierSpawnUI.SetActive(true);
            gameObject.SetActive(false);
        }       
    }

    public void ClickedSoldierToSelect(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.tag == "1")
        {
            SoldierImage1.overrideSprite = golemImage;
        }
        else
        {
            SoldierImage1.overrideSprite = goblinImage;
        }
        
        SoldierImage1.tag = eventData.pointerCurrentRaycast.gameObject.tag;
        gameObject.GetComponent<PhotonView>().RPC("allSoldiersSelected", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void allSoldiersSelected()
    {
        areSoldiersSelected = true;
    }
}
