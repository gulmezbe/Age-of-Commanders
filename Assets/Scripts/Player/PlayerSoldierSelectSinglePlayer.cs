using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSoldierSelectSinglePlayer : MonoBehaviour
{
    public GameObject SoldierSpawnUI;

    public Image SoldierSelectImage1;
    public Image SoldierSelectImage2;

    public Image SoldierImage1;

    public Sprite golemImage;
    public Sprite goblinImage;

    public GameObject HealthBarsPanel;

    bool areSoldiersSelected = false;

    void Awake()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (areSoldiersSelected)
        {
            SoldierSpawnUI.SetActive(true);
            HealthBarsPanel.SetActive(true);
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

        areSoldiersSelected = true;
    }
}
