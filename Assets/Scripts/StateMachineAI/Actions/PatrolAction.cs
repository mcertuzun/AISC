using StateMachineAI.States;
using UnityEngine;

namespace StateMachineAI.Actions
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class PatrolAction : Action
    {
        public override void Act(StateController controller)
        {
            Patrol (controller);
        }
        private void Patrol(StateController controller)
        {
            controller.navMeshAgent.destination = controller.wayPointList [controller.nextWayPoint].position;
            controller.navMeshAgent.isStopped = false;

            if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending) 
            {
                controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
            }
        }
    }
}