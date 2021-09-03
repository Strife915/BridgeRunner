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
            IncrementCylinderVolume(0.1f);
            Destroy(other.gameObject);
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
                //GameOver
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
