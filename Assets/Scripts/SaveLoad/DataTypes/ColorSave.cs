﻿using System;
using UnityEngine;

namespace SaveLoad.DataTypes
{
    /// <summary>
    /// Representation of RGBA color.
    /// </summary>
    [Serializable]
    public struct ColorSave
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public ColorSave(Color color)
        {
            this.r = color.r;
            this.g = color.g;
            this.b = color.b;
            this.a = color.a;
        }

        public static implicit operator ColorSave(Color color)
        {
            return new ColorSave(color);
        }

        public static implicit operator Color(ColorSave color)
        {
            return new Color(color.r, color.g, color.b, color.a);
        }
    }
}