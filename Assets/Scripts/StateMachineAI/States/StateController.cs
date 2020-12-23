using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachineAI.States
{
    public class StateController : MonoBehaviour
    {
        public States.State currentState;
        public States.State remainState;

        public EnemyStats enemyStats;
        public Transform eyes;

        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public List<Transform> wayPointList;
        [HideInInspector] public int nextWayPoint;
        [HideInInspector] public Transform chaseTarget;
        [HideInInspector] public float stateTimeElapsed;

        private bool aiActive;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        public void SetupAi(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
        {
            wayPointList = wayPointsFromTankManager;
            aiActive = aiActivationFromTankManager;
            if (aiActive) 
            {
                navMeshAgent.enabled = true;
            } else 
            {
                navMeshAgent.enabled = false;
            }
        }

        void Update()
        {
            if (!aiActive)
                return;
            currentState.UpdateState(this);
        }

        void OnDrawGizmos()
        {
            if (currentState != null && eyes != null) 
            {
                Gizmos.color = currentState.sceneGizmoColor;
                Gizmos.DrawWireSphere (eyes.position, enemyStats.lookSphereCastRadius);
            }
        }

        public void TransitionToState(States.State nextState)
        {
            if (nextState != remainState) 
            {
                currentState = nextState;
                OnExitState();
            }
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return (stateTimeElapsed >= duration);
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }
    }
}