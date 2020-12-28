using System;
using Champy.Level;
using StartType = Champy.UI.CanvasManager.StartType;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Variables

        public static event Action<StartType> CanvasAction;

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

        #endregion

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


        #region Setup Managers

        private void ManagersSetup()
        {
            if (canvasActive)
                CanvasSystem(CanvasAction);
            if (saveLoadActive)
                CLevelManager.Instantiate();
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

        #endregion

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnApplicationQuit()
        {
            CLevelManager.OnApplicationQuit();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            CLevelManager.OnApplicationPause(pauseStatus);
        }
    }
}