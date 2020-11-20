using StateMachineAI.States;

namespace StateMachineAI.Actions
{
    public abstract class Action : UnityEngine.ScriptableObject
    {
        public abstract void Act(StateController controller);
    }
}