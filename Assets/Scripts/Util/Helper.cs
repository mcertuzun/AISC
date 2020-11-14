using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Util
{
    public class Helper : MonoBehaviour
    {
        public static T RemoveDuplicates<T>() where T : Component
        {
            var instances = Object.FindObjectsOfType<T>();
            if (instances.Length > 1)
            {
                for (var i = 1; i < instances.Length; i++)
                {
                    Destroy(instances[i]);
                }
            }
            return instances[0];
        }
        
        
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

 

}