using UnityEngine;

namespace Champy.AI.Editor
{
    [CreateAssetMenu(menuName = "Editor/Comment Node")]
    public class CommentNode : DrawNode
    {
        public override void DrawWindow(BaseNode b)
        {
            b.comment = GUILayout.TextArea(b.comment, 200);
        }

        public override void DrawCurve(BaseNode b)
        {
        }
    }
}