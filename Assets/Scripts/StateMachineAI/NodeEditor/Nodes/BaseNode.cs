using System;
using UnityEditor.Compilation;
using UnityEngine;

namespace StateMachineAI.NodeEditor
{
    public abstract class BaseNode : ScriptableObject
    {
        public Rect nodeRect;
        public Vector2 position;
        public float width;
        public float height;
        public bool isDragged;
        public string title = "Default Node";
        
        public ConnectionPoint innerConnection;
        public ConnectionPoint outerConnection;

        public GUIStyle defaultStyle;
        public GUIStyle selectedStyle;

        protected BaseNode(Vector2 position, float? width, float? height, GUIStyle defaultStyle, GUIStyle selectedStyle)
        {
            this.width  = width ?? 200;
            this.height  = height ?? 50;
            this.position = position;
            nodeRect = new Rect(position.x, position.y, this.width, this.height);
            this.innerConnection = innerConnection;
            this.outerConnection = outerConnection;
            this.defaultStyle = defaultStyle;
            this.selectedStyle = selectedStyle;
        }
        
        public abstract void Draw();
        public abstract void Drag(Vector2 delta);
        public abstract bool ProcessEvents(Event e);
        public abstract void OnClickRemoveNode();
        
       
        
        public class ConnectionPoint
        {
            public enum ConnectionPointType
            {
                In,Out
            }
            
            public string id;
            public BaseNode node;
            public Rect connectionPointRect;
            public ConnectionPointType connectionType;
            public ConnectionPoint(string id, BaseNode node, ConnectionPointType connectionType)
            {
                this.id = id ?? Guid.NewGuid().ToString();
                this.node = node;
                this.connectionPointRect = new Rect(0, 0, 10f, 20f);;
                this.connectionType = connectionType;
                 
            }

            public void Draw()
            {
                connectionPointRect.x = node.nodeRect.y + (node.nodeRect.height * 0.5f) - connectionPointRect.height * 0.5f;
                switch (connectionType)
                {
                    case ConnectionPointType.In:
                        connectionPointRect.x = node.nodeRect.x - connectionPointRect.width + 8f;
                        break;

                    case ConnectionPointType.Out:
                        connectionPointRect.x = node.nodeRect.x - connectionPointRect.width - 8f;
                        break;
                }
            }
        }

       
    }
}