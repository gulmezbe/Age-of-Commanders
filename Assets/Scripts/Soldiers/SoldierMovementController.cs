using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SoldierMovementController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject soldierPrefab;

    public float moveSpeedRed = 3f;
    public float moveSpeedBlue = -3f;
    public float soldierHealth = 100f;
    public float soldierRange = 1f;

    public GameObject[] otherSoldiers;

    Vector3 directionOfMovementRed;
    Vector3 directionOfMovementBlue;

    // Start is called before the first frame update
    void Start()
    {
        float horizontalMovementRed = moveSpeedRed * Time.deltaTime;
        float horizontalMovementBlue = moveSpeedBlue * Time.deltaTime;

        directionOfMovementRed = new Vector3(horizontalMovementRed, 0, 0);
        directionOfMovementBlue = new Vector3(horizontalMovementBlue, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        otherSoldiers = GameObject.FindGameObjectsWithTag("Soldier");

        foreach (GameObject soldier in otherSoldiers)
        {
            if ((string)soldier.GetComponent<PhotonView>().Owner.CustomProperties["color"] != (string)gameObject.GetComponent<PhotonView>().Owner.CustomProperties["color"] && soldier.transform.position.y == gameObject.transform.position.y && Mathf.Abs(soldier.transform.position.x - gameObject.transform.position.x) < soldierRange)
            {
                //PhotonNetwork.Destroy(gameObject);
                break;
            }
            else
            {
                if ((string)PhotonNetwork.LocalPlayer.CustomProperties["color"] == "red")
                {
                    gameObject.transform.Translate(directionOfMovementRed);

                }
                else
                {
                    gameObject.transform.Translate(directionOfMovementBlue);
                }
            }
        }                  
    }
}
