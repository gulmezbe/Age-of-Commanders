using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupSinglePlayer : MonoBehaviour
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
        MyBaseHealth.fillAmount = baseHealth / maxBaseHealth;

        EnemyBaseHealth.fillAmount = enemyBaseHealth / maxBaseHealth;
    }

    void Update()
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

    public void TakeDamageBase(float damage)
    {
        baseHealth -= damage;

        MyBaseHealth.fillAmount = baseHealth / maxBaseHealth;
    }

    public void GiveDamageBase(float damage)
    {
        enemyBaseHealth -= damage;

        EnemyBaseHealth.fillAmount = enemyBaseHealth / maxBaseHealth;
    }

    public void MainMenuButton()
    {
        AgeOfCommandersGameManagerSinglePlayer.instance.LeaveRoom();
    }
}
