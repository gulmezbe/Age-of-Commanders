using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SoldierMovementController : MonoBehaviourPunCallbacks
{
    bool isDead = false;
    float moveSpeedRed = 1.0f;
    float moveSpeedBlue = -1.0f;
    float soldierHealth = 100f;
    float soldierRange = 1.5f;
    float soldierDamage = 100f;

    GameObject[] otherSoldiers;

    Vector3 directionOfMovementRed;
    Vector3 directionOfMovementBlue;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        float horizontalMovementRed = moveSpeedRed * Time.fixedDeltaTime;
        float horizontalMovementBlue = moveSpeedBlue * Time.fixedDeltaTime;

        directionOfMovementRed = new Vector3(horizontalMovementRed, 0, 0);
        directionOfMovementBlue = new Vector3(horizontalMovementBlue, 0, 0);

        animator = GetComponent<Animator>();

        animator.SetBool("IsWalking", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (soldierHealth <= 0f)
        {
            gameObject.GetComponent<PhotonView>().RPC("ChangeDie", RpcTarget.AllBuffered);
            animator.SetBool("IsDying", true);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.DieFinished") && photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }

            return;
        }

        otherSoldiers = GameObject.FindGameObjectsWithTag("Soldier");

        foreach (GameObject soldier in otherSoldiers)
        {
            if (!(soldier.GetComponent<SoldierMovementController>().isDead) && !isDead && (string)soldier.GetComponent<PhotonView>().Owner.CustomProperties["color"] != (string)gameObject.GetComponent<PhotonView>().Owner.CustomProperties["color"] && soldier.transform.position.y == gameObject.transform.position.y && Mathf.Abs(soldier.transform.position.x - gameObject.transform.position.x) < soldierRange)
            {
                animator.SetBool("IsWalking", false);
                
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Base") && (string)PhotonNetwork.LocalPlayer.CustomProperties["color"] == "red")
                {
                    animator.SetBool("IsAttacking", true);
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.AttackFinished") && (string)PhotonNetwork.LocalPlayer.CustomProperties["color"] == "red")
                {
                    if (gameObject != null && soldierHealth > 0)
                    {
                        soldier.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, soldierDamage);
                    }
                        
                    animator.SetBool("IsAttacking", false);
                }

                return;
            }
        }

        if (animator.GetBool("IsAttacking"))
        {
            animator.SetBool("IsAttacking", false);
        }

        animator.SetBool("IsWalking", true);

        if ((string)PhotonNetwork.LocalPlayer.CustomProperties["color"] == "red")
        {
            gameObject.transform.Translate(directionOfMovementRed);

        }
        else
        {
            gameObject.transform.Translate(directionOfMovementBlue);
        }
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        soldierHealth -= damage;
    }

    [PunRPC]
    public void ChangeDie()
    {
        isDead = true;
    }
}
