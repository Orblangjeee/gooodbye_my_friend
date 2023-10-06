using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioClip BtnClick_1;
    public AudioClip BtnClick_2;
    public AudioClip BtnClick_3;
    public AudioClip BtnClick_4;
    public AudioClip BtnClick_5;
    public AudioClip BtnClick_6;
    public AudioClip BtnClick_7;

    public AudioClip openbook_1;
    public AudioClip openbook_2;
    public AudioClip closebook;
    public AudioClip audiomonsterB_Hit;
    public AudioClip audiomonsterA_Hitted;
    public AudioClip audiomonsterB_Hitted;
    public AudioClip audiomonsterdie;

    public Slider mixerUi;
    public AudioSource audioSource;
    public AudioMixer mixer;
    public static AudioManager instance;



    void Awake()
    {
        if (AudioManager.instance == null)
        { AudioManager.instance = this; }

        this.audioSource = GetComponent<AudioSource>();

    }

 

    public void PlaySound(string action)
    {
        switch (action)
        {

            case "BtnClick_1":
                audioSource.clip = BtnClick_1;
                break;

            case "BtnClick_2":
                audioSource.clip = BtnClick_2;
                break;

            case "BtnClick_3":
                audioSource.clip = BtnClick_3;
                break;

            case "BtnClick_4":
                audioSource.clip = BtnClick_4;
                break;
            case "BtnClick_5":
                audioSource.clip = BtnClick_5;
                break;
            case "BtnClick_6":
                audioSource.clip = BtnClick_6;
                break;
            case "BtnClick_7":
                audioSource.clip = BtnClick_7;
                break;


        }
        audioSource.Play();
    }

    public void EffectSound(string action)
    {
        switch (action)
        {
            case "openbook_1":
                audioSource.clip = openbook_1;
                break;

            case "openbook_2":
                audioSource.clip = openbook_2;
                break;
            case "closebook":
                audioSource.clip = closebook;
                break;
            case "audiomonsterB_Hit":
                audioSource.clip = audiomonsterB_Hit;
                break;
            case "audiomonsterA_Hitted":
                audioSource.clip = audiomonsterA_Hitted;
                break;
            case "audiomonsterB_Hitted":
                audioSource.clip = audiomonsterB_Hitted;
                break;
            case "MonsterDie":
                audioSource.clip = audiomonsterdie;
                break;
        }
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void BGMSoundVolume(float vol)
    {
        mixer.SetFloat("BGMVolume", Mathf.Log10(vol) * 20);
    }

    public void EffectSoundVolume(float vol)
    {
        mixer.SetFloat("EffectVolume", Mathf.Log10(vol) * 20);
    }

    
}
