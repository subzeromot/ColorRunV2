using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using GAMO.Hesman.Utilities;

namespace GAMO.Hesman
{
    /// <summary>
    /// Automatically scale referenced Canvas per ratio
    /// </summary>
    [RequireComponent(typeof(CanvasScaler))]
    [ExecuteInEditMode]
    public class CanvasRatioHelper : MonoBehaviour
    {
        #region FIELDS
        [Header("References")]
        [SerializeField]
        [Tooltip("reference to canvas scaler component")]
        private CanvasScaler canvasScaler;
        [Header("Configurations")]
        [SerializeField]
        [Tooltip("ratio which will change canvas scaler match properties to either [width] or [height]")]
        [Range(0.5f, 2.0f)]
        private float ratioThreshold;
        #endregion
        #region MONOBEHAVIOR
        private void Awake()
        {
            this.CheckReferences();
            this.CheckConfigurations();
        }
        private void OnEnable()
        {
            this.CheckReferences();
            this.CheckConfigurations();
            this.ChangeScaleMatchMode();
        }
#if UNITY_EDITOR
        private void Update()
        {
            // ensure scale match mode get updated while play in editor
            this.ChangeScaleMatchMode();
        }
#endif
        #endregion
        #region METHODS
        #region Public Methods
        #endregion
        #region Private Methods
        /// <summary>
        /// Change canvas scaler matchWidthOrHeight properties base on actual screen aspect
        /// </summary>
        private void ChangeScaleMatchMode()
        {
            if (Camera.main.aspect <= this.ratioThreshold && this.canvasScaler.matchWidthOrHeight != 0)
            {
                this.canvasScaler.matchWidthOrHeight = 0;
            }
            else if(Camera.main.aspect > this.ratioThreshold && this.canvasScaler.matchWidthOrHeight != 1)
            {
                this.canvasScaler.matchWidthOrHeight = 1;
            }
        }
        private void CheckReferences()
        {
            // TODO: check for any references here
            if (this.canvasScaler == null)
            {
                this.canvasScaler = this.GetComponent<CanvasScaler>();
#if GAMO_DEBUG
                Debug.LogError("canvasScaler is null");
#endif
            }
        }
        private void CheckConfigurations()
        {

            if (this.ratioThreshold < (4 / 3))
            {
#if GAMO_DEBUG
                Debug.LogErrorFormat("no support for theshold smaller than {0}", (4 / 3));
#endif
            }
        }
        #endregion
        #endregion
    }
}