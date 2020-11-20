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

    [Serializable]
    public class Employe
    {
        private int _id;
        private string _name;
        private string _country;

        public Employe(int id, string name, string country)
        {
            _id = id;
            _name = name;
            _country = country;
        }
    }
}