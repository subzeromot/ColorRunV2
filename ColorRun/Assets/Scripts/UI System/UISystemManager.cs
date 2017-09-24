using Subzero.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Subzero.UISystem
{
    public class UISystemManager : MonoBehaviour
    {
        #region FIELDS
        private static UISystemManager _instance;
        private List<UISystemScreen> dialogs = new List<UISystemScreen>();
        private List<UISystemScreen> windows = new List<UISystemScreen>();
        private UISystemScreen currentScreen;
        [Header("References")]
        [SerializeField]
        private Camera renderCamera;
        [SerializeField]
        private Canvas windowCanvas;
        [SerializeField]
        private Canvas dialogCanvas;
        #endregion
        #region PROPERTIES
        public static UISystemManager Instance { get { return _instance; } }
        public static List<UISystemScreen> Dialogs { get { return _instance.dialogs; } }
        public static List<UISystemScreen> Windows { get { return _instance.windows; } }
        public static UISystemScreen CurrentScreen
        {
            get
            {
                if (!Dialogs.IsNullOrEmpty()) return Dialogs.GetLastItem();
                else return Windows.GetLastItem();
            }
        }
        public static UISystemScreen CurrentWindow { get { return _instance.windows.GetLastItem(); } }
        public static UISystemScreen CurrentDialog { get { return _instance.dialogs.GetLastItem(); } }
        public static Canvas WindowsCanvas { get { return _instance.windowCanvas; } }
        public static Canvas DialogCanvas { get { return _instance.dialogCanvas; } }
        private bool ShowExitDialog { get { return (this.windows.Count <= 1); } }
        #endregion
        #region MONOBEHAVIOR
        private void Awake()
        {
            if (this.renderCamera == null) throw new System.NullReferenceException(this.gameObject + " not reference renderCamera");
            if (this.windowCanvas == null) throw new System.NullReferenceException(this.gameObject + " not reference windowCanvas");
            if (this.dialogCanvas == null) throw new System.NullReferenceException(this.gameObject + " not reference dialogCanvas");
            if (_instance != null) { Destroy(this.gameObject); return; }
            DontDestroyOnLoad(this.gameObject);
            _instance = this;
        }
        private void Update()
        {
            if (GameManager.Instance != null && !GameManager.Instance.AllowBackKey)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (CurrentDialog != null && CurrentDialog.ButtonClose != null)
                {
                    if (CurrentDialog.ButtonClose.interactable)
                    {
                        PointerEventData eventData = new PointerEventData(EventSystem.current);
                        GameObject gameObject = CurrentDialog.ButtonClose.gameObject;
                        ExecuteEvents.Execute(gameObject, eventData, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(gameObject, eventData, ExecuteEvents.pointerDownHandler);
                        ExecuteEvents.Execute(gameObject, eventData, ExecuteEvents.pointerClickHandler);
                    }
                }
                else if (CurrentWindow != null && CurrentWindow.ButtonClose != null)
                {
                    if (CurrentWindow.ButtonClose.interactable)
                    {
                        PointerEventData eventData = new PointerEventData(EventSystem.current);
                        GameObject gameObject = CurrentWindow.ButtonClose.gameObject;
                        ExecuteEvents.Execute(gameObject, eventData, ExecuteEvents.pointerEnterHandler);
                        ExecuteEvents.Execute(gameObject, eventData, ExecuteEvents.pointerDownHandler);
                        ExecuteEvents.Execute(gameObject, eventData, ExecuteEvents.pointerClickHandler);
                    }
                }
                else Back();
            }
        }
        #endregion
        #region METHODS
        public static void Back()
        {
            if (CurrentDialog != null)
            {
                Close(CurrentDialog);
            }
            else if (CurrentWindow != null)
            {
                if (_instance.ShowExitDialog)
                {
                    Debug.LogError("Show exit dialog");
                }
                else _instance.windows.GetNextToLastItem().Show();
            }
        }
        public static void Open(UISystemScreen screen)
        {
            if (screen == null || screen == CurrentScreen) return;
            if (screen.CurrentState == UISystemScreen.States.Opened || screen.CurrentState == UISystemScreen.States.Opening) return;
            switch (screen.Type)
            {
                case UISystemScreen.Types.Window:
                    if (screen == CurrentWindow) return;
                    for (int i = 0; i < Dialogs.Count; i++)
                    {
                        if (!Dialogs[i].PersistendDialog)
                        {
                            Close(Dialogs[i]);
                            i--;
                        }
                    }
                    Close(CurrentWindow);
                    if (Windows.Contains(screen))
                    {
                        int index = Windows.FindIndex(window => window == screen);
                        Windows.RemoveFromIndex(index);
                    }
                    else Windows.Add(screen);
                    break;
                case UISystemScreen.Types.Dialog:
                    if (screen == CurrentDialog) return;
                    if (!Dialogs.Contains(screen))
                        Dialogs.Add(screen);
                    screen.transform.SetAsLastSibling();
                    break;
                default:
                    throw new System.NotSupportedException(screen.name + " have not supported type!");
            }
            screen.CurrentState = UISystemScreen.States.Opening;
        }
        public static void Close(UISystemScreen screen)
        {
            if (screen == null) return;
            if (screen.CurrentState == UISystemScreen.States.Closed || screen.CurrentState == UISystemScreen.States.Closing) return;
            switch (screen.Type)
            {
                case UISystemScreen.Types.Window:
                    if (Windows.Contains(screen) && screen.NoFootprint)
                        Windows.Remove(screen);
                    break;
                case UISystemScreen.Types.Dialog:
                    if (Dialogs.Contains(screen))
                        Dialogs.Remove(screen);
                    break;
                default:
                    throw new System.NotSupportedException(screen.name + " have not supported type!");
            }
            screen.CurrentState = UISystemScreen.States.Closing;
        }
        #endregion
    }
}