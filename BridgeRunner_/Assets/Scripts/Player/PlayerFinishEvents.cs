using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinishEvents : MonoBehaviour
{
    public static PlayerFinishEvents instance;

    public bool _isFinished;

    private void Start()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
            _isFinished = true;
            PlayerBridgeSpawner.instance.StartSpawningBridge(other.GetComponentInParent<BridgeSpawner>());
        }
    }
}
