using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBotSoldierSpawn : MonoBehaviour
{
    Dictionary<string, float> soldiersSpawnTimers = new Dictionary<string, float>();
    Dictionary<string, string> soldiers = new Dictionary<string, string>();

    bool isGameStarted = false;

    GameObject Soldier;
    GameObject soldierPrefab;

    string selectedSoldierTag;
    int spawnLane;

    float spawnTimer = 0.0f;
    float selectedSoldierTime;

    void Awake()
    {
        soldiersSpawnTimers.Add("1", 1f);
        soldiersSpawnTimers.Add("2", 2f);

        soldiers.Add("1", "Golem");
        soldiers.Add("2", "Goblin");
    }

    void Update()
    {
        if (isGameStarted)
        {
            if (spawnTimer < 1000f)
            {
                spawnTimer += Time.deltaTime;
            }

            if(spawnTimer > selectedSoldierTime)
            {         
                spawnTimer = 0.0f;

                Random random = new Random();
                spawnLane = Random.Range(1, 6);
                selectedSoldierTag = Random.Range(1, 3).ToString();
                selectedSoldierTime = soldiersSpawnTimers[selectedSoldierTag] + 2.0f;

                soldierPrefab = Resources.Load(soldiers[selectedSoldierTag], typeof(GameObject)) as GameObject;

                soldierPrefab.transform.localScale = new Vector3(-0.3f, 0.3f, 1f);
                if (soldierPrefab != null)
                {
                    Instantiate(soldierPrefab, new Vector3(7, (4f - (((float)spawnLane - 1f) * (9f / 4f))), 0), Quaternion.identity);
                }
            }
        }
        else
        {
            Soldier = GameObject.FindGameObjectWithTag("Soldier");
            if(Soldier != null)
            {
                isGameStarted = true;

                Random random = new Random();
                spawnLane = Random.Range(1, 6);
                selectedSoldierTag = Random.Range(1, 3).ToString();
            }
        }
    }
}
