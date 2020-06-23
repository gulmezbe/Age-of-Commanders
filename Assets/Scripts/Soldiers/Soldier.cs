using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Soldier : MonoBehaviourPunCallbacks
{
    public float health;
    public float attack;
    public float range;
    public string soldierName;
    public Dictionary<string, float> multipliers = new Dictionary<string, float>();
    

    void TakeDamage()
    {

    }

    void Hit()
    {

    }
}
