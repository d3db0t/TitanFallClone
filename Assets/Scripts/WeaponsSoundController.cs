using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponsSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void AR_Shoot(float volume = 1f)
    {
        audioSource.PlayOneShot((AudioClip)Resources.Load("AR_Shoot"));
        audioSource.volume = volume;
    }
    public void AR_Reload(float volume = 1f)
    {
        audioSource.PlayOneShot((AudioClip)Resources.Load("AR_Reload"));
        audioSource.volume = volume;
    }
    public void SN_Shoot(float volume = 1f)
    {
        audioSource.PlayOneShot((AudioClip)Resources.Load("SN_Shoot"));
        audioSource.volume = volume;
    }
    public void SG_Shoot(float volume = 1f)
    {
        audioSource.PlayOneShot((AudioClip)Resources.Load("SG_Shoot"));
        audioSource.volume = volume;
    }
}
