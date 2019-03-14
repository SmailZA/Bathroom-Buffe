using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    AudioSource flightAudioSource;
    AudioSource spitAudioSource;

    public List<AudioClip> spitSounds = new List<AudioClip>();

    bool isPlayingFlightSound = false;

    private void Awake()
    {
        flightAudioSource = GetComponent<AudioSource>();
        spitAudioSource = GetComponent<AudioSource>();
    }

    public void ToggleFlyFlightSound()
    {
        
    }

    public void PlayFlyShootSound(FlyController flyController)
    {
        spitAudioSource.clip = spitSounds[Random.Range(0, spitSounds.Count)];
        spitAudioSource.Play();
    }
}
