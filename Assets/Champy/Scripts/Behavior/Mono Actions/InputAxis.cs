using UnityEngine;

namespace Champy.AI
{
    [CreateAssetMenu(menuName = "Inputs/Axis")]
    public class InputAxis : Action
    {
        public string targetString;
        public float value;

        public override void Execute()
        {
            value = Input.GetAxis(targetString);
        }
    }
}