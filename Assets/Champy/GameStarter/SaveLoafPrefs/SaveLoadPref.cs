using System;
using UnityEngine;
using static Sabresaurus.PlayerPrefsUtilities.PlayerPrefsUtility;

namespace Champy.SaveLoadPrefs
{
    public static class SaveLoadPref
    {
        public static void Save<T>(string name, T value)
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    SetEncryptedBool(name, (bool) Convert.ChangeType(value, typeof(T)));
                    break;
                case TypeCode.Int32:
                    SetEncryptedInt(name, (int) Convert.ChangeType(value, typeof(T)));
                    break;
                case TypeCode.Single:
                    SetEncryptedFloat(name, (float) Convert.ChangeType(value, typeof(T)));
                    break;
                case TypeCode.String:
                    SetEncryptedString(name, (string) Convert.ChangeType(value, typeof(T)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static object Load<T>(string name, T initialValue)
        {
            object value;
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Boolean:
                    if (initialValue != null)
                        value = GetEncryptedBool(name, (bool) Convert.ChangeType(initialValue, typeof(T)));
                    else
                        value = GetEncryptedBool(name);
                    break;
                case TypeCode.Int32:
                    if (initialValue != null)
                        value = GetEncryptedInt(name, (int) Convert.ChangeType(initialValue, typeof(T)));
                    else
                        value = GetEncryptedInt(name);
                    break;
                case TypeCode.Single:
                    if (initialValue != null)
                        value = GetEncryptedFloat(name, (float) Convert.ChangeType(initialValue, typeof(T)));
                    else
                        value = GetEncryptedFloat(name);
                    break;
                case TypeCode.String:
                    if (initialValue != null)
                        value = GetEncryptedString(name, (string) Convert.ChangeType(initialValue, typeof(T)));
                    else
                        value = GetEncryptedString(name);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return value;
        }

        public static void Reset()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}