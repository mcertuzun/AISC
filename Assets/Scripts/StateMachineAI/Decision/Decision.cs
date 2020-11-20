using StateMachineAI.States;
using UnityEngine;

namespace StateMachineAI.Decision
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide (StateController controller);
    }
}