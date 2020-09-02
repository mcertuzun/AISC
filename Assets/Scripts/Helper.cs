using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class Helper
{
    public static List<T> SameTypeOfComponents<T>(MonoBehaviour thisObject) where T : class
    {
        BindingFlags bindingFlags = (BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        FieldInfo[] fields = thisObject.GetType().GetFields(bindingFlags);
        List<T> tempList = new List<T>();
        foreach (FieldInfo field in fields)
        {
            T value = field.GetValue(thisObject) as T;

            if (value != null)
            {
               tempList.Add(value);
            }
        }
        return tempList;
    }

  
   
}