using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquips : MonoBehaviour
{
    public static PlayerEquips instance;
    public List<GameObject> wearSpots;

    private void Start()
    {
        instance = this;
    }
}
