using System.IO;
using System.Text;

namespace Champy.GameStarter.SaveLoadSerialization
{
    public interface ISerializer
    {
        /// <summary>
        /// Serialize the specified object to stream with encoding.
        /// </summary>
        void Serialize<T>(T obj, Stream stream, Encoding encoding);

        /// <summary>
        /// Deserialize the specified object from stream using the encoding.
        /// </summary>
        T DeSerialize<T>(Stream stream, Encoding encoding);
    }
}