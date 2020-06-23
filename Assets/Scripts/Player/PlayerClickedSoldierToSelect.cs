using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerClickedSoldierToSelect : MonoBehaviour, IPointerDownHandler
{
    public GameObject playerSoldierSelect;

    public void OnPointerDown(PointerEventData eventData)
    {
        playerSoldierSelect.GetComponent<PlayerSoldierSelect>().ClickedSoldierToSelect(eventData);
    }
}
