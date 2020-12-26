using System;
using Enums;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static event Action<StartType> CanvasManager;
        public static event Action SaveLoadManager;
        public static event Action LevelManager;

        //<summary>
        //State Pattern separates the states of a Game and these are in the GameState enum file.
        //<summary>

        #region Game State

        public enum GameState
        {
            Prepare = 0,
            Playing = 1,
            Paused = 2,
            PreGameOver = 3,
            GameOver = 4,
        }

        [Header("Current game state")] private static GameState _gameState = GameState.Prepare;
        public static event Action<GameState, GameState> GameStateChanged;

        public static GameState gameState
        {
            get => _gameState;
            set
            {
                if (value != _gameState)
                {
                    GameState oldState = _gameState;
                    _gameState = value;
                    GameStateChanged?.Invoke(oldState, value);
                }
            }
        }


        public static void PauseGame()
        {
            Time.timeScale = 0;
            gameState = GameState.Paused;
        }

        public static void StartGame()
        {
            Time.timeScale = 1;
            gameState = GameState.Playing;
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
        [ConditionalHide("canvasActive")] public StartType chooseStartType;

        public static int GameCount { get; private set; } = 0;
        private static bool _isRestart;

        //General header for reference objects
        [Header("Object References")] public GameObject gameObjects;


        #region Managers Setup

        private void ManagersSetup()
        {
            if (canvasActive)
                CanvasSystem(CanvasManager);
            if (saveLoadActive)
                SaveLoadSystem(SaveLoadManager);
        }

        private void CanvasSystem(Action<StartType> manager)
        {
            if (manager == null)
            {
                var tempManager = Resources.Load<GameObject>($"Managers/MasterCanvas");
                Debug.LogWarning($"{tempManager.name} Instantiated");
                Instantiate(tempManager);
            }

            manager?.Invoke(chooseStartType);
        }

        private void SaveLoadSystem(Action manager)
        {
            if (manager == null)
            {
                var tempManager = Resources.Load<GameObject>($"Managers/SaveLoadManager");
                Debug.LogWarning($"{tempManager.name} Instantiated");
                Instantiate(tempManager);
            }

            manager?.Invoke();
        }

        #endregion

        // Update is called once per frame
        private void Update()
        {
            Debug.Log($"current state = {gameState} and time scale {Time.timeScale}");
        }

        private void GameStateHandler()
        {
            // switch (gameState)
            // {
            //     case GameState.Playing:
            //         StartGame();
            //         break;
            //     case GameState.Paused:
            //         PauseGame();
            //         break;
            //     case GameState.Prepare:
            //         break;
            //     case GameState.PreGameOver:
            //         break;
            //     case GameState.GameOver:
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
        }
    }
}