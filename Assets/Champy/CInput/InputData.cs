using UnityEngine;

namespace Champy.CInput
{
    public class InputData : MonoBehaviour
    {
        #region Variables

        [Header("Input Data")] public bool isHeld;
        public bool isPressed;
        public bool isReleased;
        public bool isStationary;
        public Vector2 displacementVector;

        public Vector2 deltaPosition;

        [Tooltip("Open Log Mode")] public bool debugLog;
        private Vector2 _deltaPosition, _lastPosition, _firstPos, _secondPos;
        private bool _resetValues;

        #endregion

        #region Methods

        public void Update()
        {
            HandleInputs();
        }

        private void HandleInputs()
        {
#if UNITY_EDITOR
            isPressed = Input.GetMouseButtonDown(0);
            isHeld = Input.GetMouseButton(0);
            isReleased = Input.GetMouseButtonUp(0);
            isStationary = (deltaPosition == Vector2.zero && isHeld);
            deltaPosition = DeltaPosition();
            displacementVector = CalculateDisplacement();

            if (debugLog)
            {
                if (isReleased)
                {
                    Debug.Log($"isReleased ");
                }

                if (isPressed)
                {
                    Debug.Log($"isPressed");
                }
            }


//Mobile input system            
#elif (UNITY_IPHONE || UNITY_ANDROID)
            if (Input.touchCount >= 1)
            {
                Touch touch = Input.GetTouch(0);
                isPressed = touch.phase == TouchPhase.Began;
                isStationary = touch.phase == TouchPhase.Stationary;
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    isHeld = true;
                }

                isReleased = touch.phase == TouchPhase.Ended ? true : false;
                deltaPosition = touch.deltaPosition;
                displacementVector = CalculateDisplacement();
                _resetValues = true;
            }
            else if (_resetValues)
            {
                isPressed = false;
                isStationary = false;
                isHeld = false;
                isReleased = false;
                deltaPosition = Vector2.zero;
                displacementVector = Vector2.zero;
                _resetValues = false;
            }

            Debug.Log($"Touch => displacement: {displacementVector}, delta: {deltaPosition} ");
            Debug.Log($"Editor => displacement: {displacementVector2}, delta: {deltaPosition2}");
#endif
        }

        private Vector2 CalculateDisplacement()
        {
            Vector2 displacement = Vector2.zero;
            if (isPressed)
            {
                _firstPos = Input.mousePosition;
            }

            if (isHeld)
            {
                Vector2 secondPos = Input.mousePosition;
                displacement = secondPos - _firstPos;
            }

            return displacement;
        }

        private Vector2 DeltaPosition()
        {
            Vector2 currentPosition = Input.mousePosition;
            _deltaPosition = currentPosition - _lastPosition;
            _lastPosition = currentPosition;
            return _deltaPosition;
        }

        #endregion
    }
}