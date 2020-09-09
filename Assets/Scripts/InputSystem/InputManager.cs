using InputSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputData inputData;
    private Vector2 _currentPoint;
    private Vector2 _firstPoint;
    // Update is called once per frame
    public void Update()
    {
        WriteInputData();
    }

    public void WriteInputData()
    {
// #if UNITY_EDITOR
//         inputData.isPressed = Input.GetMouseButtonDown(0);
//         inputData.isHeld = Input.GetMouseButton(0);
//         inputData.isReleased = Input.GetMouseButtonUp(0);
//       
//
// #else //Mobile input
// #endif

      
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputData.isPressed = touch.phase == TouchPhase.Began;
            inputData.isStationary = touch.phase == TouchPhase.Stationary;
            inputData.isHeld = touch.phase == TouchPhase.Moved;
            inputData.isReleased = touch.phase == TouchPhase.Ended;
            
           
            
            inputData.deltaPosition = touch.deltaPosition;
            inputData.deltaMagnitude = touch.deltaPosition.magnitude;
            Debug.Log($"Mobile is ready, delta magnitude: {inputData.deltaPosition} ");

        }

        if (inputData.isPressed)
        {
            _firstPoint = Input.mousePosition;
            
        }
        else if (inputData.isHeld)
        {
            _currentPoint = Input.mousePosition;
            MovementController();
        }

    }

    private void MovementController()
    {
        Debug.Log($"delta magnitude: {(_currentPoint - _firstPoint)} ");
    }


}