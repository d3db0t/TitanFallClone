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
    public GameObject MetalImpactEffect;
    public GameObject ConcreteImpactEffect;
    public ParticleSystem MuzzleFlash;
    public GameObject Flash;
    public bool ToggleFlash;

    void Start()
    {
        NextTimeToFire           = 0f;
        WeaponAnimatorController = GetComponent<Animator>();
        CurrentMagazineSize      = MagazineSize;
        ToggleFlash              = false;
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
            if (CurrentMagazineSize != MagazineSize && !WeaponAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
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
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                WeaponAnimatorController.SetBool("Shoot", false);
            }
        }
        else if (!IsAuto)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= NextTimeToFire && !(CurrentMagazineSize <= 0) && !WeaponAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("Reload") && !WeaponAnimatorController.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
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

        // Flash
        if (Input.GetKeyDown(KeyCode.B))
        {
            Flash.SetActive(!ToggleFlash);
            ToggleFlash = !ToggleFlash;
        }
    }

    public void Shoot()
    {
        MuzzleFlash.Play();
        CurrentMagazineSize -= 1;
        RaycastHit hit;
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, Range))
        {
            //Debug.Log(hit.transform.name);
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
