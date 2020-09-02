using System;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using RotaryHeart.Lib.SerializableDictionary;

namespace Managers
{
    public class CanvasManager : MonoBehaviour
    {
        [Header("General Options")] [Tooltip("Choose the begging action")]
        public StartType chooseStartType;

        [Header("Canvases")] 
         private Dictionary<CanvasType, Canvas> allCanvasesEnum;
         private Dictionary<StartType, List<Canvas>> canvasListEnum;

        /*
        [System.Serializable]
        public class EnumCanvas : SerializableDictionaryBase<StartType, List<Canvas>>
        {
        };

        [Tooltip("Chosen canvases")] public EnumCanvas canvasListEnum;
        */

        #region DontDestroyOnLoad

        private static CanvasManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                allCanvasesEnum = new Dictionary<CanvasType, Canvas>();
                canvasListEnum = new Dictionary<StartType, List<Canvas>>();
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion

        private void OnEnable()
        {
            GameManager.CanvasManager += OpenCanvasManager;
        }

        private void OnDisable()
        {
            GameManager.CanvasManager -= OpenCanvasManager;
        }

        private void Start()
        {
            CanvasInitializer();
            InitStartingOptions();
        }

        private void OpenCanvasManager()
        {
            CanvasInitializer();
            InitStartingOptions();
        }

        // Find canvases from this gameObject 
        private void CanvasInitializer()
        {
            var childCanvases = this.gameObject.GetComponentsInChildren<Canvas>();

            if (childCanvases.Length == 0)
            {
                Debug.LogWarning("Warning: Canvases didnt load!");
                return;
            }

            foreach (var childCanvas in childCanvases)
            {
                AddEnumList(childCanvas);
            }
        }

        private void AddEnumList(Canvas childCanvas)
        {
            var checkParse = Enum.TryParse(childCanvas.name, out CanvasType canvasType);
            (checkParse
                ? new Action(() => allCanvasesEnum.Add(canvasType, childCanvas))
                : () => Debug.LogWarning("Warning: CanvasType Enum name is not compatible with the canvas name."))();
        }

        //TODO: Optimize InitStartingOptions() Method
        private void InitStartingOptions()
        {
            var startTypes = Enum.GetValues(typeof(StartType));

            foreach (StartType startType in startTypes)
            {
                var list = new List<Canvas>();

                list.Add(allCanvasesEnum[CanvasType.InGameCanvas]);
                list.Add(allCanvasesEnum[CanvasType.WinCanvas]);
                list.Add(allCanvasesEnum[CanvasType.FailCanvas]);
                list.Add(allCanvasesEnum[CanvasType.SettingsCanvas]);
                if (startType.Equals(StartType.Menu))
                {
                    list.Add(allCanvasesEnum[CanvasType.MainCanvas]);
                }
                if (startType.Equals(StartType.Splash))
                {
                    list.Add(allCanvasesEnum[CanvasType.SplashCanvas]);
                }
                canvasListEnum.Add(startType, list);
            }

            SetActivations();
        }

        private void SetActivations()
        {
            foreach (var canvas in allCanvasesEnum.Values)
            {
                Debug.Log(canvasListEnum[chooseStartType].Count);
                if (canvasListEnum[chooseStartType].Contains(canvas))
                {
                    canvas.gameObject.SetActive(true);
                }
                else
                {
                    canvas.gameObject.SetActive(false);
                }
            }
        }

        #region SplashScreen

        #endregion
    }
}