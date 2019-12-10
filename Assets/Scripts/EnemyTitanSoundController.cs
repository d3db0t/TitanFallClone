using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyTitanSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void FootStep(float volume = 1f)
    {
        audioSource.PlayOneShot((AudioClip)Resources.Load("footstep"));
        audioSource.volume = volume;
    }

    public void SmallCanon(float volume = 1f)
    {
        audioSource.PlayOneShot((AudioClip)Resources.Load("SmallCanon"));
        audioSource.volume = volume;
    }

    public void BigCanon(float volume = 1f)
    {
        audioSource.PlayOneShot((AudioClip)Resources.Load("BigCanon"));
        audioSource.volume = volume;
    }

}
