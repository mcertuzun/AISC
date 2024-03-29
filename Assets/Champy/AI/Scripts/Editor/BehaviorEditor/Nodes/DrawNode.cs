﻿using UnityEngine;

namespace Champy.AI.Editor
{
    public abstract class DrawNode : ScriptableObject
    {
        public abstract void DrawWindow(BaseNode b);
        public abstract void DrawCurve(BaseNode b);
    }
}