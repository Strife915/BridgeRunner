using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCylinder : MonoBehaviour
{
    public static PlayerCylinder instance;

    public GameObject ridingCylinderPrefab;
    public List<RidingCylinder> cylinders;
    


    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("AddCylinder"))
        {
            PlayerSounds.instance.PlayGatherAudioClip();
            IncrementCylinderVolume(0.1f);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Coin"))
        {
            PlayerSounds.instance.PlayCoinSoundClip();
            PlayerScore.instance.ChangeScore(10);
            other.tag = "Untagged";
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(LevelController.instance.gameActive)
        {
            if (other.CompareTag("Trap"))
            {
                PlayerSounds.instance.PlayDropAudioClip();
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

    
}
