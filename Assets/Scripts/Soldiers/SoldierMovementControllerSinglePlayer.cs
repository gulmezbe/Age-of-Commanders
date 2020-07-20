using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovementControllerSinglePlayer : MonoBehaviour
{
    bool isDead = false;
    float moveSpeedPlayer;
    float moveSpeedBot;
    float soldierHealth;
    float soldierRange;
    float soldierDamage;

    GameObject gameManager;

    GameObject[] otherSoldiers;
    GameObject Player;

    Vector3 directionOfMovementPlayer;
    Vector3 directionOfMovementBot;

    Animator animator;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        Player = GameObject.FindGameObjectWithTag("Player");

        moveSpeedPlayer = gameManager.GetComponent<AgeOfCommandersGameManagerSinglePlayer>().soldierMoveSpeed[gameObject.name];
        moveSpeedBot = -1f * gameManager.GetComponent<AgeOfCommandersGameManagerSinglePlayer>().soldierMoveSpeed[gameObject.name];
        soldierHealth = gameManager.GetComponent<AgeOfCommandersGameManagerSinglePlayer>().soldierHealth[gameObject.name];
        soldierRange = gameManager.GetComponent<AgeOfCommandersGameManagerSinglePlayer>().soldierRange[gameObject.name];
        soldierDamage = gameManager.GetComponent<AgeOfCommandersGameManagerSinglePlayer>().soldierDamage[gameObject.name];

        float horizontalMovementPlayer = moveSpeedPlayer * Time.fixedDeltaTime;
        float horizontalMovementBot = moveSpeedBot * Time.fixedDeltaTime;

        directionOfMovementPlayer = new Vector3(horizontalMovementPlayer, 0, 0);
        directionOfMovementBot = new Vector3(horizontalMovementBot, 0, 0);

        animator = GetComponent<Animator>();

        animator.SetBool("IsWalking", true);
    }

    void FixedUpdate()
    {
        //My base destroyed part
        if (Player.GetComponent<PlayerSetupSinglePlayer>().baseHealth <= 0f)
        {
            if (gameObject.transform.localScale.x > 0)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                animator.SetBool("IsWalking", false);
                return;
            }
        }

        //Enemy left game part
        if (Player.GetComponent<PlayerSetupSinglePlayer>().enemyBaseHealth <= 0f)
        {
            if (gameObject.transform.localScale.x > 0)
            {
                animator.SetBool("IsWalking", false);
                return;
            }
            else
            {
                Destroy(gameObject);               
                return;
            }
        }

        //Dead control part
        if (soldierHealth <= 0f)
        {
            isDead = true;
            animator.SetBool("IsDying", true);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.DieFinished"))
            {
                Destroy(gameObject);
            }

            return;
        }

        //Enemy soldier attack part
        otherSoldiers = GameObject.FindGameObjectsWithTag("Soldier");
        bool mySoldierScaleIsPositive = gameObject.transform.localScale.x > 0;

        foreach (GameObject soldier in otherSoldiers)
        {
            bool otherSoldierScaleIsPositive = soldier.transform.localScale.x > 0;
            if (!(soldier.GetComponent<SoldierMovementControllerSinglePlayer>().isDead) && !isDead && mySoldierScaleIsPositive != otherSoldierScaleIsPositive && soldier.transform.position.y == gameObject.transform.position.y && Mathf.Abs(soldier.transform.position.x - gameObject.transform.position.x) < soldierRange)
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
                        soldier.GetComponent<SoldierMovementControllerSinglePlayer>().soldierHealth -= soldierDamage;
                    }

                    animator.SetBool("IsAttacking", false);
                }

                return;
            }
        }

        //Base attack part
        if (gameObject.transform.localScale.x > 0)
        {
            if (gameObject.transform.position.x > 6.5f)
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
                        Player.GetComponent<PlayerSetupSinglePlayer>().GiveDamageBase(soldierDamage);
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
                        Player.GetComponent<PlayerSetupSinglePlayer>().TakeDamageBase(soldierDamage);
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

        if (gameObject.transform.localScale.x > 0)
        {
            gameObject.transform.Translate(directionOfMovementPlayer);

        }
        else
        {
            gameObject.transform.Translate(directionOfMovementBot);
        }
    }
}
