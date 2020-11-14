using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class example :MonoBehaviour
    {
        
        public void OnClickNext()
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }
        public void OnClickPre()
        {
            if((SceneManager.GetActiveScene().buildIndex - 1) >= 0)
                SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex - 1) % SceneManager.sceneCountInBuildSettings);
            else 
                Debug.LogWarning($"{(SceneManager.GetActiveScene().buildIndex)} is the least index");
        }
    }
}