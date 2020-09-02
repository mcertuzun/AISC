using UnityEngine;

namespace Enums
{
    public enum StartType
    {
        [Tooltip("Start the game from splash screen")]
        Splash,
        [Tooltip("Start the game directly")]
        Direct,
        [Tooltip("Start the game from menu canvas")]
        Menu,
        
    }
}