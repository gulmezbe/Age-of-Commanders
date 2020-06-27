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
    float soldierDamage = 25f;

    GameObject[] otherSoldiers;
    GameObject[] Players;
    GameObject otherPlayer;
    GameObject myPlayer;

    Vector3 directionOfMovementRed;
    Vector3 directionOfMovementBlue;

    Animator animator;

    void Awake()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in Players)
        {
            if ((string)player.GetComponent<PhotonView>().Owner.CustomProperties["color"] == (string)gameObject.GetComponent<PhotonView>().Owner.CustomProperties["color"])
            {
                myPlayer = player;
            }
            else
            {
                otherPlayer = player;
            }
        }
    }

    void Start()
    {
        float horizontalMovementRed = moveSpeedRed * Time.fixedDeltaTime;
        float horizontalMovementBlue = moveSpeedBlue * Time.fixedDeltaTime;

        directionOfMovementRed = new Vector3(horizontalMovementRed, 0, 0);
        directionOfMovementBlue = new Vector3(horizontalMovementBlue, 0, 0);

        animator = GetComponent<Animator>();

        animator.SetBool("IsWalking", true);
    }

    void FixedUpdate()
    {
        //My base destroyed part
        if(myPlayer.GetComponent<PlayerSetup>().baseHealth <= 0f)
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
                return;
            }
        }
        
        //Enemy left game part
        if(otherPlayer == null)
        {
            animator.SetBool("IsWalking", false);
            return;
        }

        //Enemy base destroyed part
        if(otherPlayer != null && otherPlayer.GetComponent<PlayerSetup>().baseHealth <= 0f)
        {
            animator.SetBool("IsWalking", false);
            return;
        }

        //Dead control part
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

        //Enemy soldier attack part
        otherSoldiers = GameObject.FindGameObjectsWithTag("Soldier");

        foreach (GameObject soldier in otherSoldiers)
        {
            if (!(soldier.GetComponent<SoldierMovementController>().isDead) && !isDead && (string)soldier.GetComponent<PhotonView>().Owner.CustomProperties["color"] != (string)gameObject.GetComponent<PhotonView>().Owner.CustomProperties["color"] && soldier.transform.position.y == gameObject.transform.position.y && Mathf.Abs(soldier.transform.position.x - gameObject.transform.position.x) < soldierRange)
            {
                animator.SetBool("IsWalking", false);
                
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Base"))
                {
                    animator.SetBool("IsAttacking", true);
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.AttackFinished"))
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

        //Base attack part
        if ((string)PhotonNetwork.LocalPlayer.CustomProperties["color"] == "red")
        {
            if(gameObject.transform.position.x > 6.5f)
            {
                animator.SetBool("IsWalking", false);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Base"))
                {
                    animator.SetBool("IsAttacking", true);
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.AttackFinished"))
                {
                    if (gameObject != null && soldierHealth > 0)
                    {
                        otherPlayer.GetComponent<PhotonView>().RPC("TakeDamageBase", RpcTarget.AllBuffered, soldierDamage);
                        myPlayer.GetComponent<PhotonView>().RPC("GiveDamageBase", RpcTarget.AllBuffered, soldierDamage);
                    }

                    animator.SetBool("IsAttacking", false);
                }

                return;
            }

        }
        else
        {
            if (gameObject.transform.position.x < -6.5f)
            {
                animator.SetBool("IsWalking", false);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Base"))
                {
                    animator.SetBool("IsAttacking", true);
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.AttackFinished"))
                {
                    if (gameObject != null && soldierHealth > 0)
                    {
                        otherPlayer.GetComponent<PhotonView>().RPC("TakeDamageBase", RpcTarget.AllBuffered, soldierDamage);
                        myPlayer.GetComponent<PhotonView>().RPC("GiveDamageBase", RpcTarget.AllBuffered, soldierDamage);
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

        //Walking part
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
