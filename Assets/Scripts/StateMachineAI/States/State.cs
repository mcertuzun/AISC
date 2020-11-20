using StateMachineAI.Actions;
using UnityEngine;

namespace StateMachineAI.States
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class State : ScriptableObject
    {
        public Action[] actions;
        public Transition[] transitions;
        public Color sceneGizmoColor = Color.grey;

        public void UpdateState(StateController controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }
        
        private void DoActions(StateController controller)
        {
            for (int i = 0; i < actions.Length; i++) {
                actions [i].Act (controller);
            }
        }

        private void CheckTransitions(StateController controller)
        {
            for (var i = transitions.Length - 1; i >= 0; i--) 
            {
                var decisionSucceeded = transitions [i].decision.Decide (controller);

                if (decisionSucceeded) 
                    controller.TransitionToState (transitions [i].trueState);
                else
                    controller.TransitionToState (transitions [i].falseState);
                
            }
        }


    }
}