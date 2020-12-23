using UnityEngine;

namespace StateMachineAI.NodeEditor.Nodes
{
    public class StateNode : BaseNode
    {
        public override void Draw()
        {
            innerConnection.Draw();
            outerConnection.Draw();
            GUI.Box(nodeRect, title, defaultStyle);
        }

        public override void Drag(Vector2 delta)
        {
            nodeRect.position += delta;
        }

        public override bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        Debug.Log($"{nodeRect.Contains(e.mousePosition)} and {e.mousePosition}");
        
                        if (nodeRect.Contains(e.mousePosition))
                        {
                            Debug.Log("clicked");
                            isDragged = true;
                            GUI.changed = true;
                        }
                        else
                        {
                            GUI.changed = true;
                        }
                    }
                    break;

                case EventType.MouseUp:
                    isDragged = false;
                    break;

                case EventType.MouseDrag:
                    if (e.button == 0 && isDragged)
                    {
                        Debug.Log("Draggig");
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }

            return false;
        }
        public override void OnClickRemoveNode()
        {
        }


        public StateNode(Vector2 position, float? width, float? height, GUIStyle defaultStyle, GUIStyle selectedStyle) : base(position, width, height, defaultStyle, selectedStyle)
        {
        }
    }
}