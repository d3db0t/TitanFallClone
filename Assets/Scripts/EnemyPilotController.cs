using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
public class EnemyPilotController : MonoBehaviour
{
    public NavMeshAgent agent { get; private set; }            
    public Transform[] targets; // empty objects (for patrolling)
    public GameObject player;                                  
    private Animator animator;
    private int HP;
    public int MaxHP;
    public bool Dead;
    public bool activated;
    public Image HealthBar;
    public GameObject OuterHUD;
    
    void Start()
    {
        agent                = GetComponent<NavMeshAgent>();
        animator             = GetComponent<Animator>();
        agent.updateRotation = true;
        agent.updatePosition = true;
        Dead                 = false;
        activated            = false;
        HP                   = MaxHP;
    }

    
    void Update()
    {
        if(!Dead)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            Vector3 playerposition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            if (distance <= 10)
            {
                activated = true;
            }
            
            if (activated)
            {
                if (agent.stoppingDistance < distance)
                {
                    agent.SetDestination(player.transform.position);
                    animator.SetBool("StepForward", true);
                }
                else
                {
                    animator.SetBool("StepForward", false);
                    agent.transform.LookAt(playerposition);
                    animator.SetBool("Shoot", true);
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
        else if (!Dead && Random.value > 0.8) // 20% chance HitReaction
        {
            animator.SetTrigger("HitReaction");
        }
    }

    public void Die()
    {
        Dead = true;
        animator.SetBool("Shoot", false);
        animator.ResetTrigger("HitReaction");
        animator.SetTrigger("Die");
        GetComponent<NavMeshAgent>().isStopped = true;
        OuterHUD.GetComponent<OuterHUDManager>().IncreaseTitanMeter(100);
    }
}
