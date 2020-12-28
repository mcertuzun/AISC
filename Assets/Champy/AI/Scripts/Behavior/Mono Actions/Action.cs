using UnityEngine;

namespace Champy.AI
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute();
    }
}