using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Champy.GameStarter.SaveLoadSerialization
{
    /// <summary>
    /// Interface for Save Game Encoders.
    /// </summary>
    public interface IEncoder
    {
        /// <summary>
        /// Encode the specified input with password.
        /// </summary>
        string Encode(string input, string password);

        /// <summary>
        /// Decode the specified input with password.
        /// </summary>
        string Decode(string input, string password);
    }
}

