using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingCylinder : MonoBehaviour
{
    private bool Filled;
    private float Value;


    public void IncrementCylinderVolume(float value)
    {
        Value += value;
        if(Value > 1)
        {
            float leftValue = Value - 1;
            int cylinderCount = PlayerCylinder.instance.cylinders.Count;
            transform.localPosition = new Vector3(transform.localPosition.x, -0.5f * (cylinderCount - 1) - 0.25f, transform.localPosition.z);
            transform.localScale = new Vector3(0.5f, transform.localScale.y, 0.5f);
            PlayerCylinder.instance.CreateCylinder(leftValue);
        }
        else if(Value < 0)
        {
            PlayerCylinder.instance.DestroyCylinder(this);
        }
        else
        {
            int cylinderCount = PlayerCylinder.instance.cylinders.Count;
            transform.localPosition = new Vector3(transform.localPosition.x, -0.5f * (cylinderCount - 1) - 0.25f * Value, transform.localPosition.z);
            transform.localScale = new Vector3(0.5f * Value, transform.localScale.y, 0.5f * Value);
        }
    }
}
