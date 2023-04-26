using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    [SerializeField] private AudioClip nightSound;
    [SerializeField] private AudioClip daySound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayNightSound()
    {
        audioSource.clip = nightSound;
        audioSource.Play();
    }

    public void PlayDaySound()
    {
        audioSource.clip = daySound;
        audioSource.Play();
    }
}
