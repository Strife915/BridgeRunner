using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBridgeSpawner : MonoBehaviour
{
    public static PlayerBridgeSpawner instance;
    public bool SpawningBridge;
    [SerializeField] BridgeSpawner _bridgeSpawner;
    [SerializeField] PlayerCylinder _playerClinder;

    [SerializeField] GameObject _bridgePrefabPeace;
    [SerializeField] float _bridgeSpawnTimer;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if(SpawningBridge)
        {
            PlayerSounds.instance.PlayDropAudioClip();
            _bridgeSpawnTimer -= Time.deltaTime;
            if(_bridgeSpawnTimer < 0)
            {
                _bridgeSpawnTimer = 0.01f;
                _playerClinder.IncrementCylinderVolume(-0.01f);
                GameObject createdPeace = Instantiate(_bridgePrefabPeace,this.transform);
                createdPeace.transform.SetParent(null);
                Vector3 direction = _bridgeSpawner.endReference.transform.position - _bridgeSpawner.startReference.transform.position;
                float distance = direction.magnitude;
                direction = direction.normalized;
                createdPeace.transform.forward = direction;
                float characterDistance = transform.position.z - _bridgeSpawner.startReference.transform.position.z;
                characterDistance = Mathf.Clamp(characterDistance, 0, distance);
                Vector3 newPiecePosition = _bridgeSpawner.startReference.transform.position + direction * characterDistance;
                newPiecePosition.x = transform.position.x;
                createdPeace.transform.position = newPiecePosition;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SpawnBridge"))
        {
            StartSpawningBridge(other.GetComponentInParent<BridgeSpawner>());
        }
        else if(other.CompareTag("StopSpawnBridge"))
        {
            StopSpawningBridge();
            if(PlayerFinishEvents.instance._isFinished)
            {
                LevelController.instance.FinishGame();
            }
        }
    }

    public void StartSpawningBridge(BridgeSpawner spawner)
    {
        _bridgeSpawner = spawner;
        SpawningBridge = true;
    }
    public void StopSpawningBridge()
    {
        SpawningBridge = false;
    }
}
