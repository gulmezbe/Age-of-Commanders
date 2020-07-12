using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject playerCamera;

    public Image MyBaseHealth;
    public Image EnemyBaseHealth;

    public GameObject SpawnPanel;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    public float maxBaseHealth = 100f;
    public float baseHealth = 100f;
    public float enemyBaseHealth = 100f;  

    void Start()
    {
        if (photonView.IsMine)
        {
            transform.GetComponent<PlayerSoldierSpawn>().enabled = true;
            playerCamera.GetComponent<Camera>().enabled = true;
            playerCamera.GetComponent<AudioListener>().enabled = true;
            playerCamera.GetComponent<CamScript>().enabled = true;

            MyBaseHealth.fillAmount = baseHealth / maxBaseHealth;

            EnemyBaseHealth.fillAmount = enemyBaseHealth / maxBaseHealth;
        }
        else
        {
            transform.GetComponent<PlayerSoldierSpawn>().enabled = false;
            playerCamera.GetComponent<Camera>().enabled = false;
            playerCamera.GetComponent<AudioListener>().enabled = false;
            playerCamera.GetComponent<CamScript>().enabled = false;
            transform.GetComponent<PlayerSetup>().enabled = false;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (baseHealth <= 0f)
            {
                SpawnPanel.SetActive(false);
                LoseScreen.SetActive(true);
            }
            else if (enemyBaseHealth <= 0f)
            {
                SpawnPanel.SetActive(false);
                WinScreen.SetActive(true);
            }
        }        
    }

    [PunRPC]
    public void TakeDamageBase(float damage)
    {
        baseHealth -= damage;

        MyBaseHealth.fillAmount = baseHealth / maxBaseHealth;
    }

    [PunRPC]
    public void GiveDamageBase(float damage)
    {
        enemyBaseHealth -= damage;

        EnemyBaseHealth.fillAmount = enemyBaseHealth / maxBaseHealth;
    }

    public void MainMenuButton()
    {
        if (photonView.IsMine)
        {
            AgeOfCommandersGameManager.instance.LeaveRoom();
        }
    }
}
