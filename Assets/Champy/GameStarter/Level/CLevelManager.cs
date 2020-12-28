using System.Collections;
using UnityEngine.SceneManagement;
using static Champy.SaveLoadPrefs.SaveLoadPref;

namespace Champy.Level
{
    public static class CLevelManager
    {
        #region Variables

        public static int CurrentLevelIndex
        {
            get => _lastSaveIndex == 0 ? 1 : _lastSaveIndex;
            set
            {
                _lastSaveIndex = value;
                Save("CurrentLevelIndex", _lastSaveIndex);
            }
        }

        private static int _lastSaveIndex = 0;

        //Get hasTutorial from the level manager
        public static bool IsTutorialPassed
        {
            get => _hasTutorial;
            set => _hasTutorial = value;
        }

        private static bool _hasTutorial;
        public static readonly int TutorialLevelIndex = 1;
        public static bool loopModeActive = true;

        #endregion

        #region Instantiate

        public static void Instantiate()
        {
            LoadSavedGame();
        }

        public static void LoadSavedGame()
        {
            IsTutorialPassed = (bool) Load("IsTutorialPassed", false);
            CurrentLevelIndex = (int) Load("CurrentLevelIndex", 1);
            SceneManager.LoadSceneAsync(CurrentLevelIndex, LoadSceneMode.Additive);
        }

        #endregion

        #region Scene Management

        public static IEnumerator LoadNextScene()
        {
            var nextLevelIndex = (_lastSaveIndex + 1) % SceneManager.sceneCountInBuildSettings;
            nextLevelIndex = nextLevelIndex == 0 ? 1 : nextLevelIndex;
            var loadingTime = SceneManager.LoadSceneAsync(nextLevelIndex, LoadSceneMode.Additive);
            yield return loadingTime;
            SceneManager.UnloadSceneAsync(CurrentLevelIndex);
            CurrentLevelIndex = nextLevelIndex;
        }

        public static IEnumerator LoadPreviousScene()
        {
            var previousLevelIndex = (_lastSaveIndex - 1) % SceneManager.sceneCountInBuildSettings;
            previousLevelIndex = previousLevelIndex == 0 ? 1 : previousLevelIndex;
            var loadingTime = SceneManager.LoadSceneAsync(CurrentLevelIndex, LoadSceneMode.Additive);
            yield return loadingTime;
            SceneManager.UnloadSceneAsync(CurrentLevelIndex);
            CurrentLevelIndex = previousLevelIndex;
        }

        #endregion

        #region Helper Methods

        public static void OnApplicationQuit()
        {
            Save("CurrentLevelIndex", SceneManager.GetActiveScene().buildIndex);
        }

        public static void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                OnApplicationQuit();
            }
        }

        #endregion
    }
}