using UnityEngine;

namespace Champy.AI
{
    public abstract class Condition : ScriptableObject

    {
        public string description;
        public abstract bool CheckCondition(StateManager state);
    }
}