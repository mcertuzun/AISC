using System;
using System.Collections.Generic;
using StateMachineAI.NodeEditor.Nodes;
using UnityEditor;
using UnityEngine;

namespace StateMachineAI.NodeEditor
{
    public class NodeEditor : EditorWindow
    {
        private Rect editorRect;

        private List<BaseNode> nodeList;
        private List<Connection> connectionList;

        private GUIStyle nodeStyle;
        private GUIStyle selectedNodeStyle;
        private GUIStyle inPointStyle;
        private GUIStyle outPointStyle;
        private Vector2 drag;
        

        
        private BaseNode.ConnectionPoint selectedInnerPoint;
        private BaseNode.ConnectionPoint selectedOuterPoint;

        

        [MenuItem("Window/Node Editor")]
        private static void OpenWindow()
        {
            NodeEditor editorWindow = GetWindow<NodeEditor>();
            editorWindow.titleContent = new GUIContent("Node Editor");
        }

        private void OnEnable()
        {
            SetupStyle();
        }

        private void SetupStyle()
        {
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
            selectedNodeStyle = new GUIStyle();
            selectedNodeStyle.normal.background =
                EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
            selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

            inPointStyle = new GUIStyle();
            inPointStyle.normal.background =
                EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
            inPointStyle.active.background =
                EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
            inPointStyle.border = new RectOffset(4, 4, 12, 12);

            outPointStyle = new GUIStyle();
            outPointStyle.normal.background =
                EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
            outPointStyle.active.background =
                EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
            outPointStyle.border = new RectOffset(4, 4, 12, 12);
        }

        private void OnGUI()
        {
            ProcessEvents(Event.current);
            ProcessNodeEvents(Event.current);
            DrawConnections();
            DrawNodes();
        }

        private void DrawConnections()
        {
            if (connectionList != null)
            {
                for (var i = connectionList.Count - 1 ; i >= 0; i--)
                {
                    connectionList[i].Draw();
                }
            }
        }

        private void ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 1)
                    {
                        OnClickRightButton(e.mousePosition);
                    }
                    break;
            }
        }

        private void ProcessNodeEvents(Event e)
        {
            if (nodeList == null) return;
            for (int i = nodeList.Count - 1; i >= 0; i--)
            {
                var guiChanged = nodeList[i].ProcessEvents(e);
                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }

        private void OnClickRightButton(Vector2 mousePosition)
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Add a new node"), false, () => OnClickAddNode(mousePosition));
            genericMenu.ShowAsContext();
        }

        private void OnClickAddNode(Vector2 mousePosition)
        {
            if (nodeList == null)
            {
                nodeList = new List<BaseNode>();
            }

            nodeList.Add(new StateNode(mousePosition, null, null, nodeStyle, selectedNodeStyle));
        }

        private void OnClickInPoint(BaseNode.ConnectionPoint inPoint)
        {
            selectedInnerPoint = inPoint;

            if (selectedOuterPoint != null)
            {
                if (selectedOuterPoint.node != selectedInnerPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection(); 
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }

        private void OnClickOutPoint(BaseNode.ConnectionPoint outPoint)
        {
            selectedOuterPoint = outPoint;

            if (selectedInnerPoint != null)
            {
                if (selectedOuterPoint.node != selectedInnerPoint.node)
                {
                    CreateConnection();
                    ClearConnectionSelection();
                }
                else
                {
                    ClearConnectionSelection();
                }
            }
        }
        private void CreateConnection()
        {
            if (connectionList == null)
            {
                connectionList = new List<Connection>();
            }

            // connectionList.Add(new Connection(selectedInnerPoint, selectedOuterPoint, 
                // OnClickRemoveConnection));
        }

        private void ClearConnectionSelection()
        {
            selectedInnerPoint = null;
            selectedOuterPoint = null;
        }
        private void OnClickRemoveConnection(Connection connection)
        {
            connectionList.Remove(connection);
        }

        private void DrawNodes()
        {
            if (nodeList == null) return;
            for (var i = nodeList.Count - 1; i >= 0; i--)
            {
                nodeList[i].Draw();
            }
        }
    }
}