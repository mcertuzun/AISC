using System;
using UnityEngine;
using Enums;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static event Action CanvasManager;

        //<summary>
        //State Pattern separates the states of a Game and these are in the GameState enum file.
        //<summary>
        #region State Pattern

        [Header("Current game state")] private GameState _gameState = GameState.Prepare;
        public static event Action<GameState, GameState> GameStateChanged;

        public GameState GameState
        {
            get => _gameState;
            private set
            {
                if (value != _gameState)
                {
                    GameState oldState = _gameState;
                    _gameState = GameState;
                    GameStateChanged?.Invoke(_gameState, oldState);
                }
            }
        }

        #endregion

        #region Singleton Pattern

        private static GameManager _instance;
        public static GameManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }

        #endregion

        [Header("Game Settings Config")]
        [Tooltip("Use 60 for games requiring smooth quick motion, set -1 to use platform default frame rate")]
        public int targetFrameRate = 60;
        public static int GameCount { get; private set; } = 0;
        private static bool _isRestart;

        //General header for reference objects
        [Header("Object References")] 
        public GameObject gameObject;

        
        
        // Start is called before the first frame update
        private void Start()
        {
            CanvasManager?.Invoke();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public static void That(Func<bool> check)
        {
            if (!check())
            {
                throw new ArgumentException("Invalid");
            }
        }
    }
}