using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Champy.GameStarter.SaveLoadSerialization
{
    // ReSharper disable once InconsistentNaming
    public class XMLSerializer : ISerializer
    {
        public void Serialize<T>(T obj, Stream stream, Encoding encoding)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, obj);
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
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                result = (T) serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            return result;
        }
    }
}