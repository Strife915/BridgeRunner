using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public static PlayerSounds instance;
    public AudioSource playerAudioSource;
    public AudioClip gatherAudioClip, dropAudioClip, coinSoundClip;
    public float _dropSoundTimer;

    private void Start()
    {
        instance = this;
    }


    public void PlayGatherAudioClip()
    {
        playerAudioSource.PlayOneShot(gatherAudioClip, 0.1f);
    }
    public void PlayDropAudioClip()
    {
        _dropSoundTimer -= Time.deltaTime;
        if (_dropSoundTimer < 0)
        {
            _dropSoundTimer = 0.15f;
            playerAudioSource.PlayOneShot(dropAudioClip, 0.1f);
        }
    }
    public void PlayCoinSoundClip()
    {
        playerAudioSource.PlayOneShot(coinSoundClip, 0.1f);
    }
}
