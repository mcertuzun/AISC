using UnityEngine;

namespace Champy.AI
{
    [CreateAssetMenu (menuName = "Champy2/AI/Actions/Chase Action")]

    public class ChaseAction : StateActions
    {
        public override void Execute(StateManager controller)
        {
            Chase(controller); 
        }
        private void Chase(StateManager controller)
        {
            Debug.Log("Chasing");
            controller.navMeshAgent.destination = controller.chaseTarget.position;
            controller.navMeshAgent.isStopped = false;
        }
    }
}