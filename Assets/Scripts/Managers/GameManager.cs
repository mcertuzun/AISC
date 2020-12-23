using System;
using UnityEngine;
using Enums;
using InputSystem;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static event System.Action CanvasManager;
        public static event System.Action SaveLoadManager;
        public static event System.Action LevelManager;

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
                ManagersSetup();
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

        [Tooltip("Open/Close save load system")]
        public bool saveLoadActive = true;

        [Tooltip("Open/Close UI system")] public bool canvasActive = true;
        public static int GameCount { get; private set; } = 0;
        private static bool _isRestart;

        //General header for reference objects
        [Header("Object References")] public GameObject gameObjects;

        #region Managers Setup

        private void ManagersSetup()
        {
            SaveLoadInstantiate();
            CanvasInstantiate();
        }

        private void CanvasInstantiate()
        {
            if (CanvasManager == null && canvasActive)
            {
                var canvasManager = Resources.Load<GameObject>("Managers/MasterCanvas");
                Debug.LogWarning($"{canvasManager.name} Instantiated");
                Instantiate(canvasManager);
            }

            CanvasManager?.Invoke();
        }

        private void SaveLoadInstantiate()
        {
            if (SaveLoadManager == null && saveLoadActive)
            {
                var saveLoadManager = Resources.Load<GameObject>("Managers/SaveLoadManager");
                Debug.LogWarning($"{saveLoadManager.name} Instantiated");
                Instantiate(saveLoadManager);
            }

            SaveLoadManager?.Invoke();
        }

        #endregion


        public void TryIt()
        {
            Debug.Log("It is working");
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}