using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
public class EnemyTitanController : MonoBehaviour
{
    public NavMeshAgent agent { get; private set; }
    public GameObject player;
    private Animator animator;
    private int HP;
    public int MaxHP;
    public bool Dead;
    public bool activated;
    public Image HealthBar;
    public GameObject OuterHUD;
    public GameObject BulletSpawnPointR;
    public GameObject BulletSpawnPointL;
    public GameObject Bullet;
    public ParticleSystem MuzzleFlash;
    private float NextTimeToFire;

    void Start()
    {
        agent                = GetComponent<NavMeshAgent>();
        animator             = GetComponent<Animator>();
        agent.updateRotation = true;
        agent.updatePosition = true;
        Dead                 = false;
        activated            = false;
        HP                   = MaxHP;
        NextTimeToFire       = 0f;
    }


    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(!Dead)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            Vector3 playerposition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            if (distance <= 10)
            {
                animator.SetBool("Walk", false);
                activated = true;
            }

            if (activated)
            {
                animator.SetBool("Walk", false);
                if (agent.stoppingDistance < distance)
                {
                    agent.SetDestination(player.transform.position);
                    animator.SetBool("Shoot", false);
                    animator.SetBool("Walk", true);
                    agent.transform.LookAt(playerposition);
                }
                else
                {
                    animator.SetBool("Walk", false);
                    agent.transform.LookAt(playerposition);
                    if (Time.time >= NextTimeToFire)
                    {
                        animator.SetBool("Shoot", true);
                        Shoot();
                        NextTimeToFire = Time.time + 1f/5;
                    }
                }
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        activated = true;
        HP -= dmg;
        HealthBar.fillAmount = (float) HP / (float) MaxHP;
        if (HP <= 0 && !Dead)
        {
            Die();
        }
    }

    public void Die()
    {
        Dead = true;
        animator.SetBool("Shoot", false);
        animator.SetTrigger("Die");
        GetComponent<NavMeshAgent>().isStopped = true;
        OuterHUD.GetComponent<OuterHUDManager>().IncreaseTitanMeter(100);
    }

    public void Shoot()
    {
        //MuzzleFlash.Play();
        GameObject BulletCloneR = Instantiate(Bullet, BulletSpawnPointR.transform.position, BulletSpawnPointR.transform.rotation);
        GameObject BulletCloneL = Instantiate(Bullet, BulletSpawnPointL.transform.position, BulletSpawnPointL.transform.rotation);
        BulletCloneR.GetComponent<Rigidbody>().AddForce(BulletSpawnPointR.transform.forward * 1700);
        BulletCloneL.GetComponent<Rigidbody>().AddForce(BulletSpawnPointL.transform.forward * 1700);
        Destroy (BulletCloneR, 2);
        Destroy (BulletCloneL, 2);
    }
}
