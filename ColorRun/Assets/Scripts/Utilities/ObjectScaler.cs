using System;
using UnityEngine;

namespace GAMO.Hesman.Utilities
{
    /// <summary>
    /// Lazy scale object by depth to camera
    /// </summary>
    [ExecuteInEditMode]
    public class ObjectScaler : MonoBehaviour
    {
        #region CONSTANT FIELDS
        private const float REF_WIDTH = 1136f;
        private const float REF_HEIGHT = 640f;
        #endregion
        #region FIELDS
        [Header("References")]
        [SerializeField]
        private Camera renderCamera;
        [SerializeField]
        private MeshRenderer meshRenderer;
        [Header("Configurations")]
        [SerializeField]
        private int sortingOrder = 0;
        [SerializeField]
        private ScaleMethod scaleMethod = ScaleMethod.FILL;
        [SerializeField]
        private Vector2 originalTexture;
        [SerializeField]
        [Tooltip("Phần scale thừa ra hoặc thụt vào khi sử dụng INNER or OUTER scale")]
        [Range(0, 1)]
        private float redundantPercentage;
        [Header("Debug Info")]
        [SerializeField]
        private Vector2 cameraSize;
        [SerializeField]
        private Vector2 originalScale;
        [SerializeField]
        private float originalRatio;
        [SerializeField]
        private float screenRatio;
        #endregion
        #region ENUMS
        /// <summary>
        /// All supported scale methods
        /// </summary>
        public enum ScaleMethod
        {
            /// <summary>
            /// No scale
            /// </summary>
            NONE = 0,
            /// <summary>
            /// Scale to fit camera projection, either by he
            /// </summary>
            FIT = 1,
            FILL = 2,
            INNER = 3,
            OUTER = 4
        }
        #endregion
        #region MONOBEHAVIOR
        private void Awake()
        {
            if (this.renderCamera == null)
                this.renderCamera = Camera.main;
            if (this.meshRenderer == null)
                throw new NullReferenceException();
        }
        private void Start()
        {
            this.initialize();
            this.scale();
        }
#if UNITY_EDITOR
        private void Update()
        {
            this.scale();
        }
#endif
        #endregion
        #region METHODS
        private void initialize()
        {
            if (this.scaleMethod != ScaleMethod.NONE)
            {
                this.originalScale = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
                this.originalRatio = this.originalTexture.x / this.originalTexture.y;
                if (this.renderCamera == null) return;
                this.screenRatio = (float)Screen.width / (float)Screen.height;
                if (this.renderCamera.orthographic)
                {
                    float width = this.renderCamera.orthographicSize * 2f;
                    float height = width * screenRatio;
                    this.cameraSize = new Vector2(height, width);
                }
                else
                {
                    float distance = Vector3.Distance(this.transform.position, this.renderCamera.transform.position);
                    float width = 2f * Mathf.Tan(0.5f * this.renderCamera.fieldOfView * Mathf.Deg2Rad) * distance;
                    float height = width * screenRatio;
                    this.cameraSize = new Vector2(height, width);
                }
            }
            this.meshRenderer.sortingOrder = this.sortingOrder;
        }
        private void scale()
        {
            float x = 1, y = 1;
            switch (this.scaleMethod)
            {
                case ScaleMethod.FIT:
                    if (this.originalRatio < this.screenRatio)
                    {
                        // scale by height
                        if (this.originalTexture.x < REF_WIDTH)
                        {
                            x = this.cameraSize.x * this.originalTexture.x / REF_WIDTH;
                            y = this.cameraSize.y;
                        }
                        else
                        {
                            x = this.cameraSize.x;
                            y = this.cameraSize.y * (this.screenRatio / this.originalRatio);
                        }
                    }
                    else
                    {
                        // scale by width
                        if (this.originalTexture.y < REF_HEIGHT)
                        {
                            y = this.cameraSize.y * this.originalTexture.y / REF_HEIGHT;
                            x = this.cameraSize.x + 0.1f;
                        }
                        else
                        {
                            y = this.cameraSize.y + 0.1f;
                            x = this.cameraSize.x * (this.originalRatio / this.screenRatio);
                        }
                    }
                    break;
                case ScaleMethod.FILL:
                    this.transform.localScale = this.cameraSize;
                    return;
                case ScaleMethod.INNER:
                    if (this.originalRatio < this.screenRatio)
                    {
                        // scale by height
                        if (this.originalTexture.x < REF_WIDTH)
                        {
                            x = (this.cameraSize.x - (this.cameraSize.x * this.redundantPercentage)) * this.originalTexture.x / REF_WIDTH;
                            y = (this.cameraSize.y - (this.cameraSize.y * this.redundantPercentage));
                        }
                        else
                        {
                            x = this.cameraSize.x - (this.cameraSize.x * this.redundantPercentage);
                            y = (this.cameraSize.y - (this.cameraSize.y * this.redundantPercentage)) * (this.screenRatio / this.originalRatio);
                        }
                    }
                    else
                    {
                        // scale by width
                        if (this.originalTexture.y < REF_HEIGHT)
                        {
                            y = (this.cameraSize.y - (this.cameraSize.y * this.redundantPercentage)) * this.originalTexture.y / REF_HEIGHT;
                            x = this.cameraSize.x - (this.cameraSize.x * this.redundantPercentage);
                        }
                        else
                        {
                            y = this.cameraSize.y - (this.cameraSize.y * this.redundantPercentage);
                            x = (this.cameraSize.x - (this.cameraSize.x * this.redundantPercentage)) * (this.originalRatio / this.screenRatio);
                        }
                    }
                    break;
                case ScaleMethod.OUTER:
                    if (this.originalRatio < this.screenRatio)
                    {
                        // scale by height
                        x = this.cameraSize.x + (this.cameraSize.x * this.redundantPercentage);
                        y = (this.cameraSize.y + (this.cameraSize.y * this.redundantPercentage)) * (this.screenRatio / this.originalRatio);
                    }
                    else
                    {
                        // scale by width
                        y = this.cameraSize.y + (this.cameraSize.y * this.redundantPercentage);
                        x = (this.cameraSize.x + (this.cameraSize.x * this.redundantPercentage)) * (this.originalRatio / this.screenRatio);
                    }
                    break;
                default:
                    break;
            }
            if (this.scaleMethod == ScaleMethod.FILL || this.scaleMethod == ScaleMethod.FIT)
                this.transform.localScale = new Vector3(x, y, 1);
            else if (this.scaleMethod != ScaleMethod.NONE)
                this.transform.localScale = new Vector3(x.floorInRange(), y.floorInRange(), 1);
        }
        #endregion
    }
}