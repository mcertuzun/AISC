using System;
using System.Collections;
using System.Collections.Generic;
using Champy.Level;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Champy.UI
{
    public class CanvasManager : MonoBehaviour
    {
        #region Variables

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

        public enum StartType
        {
            [Tooltip("Start the game from splash screen")]
            Splash,
            [Tooltip("Start the game directly")] Direct,

            [Tooltip("Start the game from menu canvas")]
            Menu,

            Empty
        }

        [Header("General Options")] [Tooltip("Choose the begging action")]
        private static StartType _startType;

        [Header("Canvases")] private static Dictionary<CanvasType, Canvas> _canvasList;
        private static CanvasManager _instance;

        #endregion

        #region Instantiate

        private void Awake()
        {
            _canvasList = new Dictionary<CanvasType, Canvas>();
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
            GameManager.CanvasAction += CanvasSetup;
        }

        private void OnDisable()
        {
            GameManager.CanvasAction -= CanvasSetup;
        }

        private void CanvasSetup(StartType type)
        {
            _startType = type;
            CanvasInitializer();
            SetActivations();
        }

        // Find child canvases from the MasterCanvas 
        private void CanvasInitializer()
        {
            var childCanvases = this.gameObject.GetComponentsInChildren<Canvas>();
            _canvasList.Clear();

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

            Debug.Log($"Master has {_canvasList.Count} canvas ");
        }

        private void SetActivations()
        {
            switch (_startType)
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

        private void AddCanvasList(CanvasType canvasType, Canvas childCanvas)
        {
            childCanvas.enabled = false;
            childCanvas.gameObject.SetActive(true);
            _canvasList.Add(canvasType, childCanvas);
        }

        #endregion

        #region Methods

        //TODO: Calculate splash waiting time later
        private IEnumerator WaitSplash()
        {
            CanvasSwitch(CanvasType.SplashCanvas);
            yield return new WaitForSeconds(2f);
            CanvasSwitch(CanvasType.MainCanvas);
        }

        private void CanvasSwitch(CanvasType canvasType)
        {
            foreach (var canvasValue in _canvasList.Values)
            {
                if (!_canvasList.ContainsKey(canvasType))
                    continue;
                if (canvasValue != _canvasList[canvasType])
                {
                    canvasValue.enabled = false;
                    continue;
                }

                canvasValue.enabled = true;
            }
        }

        private void OpenCanvas(CanvasType canvasType)
        {
            _canvasList[canvasType].enabled = true;
        }

        #endregion

        #region UI Action Methods

        public void OnClickStart()
        {
            CanvasSwitch(CanvasType.InGameCanvas);
            OpenCanvas(CanvasType.WinCanvas);
        }

        public void OnClickRestart()
        {
            CanvasSwitch(CanvasType.InGameCanvas);
        }

        public void OnClickPause()
        {
            CanvasSwitch(CanvasType.SettingsCanvas);
        }

        public void OnClickNext()
        {
            StartCoroutine(CLevelManager.LoadNextScene());
        }

        public void OnClickBack()
        {
            StartCoroutine(CLevelManager.LoadPreviousScene());
        }

        public void OnClickExit()
        {
            Application.Quit();
        }

        public void OpenWinMenu()
        {
            CanvasSwitch(CanvasType.WinCanvas);
        }

        public void OpenFailMenu()
        {
            CanvasSwitch(CanvasType.FailCanvas);
        }

        #endregion
    }
}