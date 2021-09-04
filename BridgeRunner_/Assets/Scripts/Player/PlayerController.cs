using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float limitX;
    public float runningSpeed;
    public float xSpeed;
    private float _currentRunningSpeed;
    private float _lastTouchedX;

    void Start()
    {
        instance = this;
    }
    void Update()
    {
        if(LevelController.instance == null || !LevelController.instance.gameActive)
        {
            return;
        }
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float newX = 0;
        float touchxDelta = 0;
        if (Input.touchCount > 0 )
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _lastTouchedX = Input.GetTouch(0).position.x;
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchxDelta =2 * (_lastTouchedX-Input.GetTouch(0).position.x) / Screen.width;
            }
            
        }
        else if (Input.GetMouseButton(0))
        {
            touchxDelta = Input.GetAxis("Mouse X");
        }

        newX = transform.position.x + xSpeed * touchxDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limitX, limitX);

        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + _currentRunningSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    public void ChangeSpeed(float value)
    {
        _currentRunningSpeed = value;
    }
}
