using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCylinder : MonoBehaviour
{
    public static PlayerCylinder instance;

    public GameObject ridingCylinderPrefab;
    public List<RidingCylinder> cylinders;
    public AudioSource cylinderAudioSource;
    public AudioClip gatherAudioClip, dropAudioClip;
    public float _dropSoundTimer;


    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("AddCylinder"))
        {
            cylinderAudioSource.PlayOneShot(gatherAudioClip, 0.1f);
            IncrementCylinderVolume(0.1f);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(LevelController.instance.gameActive)
        {
            if (other.CompareTag("Trap"))
            {
                PlayDropSound();
                IncrementCylinderVolume(-Time.fixedDeltaTime);
            }
        }
    }
    public void IncrementCylinderVolume(float value)
    {
        if(cylinders.Count == 0)
        {
            if(value > 0)
            {
                CreateCylinder(value);
            }
            else
            {
                if(PlayerFinishEvents.instance._isFinished)
                {
                    LevelController.instance.FinishGame();
                    PlayerBridgeSpawner.instance.SpawningBridge = false;
                }
                else
                {
                    LevelController.instance.GameOver();
                    PlayerAnimatons.instance.Die();
                }
            }
        }
        else
        {
            cylinders[cylinders.Count - 1].IncrementCylinderVolume(value);
        }
    }
    public void CreateCylinder(float value)
    {
        RidingCylinder createdCylinder = Instantiate(ridingCylinderPrefab, transform).GetComponent<RidingCylinder>();
        cylinders.Add(createdCylinder);
        createdCylinder.IncrementCylinderVolume(value);
    }
    public void DestroyCylinder(RidingCylinder cylinder)
    {
        cylinders.Remove(cylinder);
        Destroy(cylinder.gameObject);    }

    public void PlayDropSound()
    {
        _dropSoundTimer -= Time.deltaTime;
        if(_dropSoundTimer < 0)
        {
            _dropSoundTimer = 0.15f;
            cylinderAudioSource.PlayOneShot(dropAudioClip, 0.1f);
        }
    }
}
