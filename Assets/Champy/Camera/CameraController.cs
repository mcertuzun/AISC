using System.Collections;
using UnityEngine;

namespace Champy.Camera
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera Config")] public Transform followedObject;
        public float cameraSpeed = 1f;

        #region TODO

        //TODO: Create a prepared shake types.
        //    public ShakeType shakeType;
        public float smoothTime = 0.1f;
        public float shakeDuration = 0.1f;
        public float shakeAmount = 0.2f;
        public float decreaseFactor = 0.3f;

        public Vector3 originalPos;
        private float _currentShakeDuration;
        private float _currentDistance;

        #endregion

        private void FixedUpdate()
        {
            FollowTheObject(followedObject);
        }

        private void FollowTheObject(Transform objectTransform)
        {
            transform.position =
                Vector3.Lerp(transform.position, objectTransform.position, Time.fixedDeltaTime * smoothTime);
        }

        private IEnumerator Shake()
        {
            originalPos = transform.position;
            _currentShakeDuration = shakeDuration;
            while (_currentShakeDuration > 0)
            {
                originalPos = transform.position;
                transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
                _currentShakeDuration -= Time.deltaTime * decreaseFactor;
                yield return null;
            }

            transform.position = originalPos;
        }
    }
}