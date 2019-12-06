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
    private int currentTargetIndex; // this is used for patrolling
    private float time; // this is used for the waiting time when patrolling
    public int patrolWaitTime; // this is the time the pilot will spend waiting when before moving to the other target
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
        currentTargetIndex   = 0;
        time                 = 0;
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
            else
            {
                // if pilot is not activated, it should keep on patrolling between the two targets
                Transform currentTarget = targets[currentTargetIndex];
                distance = currentTarget.position.x - transform.position.x;

                if(distance < 0.1 && distance > -0.1)
                {
                    // if pilot reaches one of the targets, it should wait for a while then switch to the other one
                    if(time > patrolWaitTime)
                    {
                        currentTargetIndex++;
                        if(currentTargetIndex == 2)
                        {
                            currentTargetIndex = 0;
                        }

                        currentTarget = targets[currentTargetIndex];
                        agent.transform.LookAt(currentTarget.position);
                        agent.SetDestination(currentTarget.position);
                        animator.SetBool("Walk", true);

                        time = 0;
                    }
                    else
                    {
                        time += Time.deltaTime;
                        animator.SetBool("Walk", false);
                        animator.SetBool("Idle", true);
                    }
                }
                else
                {
                    agent.transform.LookAt(currentTarget.position);
                    agent.SetDestination(currentTarget.transform.position);
                    animator.SetBool("Walk", true);
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
