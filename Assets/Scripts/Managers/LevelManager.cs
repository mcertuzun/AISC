using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public bool saveLoadSystem;
        
        #region Single Object

        private static LevelManager _instance;
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
        
        private void OnEnable()
        {
            if (saveLoadSystem)
                GameManager.LevelManager += LoadSaveSystem;
        }

        private void OnDisable()
        {
            if(saveLoadSystem)
                GameManager.LevelManager -= LoadSaveSystem;
        }

        private void LoadSaveSystem()
        {
            
        }
      
        
        public static void LoadNextLevelAsync()
        {
            // StartCoroutine(LoadYourAsyncScene(SceneManager.GetActiveScene().buildIndex+1));
        }
        /*IEnumerator LoadYourAsyncScene(int nextLevelIndex)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextLevelIndex);
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }*/
        
    }
}




/*public class LevelManager : MonoBehaviour
{
    #region Single Object

    private static LevelManager _instance;
        
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

    private void Destroy(object gameObject)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    private SaveLoadManager _saveLoadManager;
    private void Start()
    {
        _saveLoadManager = GameObject.Find("SaveLoadManager").GetComponent<SaveLoadManager>();
    }

    public void LoadNextLevelAsync()
    {
        StartCoroutine(LoadYourAsyncScene(SceneManager.GetActiveScene().buildIndex+1));
    }
    IEnumerator LoadYourAsyncScene(int nextLevelIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextLevelIndex);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }*/