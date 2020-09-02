using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        //Game events for UI section
        public static event Action CanvasManager;
        // Start is called before the first frame update
        void Start()
        {
            CanvasManager?.Invoke();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
