using System;
using UnityEngine;
using UnityEngine.UI;

namespace InputSystem
{
    public class InputController : MonoBehaviour
    {
        private Vector2 _currentPoint;
        private Vector2 _firstPoint;

        public InputData inputData;
        public GameObject joystick;
        public GameObject joystickBorder;

        [Header("Joystick Objects")] [SerializeField]
        private bool joystickImage;

        [SerializeField] [Range(0f, 10f)] private float maxAcceleration = 10;
        [SerializeField] [Range(0f, 100f)] private float maxSpeed = 10f;
        public GameObject player;
        [SerializeField] [Range(0f, 1f)] private float playerDisplacementSpeed = 0.009f;

        public bool logControlMode;

        private void Update()
        {
            if (inputData.isPressed)
            {
                _firstPoint = Input.mousePosition;
                if (logControlMode)
                {
                    Debug.Log("Pressed");
                }
            }
            else if (inputData.isHeld)
            {
                _currentPoint = Input.mousePosition;
                MovementController();
            }
            else if (inputData.isReleased)
            {
                JoyStickImageSwitch(null, false);
                Debug.Log("Released");
            }
            else if (inputData.isStationary)
            {
                Debug.Log("Stationary");
            }
        }

        

        public void MovementController()
        {
            var displacement = _currentPoint - _firstPoint;
            displacement = Vector2.ClampMagnitude(displacement, 20);
            JoyStickManager(displacement);
        }


        private void JoyStickManager(Vector2 displacement)
        {
            JoyStickImageSwitch(_firstPoint, true);
            joystick.GetComponent<Image>().transform.localPosition = displacement;
        }

        private void JoyStickImageSwitch(Vector2? point, bool val)
        {
            if (joystickImage)
            {
                if (point.HasValue) joystick.transform.parent.position = point.Value;
                joystick.GetComponent<Image>().enabled = val;
                joystickBorder.GetComponent<Image>().enabled = val;
            }
        }
    }
}