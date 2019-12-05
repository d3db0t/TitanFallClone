using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class ShootingSystem : MonoBehaviour
{
    public int Damage;
    public float FireRate;
    public int MagazineSize;
    public bool IsAuto;
    public float Range;
    private Animator WeaponAnimatorController;
    public GameObject Player;
    public Camera Camera;
    private float NextTimeToFire;
    private int CurrentMagazineSize;
    public TextMeshProUGUI AmmoNumber;

    void Start()
    {
        NextTimeToFire           = 0f;
        WeaponAnimatorController = GetComponent<Animator>();
        CurrentMagazineSize      = MagazineSize;
    }

    
    void Update()
    {
        AmmoNumber.text = CurrentMagazineSize.ToString();
        // Run
        if (Player.GetComponent<RigidbodyFirstPersonController>().movementSettings.Running)
        {
            WeaponAnimatorController.SetBool("Run", true);
        }
        else
        {
            WeaponAnimatorController.SetBool("Run", false);
        }

        // Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (CurrentMagazineSize != MagazineSize)
            {
                WeaponAnimatorController.SetBool("Reload", true);
                CurrentMagazineSize = MagazineSize;
            }
        }
        else
        {
            WeaponAnimatorController.SetBool("Reload", false);
        }

        // Shoot
        if (IsAuto)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= NextTimeToFire && !(CurrentMagazineSize <= 0) && !WeaponAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= NextTimeToFire && !(CurrentMagazineSize <= 0) && !WeaponAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
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
    }

    public void Shoot()
    {
        CurrentMagazineSize -= 1;
        RaycastHit hit;
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, Range))
        {
            //Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Enemy")
            {
                if (!hit.transform.GetComponent<EnemyPilotController>().Dead)
                    hit.transform.GetComponent<EnemyPilotController>().TakeDamage(Damage);
            }
        }

    }
}
