

namespace StateMachineAI
{
    [System.Serializable]
    public class Transition 
    {
        public Decision.Decision decision;
        public States.State trueState;
        public States.State falseState;
    }
}