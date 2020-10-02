using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;

namespace Managers
{
    public class SaveLoadManager : MonoBehaviour
    {
        private bool isReset;
        public int lastSaveIndex;
        public bool hasTutorial;
        public int tutorialLevelIndex = 1; 
        
        #region Singleton

        private static SaveLoadManager _instance;

        public static SaveLoadManager Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                LoadLastSaveIndex();
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        #endregion

        private void LoadLastSaveIndex()
        {
          
            lastSaveIndex = PlayerPrefs.GetInt("nextSceneIndex");
            if (hasTutorial && lastSaveIndex > tutorialLevelIndex)
            {
                PlayerPrefsX.SetBool("IsTutorialPassed", true);
            }
            Debug.LogWarning($"The last save index is {lastSaveIndex}");
            SceneManager.LoadScene((lastSaveIndex));
        }

        public void PassTheLevel()
        {
            var nextLevelIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            PlayerPrefs.SetInt("nextSceneIndex", nextLevelIndex);
        }

        public void SaveTheLevel(int num)
        {
            PlayerPrefs.SetInt("nextSceneIndex", num);
        }

        public void ResetHistory()
        {
            PlayerPrefs.DeleteAll();
            isReset = true;
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("nextSceneIndex", SceneManager.GetActiveScene().buildIndex);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                OnApplicationQuit();
            }
        }
    }
}