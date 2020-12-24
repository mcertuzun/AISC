using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace Champy.SaveLoadPrefs
{
    public class SaveLoadManager : MonoBehaviour
    {
        public bool loopModeActive;
        public int lastSaveIndex;
        //Get hasTutorial from the level manager
        public bool hasTutorial;
        public int tutorialLevelIndex = 1; 
        
        #region Single Object

        private static SaveLoadManager _instance;
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
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
        
        
        
        public static void Save<T>(T value) where T : Component
        {
            Save(value);
        } 
        
        public static void Save(GameObject obj)
        {
            GameObject prefab;
           
        }
        public void Save(int num)
        {
            PlayerPrefs.SetInt("nextSceneIndex", num);
        }
        
     
       

        public static void SaveAs<T>(string name, T value)
        {
          
        }
        public void ResetHistory()
        {
            PlayerPrefs.DeleteAll();
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