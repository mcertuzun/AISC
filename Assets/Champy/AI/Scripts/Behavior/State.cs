using System.Collections.Generic;
using UnityEngine;

namespace Champy.AI
{
    [CreateAssetMenu(fileName = "New State", menuName = "Champy2/AI/State", order = 0)]
    public class State : ScriptableObject
    {
        public StateActions[] onUpdate;
        public StateActions[] onFixed;
        public List<Transition> transitions = new List<Transition>();
        public Color sceneGizmoColor = Color.grey;
        public int idCount;


        public void FixedTick(StateManager states)
        {
            ExecuteActions(states, onFixed);
        }

        public void UpdateState(StateManager states)
        {
            ExecuteActions(states, onUpdate);
            CheckTransitions(states);
        }


        private void ExecuteActions(StateManager states, StateActions[] l)
        {
            for (int i = 0; i < l.Length; i++)
            {
                if (l[i] != null)
                    l[i].Execute(states);
            }
        }

        private void CheckTransitions(StateManager states)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].disable && transitions[i] == null)
                    continue;
                Debug.Log(transitions[i].condition.name);
                if (transitions[i].condition.CheckCondition(states))
                {
                    if (transitions[i].targetState != null)
                    {
                        states.currentState = transitions[i].targetState;
                    }

                    return;
                }
            }
        }

        public Transition AddTransition()
        {
            Transition retVal = new Transition();
            transitions.Add(retVal);
            retVal.id = idCount;
            idCount++;
            return retVal;
        }

        public Transition GetTransition(int id)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].id == id)
                    return transitions[i];
            }

            return null;
        }

        public void RemoveTransition(int id)
        {
            Transition t = GetTransition(id);
            if (t != null)
                transitions.Remove(t);
        }
    }
}