using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float limitX;

    public float runningSpeed;
    public float xSpeed;
    private float CurrentRunningSpeed;
    

    void Start()
    {
        CurrentRunningSpeed = runningSpeed;
    }
    void Update()
    {
        float newX = 0;
        float touchxDelta = 0;
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchxDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }
        else if(Input.GetMouseButton(0))
        {
            touchxDelta = Input.GetAxis("Mouse X");
        }

        newX = transform.position.x + xSpeed * touchxDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limitX, limitX);

        Vector3 newPosition = new Vector3(newX,transform.position.y, transform.position.z + CurrentRunningSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
}
