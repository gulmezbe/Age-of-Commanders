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
    float attackRate = 5.0f;
    float attackTimer;

    GameObject[] otherSoldiers;

    Vector3 directionOfMovementRed;
    Vector3 directionOfMovementBlue;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = 0.0f;

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
            Die();
        }

        if (attackTimer < attackRate)
        {
            attackTimer += Time.fixedDeltaTime;
        }

        otherSoldiers = GameObject.FindGameObjectsWithTag("Soldier");

        foreach (GameObject soldier in otherSoldiers)
        {
            if (!isDead && (string)soldier.GetComponent<PhotonView>().Owner.CustomProperties["color"] != (string)gameObject.GetComponent<PhotonView>().Owner.CustomProperties["color"] && soldier.transform.position.y == gameObject.transform.position.y && Mathf.Abs(soldier.transform.position.x - gameObject.transform.position.x) < soldierRange)
            {
                animator.SetBool("IsAttacking", true);

                if (attackTimer > attackRate && (string)PhotonNetwork.LocalPlayer.CustomProperties["color"] == "red")
                {
                    GiveDamage(soldier);
                    attackTimer = 0.0f;
                }
                
                return;
            }
            else
            {
                
            }
        }

        animator.SetBool("IsAttacking", false);

        if ((string)PhotonNetwork.LocalPlayer.CustomProperties["color"] == "red")
        {
            gameObject.transform.Translate(directionOfMovementRed);

        }
        else
        {
            gameObject.transform.Translate(directionOfMovementBlue);
        }
    }

    public void GiveDamage(GameObject soldier)
    {
        StartCoroutine(TakeDamageWaiter());
        if (gameObject != null)
        {
            soldier.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, soldierDamage);
        }        
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        soldierHealth -= damage;
    }

    public void Die()
    {
        isDead = true;
        animator.SetBool("IsDying", true);
        StartCoroutine(DieWaiter());

        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }        
    }

    IEnumerator TakeDamageWaiter()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
    }

    IEnumerator DieWaiter()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
    }
}
