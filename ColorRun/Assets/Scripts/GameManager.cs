using System;
using System.Collections.Generic;
using UnityEngine;

namespace Subzero
{
    public class GameManager : MonoBehaviour
    {
        #region FIELDS

        private static GameManager _instance;

        [Header("Configurations")]
        [SerializeField]
        private bool allowBackKey = true;
        
        //[Header("References - Windows")]
        //[SerializeField]
        //private SplashScreen splashScreen;

        //[SerializeField]
        //private MainScreen mainScreen;

        
        #endregion FIELDS

        #region PROPERTIES

        public static GameManager Instance { get { return _instance; } }

        /// <summary>
        /// Add or remove event handler when timer interval (default 1 seconds)
        /// </summary>

        #region Screens

        /// <summary>
        /// Get component manage Splash Screen
        /// </summary>
        //public static SplashScreen SplashScreen { get { return _instance.splashScreen; } }
        #endregion Screens

        #region Dialogs

        /// <summary>
        /// Get component manage Login Dialog
        /// </summary>
        //public static LoginDialog LoginDialog { get { return _instance.loginDialog; } }
        #endregion Dialogs

        public bool AllowBackKey { get { return this.allowBackKey; } set { this.allowBackKey = value; } }

        #endregion PROPERTIES

        #region MONOBEHAVIOR

        private void Awake()
        {
            if (_instance != null) { Destroy(this.gameObject); return; }
            DontDestroyOnLoad(this.gameObject);
            _instance = this;

            // check references
        }

        private void Start()
        {
            TouchScreenKeyboard.hideInput = true;
            //this.splashScreen.Show();
        }
        #endregion MONOBEHAVIOR

        #region METHODS
        
        #endregion METHODS
    }
}