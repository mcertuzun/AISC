using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Champy.AI
{
    public class StateManager : MonoBehaviour
    {
        public float health;
        public State currentState;
        [HideInInspector] public NavMeshAgent navMeshAgent;
        public bool aiActive;
        [HideInInspector] public List<Transform> wayPointList;
        [HideInInspector] public int nextWayPoint;

        [HideInInspector] public Transform chaseTarget;
        public EnemyStats enemyStats;
        public Transform eyes;
        [HideInInspector] public Transform mTransform;

        private void Start()
        {
            mTransform = this.transform;
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            Debug.Log(currentState.name);

            if (currentState != null)
            {
                currentState.UpdateState(this);
                Debug.Log(currentState.name);
            }
        }

        public void SetupAi(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
        {
            wayPointList = wayPointsFromTankManager;
            aiActive = aiActivationFromTankManager;
            if (aiActive)
            {
                navMeshAgent.enabled = true;
            }
            else
            {
                navMeshAgent.enabled = false;
            }
        }
    }
}