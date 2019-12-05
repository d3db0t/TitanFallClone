using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ShootingSystem : MonoBehaviour
{
    public float Damage;
    public float FireRate;
    public int MagazineSize;
    public bool IsAuto;
    public float Range;
    private Animator WeaponAnimatorController;
    public GameObject Player;
    public Camera Camera;
    private float NextTimeToFire;

    void Start()
    {
        NextTimeToFire           = 0f;
        WeaponAnimatorController = GetComponent<Animator>();
    }

    
    void Update()
    {
        // Run
        if (Player.GetComponent<RigidbodyFirstPersonController>().movementSettings.Running)
        {
            WeaponAnimatorController.SetBool("Run", true);
        }
        else
        {
            WeaponAnimatorController.SetBool("Run", false);
        }

        // Shoot
        if (IsAuto)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= NextTimeToFire)
            {
                WeaponAnimatorController.SetBool("Shoot", true);
                NextTimeToFire = Time.time + 1f/FireRate;
                Shoot();
            }
            else
            {
                WeaponAnimatorController.SetBool("Shoot", false);
            }
        }
        else if (!IsAuto)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= NextTimeToFire)
            {
                WeaponAnimatorController.SetBool("Shoot", true);
                NextTimeToFire = Time.time + 1f/FireRate;
                Shoot();
            }
            else
            {
                WeaponAnimatorController.SetBool("Shoot", false);
            }
        }

        // Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            WeaponAnimatorController.SetBool("Reload", true);
        }
        else
        {
            WeaponAnimatorController.SetBool("Reload", false);
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);

        }

    }
}
