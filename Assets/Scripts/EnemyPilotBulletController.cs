using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPilotBulletController : MonoBehaviour
{
    private GameObject OuterHUD;
    public float Damage;
    void Start()
    {
        OuterHUD = GameObject.FindGameObjectWithTag("OuterHUD");
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.transform.tag == "Player")
        {
            OuterHUD.GetComponent<OuterHUDManager>().TakeDamage(Damage);
        }
    }
}
