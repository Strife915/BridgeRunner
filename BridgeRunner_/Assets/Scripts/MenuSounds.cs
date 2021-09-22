using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    public static MenuSounds instance;
    public AudioSource menuAudioSource;
    public AudioClip buySound,equipItemAudioClip,unequipItemAudioClip;
    

    private void Start()
    {
        instance = this;
    }

    public void PlayBuySound()
    {
        menuAudioSource.PlayOneShot(buySound, 0.1f);
    }
    public void PlayEquipItemAudioClip()
    {
        menuAudioSource.PlayOneShot(equipItemAudioClip, 0.1f);
    }
    public void PlayUnEquipItemSound()
    {
        menuAudioSource.PlayOneShot(unequipItemAudioClip, 0.1f);
    }

}
