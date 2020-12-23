using System;
using UnityEngine;

namespace GameEventSystem
{
    public class Player : MonoBehaviour
    {
        
        [SerializeField]
        public GameEvent onHitEvent;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                onHitEvent.value = 20;
                onHitEvent.Raise();    
            }
        }
    }
}
