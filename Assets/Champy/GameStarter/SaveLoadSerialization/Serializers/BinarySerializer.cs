using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace Champy.GameStarter.SaveLoadSerialization
{
    public class BinarySerializer : ISerializer
    {
        public void Serialize<T>(T obj, Stream stream, Encoding encoding)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public T DeSerialize<T>(Stream stream, Encoding encoding)
        {
            T result = default(T);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                result = (T) formatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            return result;
        }
    }
}