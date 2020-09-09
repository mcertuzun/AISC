using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(fileName = "Input_Data")]
    public class InputData : ScriptableObject
    {
        public bool isHeld;
        public bool isPressed;
        public bool isReleased;
        public bool isStationary;
        public bool displacement;
        public bool displacementMagnitude;
        public Vector2 deltaPosition;
        public float deltaMagnitude;
    }
}