using UnityEngine;

namespace Champy.AI
{
    [CreateAssetMenu (menuName = "Champy/AI/Decisions/Look Decision")]

    public class LookCondition : Condition
    {
       
        public override bool CheckCondition(StateManager state)
        {
             bool targetVisible = Look(state);
            return targetVisible;
        }
        private bool Look(StateManager controller)
        {
            Debug.Log("Looking");
            RaycastHit hit;

            Debug.DrawRay(controller.eyes.position,
                controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.green);

            if (Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius,
                    controller.eyes.forward, out hit, controller.enemyStats.lookRange)
                && hit.collider.CompareTag("Player"))
            {
                Debug.Log("looo");

                controller.chaseTarget = hit.transform;
                return true;
            }
            else
            {
                return false;
            }
        }

       
    }
}