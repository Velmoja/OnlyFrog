using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private AudioSource audioSrc;
    private float musicVolume = 1f;
    private AudioController mController;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        mController = FindObjectOfType<AudioController>();
    }

    private void Update()
    {
        audioSrc.volume = musicVolume;
    }


    public void SetVolume(float vol)
    {
        musicVolume = vol;
        mController.SaveSoundSettings();
    }

}
