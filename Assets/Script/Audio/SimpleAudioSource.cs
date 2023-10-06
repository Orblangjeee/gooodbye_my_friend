using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudioSource : MonoBehaviour
{
    public AudioClip audioclip;
    public AudioSource audioSource;
    
    public void PlayEffect()
    {
        audioSource.clip = audioclip;
        audioSource.Play();
    }
}
