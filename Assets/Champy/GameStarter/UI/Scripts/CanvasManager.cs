using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Champy.UI
{
    public class CanvasManager : MonoBehaviour
    {
        [Serializable]
        public enum CanvasType
        {
            SplashCanvas,
            MainCanvas,
            InGameCanvas,
            SettingsCanvas,
            WinCanvas,
            FailCanvas,
            Empty
        }

        [Header("General Options")] [Tooltip("Choose the begging action")]
        private static StartType startType;

        [Header("Canvases")] private static Dictionary<CanvasType, Canvas> canvasList;

        #region Instantiate

        private static CanvasManager _instance;

        private void Awake()
        {
            canvasList = new Dictionary<CanvasType, Canvas>();
            this.Instantiate();
        }

        private void Instantiate()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (this != _instance)
            {
                Debug.LogError($"Duplicated {this.name} destroyed!");
                Destroy(this.gameObject);
            }
        }

        #endregion

        #region Setup Canvas

        private void OnEnable()
        {
            GameManager.CanvasManager += OpenCanvasManager;
        }

        private void OnDisable()
        {
            GameManager.CanvasManager -= OpenCanvasManager;
        }

        private void OpenCanvasManager(StartType type)
        {
            startType = type;
            CanvasInitializer();
            SetActivations();
        }

        // Find child canvases from the MasterCanvas 
        private void CanvasInitializer()
        {
            var childCanvases = this.gameObject.GetComponentsInChildren<Canvas>();
            canvasList.Clear();

            if (EventSystem.current == null)
            {
                var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
                Instantiate(eventSystem);
            }

            if (childCanvases.Length == 0)
            {
                Debug.LogWarning("Warning: Canvases didn't load!");
                return;
            }

            foreach (var childCanvas in childCanvases)
            {
                var checkParse = Enum.TryParse(childCanvas.name, out CanvasType canvasType);
                if (!checkParse)
                {
                    Debug.LogWarning("Warning: CanvasType Enum name is not compatible with the canvas name.");
                    continue;
                }

                Debug.LogWarning($"canvastype {canvasType}");

                AddCanvasList(canvasType, childCanvas);
            }

            Debug.Log($"Master has {canvasList.Count} canvas ");
        }

        private void AddCanvasList(CanvasType canvasType, Canvas childCanvas)
        {
            childCanvas.enabled = false;
            childCanvas.gameObject.SetActive(true);
            canvasList.Add(canvasType, childCanvas);
        }

        private void SetActivations()
        {
            switch (startType)
            {
                case StartType.Menu:
                    CanvasSwitch(CanvasType.MainCanvas);
                    break;
                case StartType.Splash:
                    //Temporary solution for loading UI
                    StartCoroutine(WaitSplash());
                    break;
                case StartType.Direct:
                    CanvasSwitch(CanvasType.InGameCanvas);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerator WaitSplash()
        {
            CanvasSwitch(CanvasType.SplashCanvas);
            yield return new WaitForSeconds(2f);
            CanvasSwitch(CanvasType.MainCanvas);
        }

        private void CanvasSwitch(CanvasType canvasType)
        {
            foreach (var canvasValue in canvasList.Values)
            {
                if (!canvasList.ContainsKey(canvasType))
                    continue;

                canvasValue.enabled = (canvasValue == canvasList[canvasType]);
                //Debug.Log($"name {canvasValue} is {canvasValue.enabled}");
            }
        }

        #endregion

        #region UI Action Methods

        public void OnClickStart()
        {
            GameManager.StartGame();
            CanvasSwitch(CanvasType.InGameCanvas);
        }

        public void OnClickRestart()
        {
            CanvasSwitch(CanvasType.InGameCanvas);
        }

        public void OnClickPause()
        {
            GameManager.PauseGame();
            //CanvasSwitch(CanvasType.SettingsCanvas);
        }

        public void OnClickExit()
        {
            Application.Quit();
        }

        public void OnClickBack()
        {
            Application.Quit();
        }

        public void OnWinMenu()
        {
            CanvasSwitch(CanvasType.WinCanvas);
        }

        public void OnFailMenu()
        {
            CanvasSwitch(CanvasType.FailCanvas);
        }

        #endregion
    }
}