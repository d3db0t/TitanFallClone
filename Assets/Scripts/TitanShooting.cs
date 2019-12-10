using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class TitanShooting : MonoBehaviour
{
    public int Damage;
    public float FireRate;
    //public bool IsAuto;
    public float Range;
    public GameObject Player;
    public Camera Camera;
    private float NextTimeToFire;
    public GameObject MetalImpactEffect;
    public GameObject ConcreteImpactEffect;
    public ParticleSystem MuzzleFlash;
    //public GameObject Flash;
    //public bool ToggleFlash;

    void Start()
    {
        NextTimeToFire = 0f;
    }

    
    void Update()
    {
        // Shoot
         if (Input.GetKey(KeyCode.Mouse0) && Time.time >= NextTimeToFire)
         {
              NextTimeToFire = Time.time + 1f/FireRate;
              Shoot();
         }   
    }

    public void Shoot()
    {
        //MuzzleFlash.Play();
        Instantiate(MuzzleFlash,
                    new Vector3(transform.position.x,transform.position.y, transform.position.z + 3),
                    Quaternion.identity);

        RaycastHit hit;
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Enemy")
            {
                if (!hit.transform.GetComponent<EnemyPilotController>().Dead)
                {
                    hit.transform.GetComponent<EnemyPilotController>().TakeDamage(Damage);
                    GameObject ImpactGO = Instantiate(MetalImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(ImpactGO, 2f);
                }
            }
            else if (hit.transform.tag == "Concrete")
            {
                GameObject ImpactGO = Instantiate(ConcreteImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ImpactGO, 2f);
            }
        }

    }
}
