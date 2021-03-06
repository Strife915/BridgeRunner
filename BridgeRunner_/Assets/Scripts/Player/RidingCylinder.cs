using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingCylinder : MonoBehaviour
{
    private bool Filled;
    private float _value;


    public void IncrementCylinderVolume(float value)
    {
        _value += value;
        if(_value > 1)
        {
            float leftValue = _value - 1;
            int cylinderCount = PlayerCylinder.instance.cylinders.Count;
            transform.localPosition = new Vector3(transform.localPosition.x, -0.5f * (cylinderCount - 1) - 0.25f, transform.localPosition.z);
            transform.localScale = new Vector3(0.5f, transform.localScale.y, 0.5f);
            PlayerCylinder.instance.CreateCylinder(leftValue);
        }
        else if(_value < 0)
        {
            PlayerCylinder.instance.DestroyCylinder(this);
        }
        else
        {
            int cylinderCount = PlayerCylinder.instance.cylinders.Count;
            transform.localPosition = new Vector3(transform.localPosition.x, -0.5f * (cylinderCount - 1) - 0.25f * _value, transform.localPosition.z);
            transform.localScale = new Vector3(0.5f * _value, transform.localScale.y, 0.5f * _value);
        }
    }
}
