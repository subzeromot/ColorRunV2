using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Subzero.UISystem
{
    [RequireComponent(typeof(Animator))]
    public class UISystemScreen : MonoBehaviour {
        #region FIELDS
        private States currentState = States.Closed;
        [Header("Configurations")]
        [SerializeField]
        [Tooltip("Will not be register in history, can not be back to")]
        private bool noFootprint = false;
        [SerializeField]
        [Tooltip("Will not be close when change window")]
        private bool persistentDialog = false;
        [SerializeField]
        private Types type = Types.Window;
        [Header("References")]
        [SerializeField]
        [Tooltip("When screen want to have differently with backkey")]
        private Button buttonClose;
        [SerializeField]
        [Tooltip("Animator control animations of this screen")]
        private Animator animator;
        [Header("Events")]
        [SerializeField]
        private UnityEvent onShowing;
        [SerializeField]
        private UnityEvent onShowed;
        [SerializeField]
        private UnityEvent onClosing;
        [SerializeField]
        private UnityEvent onClosed;
        #endregion
        #region ENUMS
        public enum States { Opening, Opened, Closing, Closed }
        public enum Types { Window, Dialog }
        #endregion
        #region PROPERTIES
        /// <summary>
        /// Get the animator of this screen
        /// </summary>
        protected Animator Animator { get { return this.animator; } }
        public bool NoFootprint { get { return this.noFootprint; } }
        public bool PersistendDialog { get { return this.persistentDialog; } }
        public virtual Button ButtonClose { get { return this.buttonClose; } }
        public Types Type { get { return this.type; } }
        public States CurrentState {
            get { return this.currentState; }
            set {
                if (this.currentState != value) {
                    this.currentState = value;
                    this.changeState();
                }
            }
        }
        public UnityEvent OnShowing {
            get { return this.onShowing; }
            set { this.onShowing = value; }
        }
        public UnityEvent OnShowed {
            get { return this.onShowed; }
            set { this.onShowed = value; }
        }
        public UnityEvent OnClosing {
            get { return this.onClosing; }
            set { this.onClosing = value; }
        }
        public UnityEvent OnClosed {
            get { return this.onClosed; }
            set { this.onClosed = value; }
        }
        #endregion
        #region MONOBEHAVIOR
        private void Awake() {
            if (this.animator == null) this.animator = this.GetComponent<Animator>();
        }
        #endregion
        #region METHODS
        /// <summary>
        /// Show this screen
        /// </summary>
        public virtual void Show()
        {
            UISystemManager.Open(this);
        }
        /// <summary>
        /// Convenience function for lazy coding to hide this screen. USE WITH CAUTION!
        /// </summary>
        public virtual void Hide()
        {
            UISystemManager.Close(this);
        }
        private void changeState() {
            switch (this.currentState) {
                case States.Opening:
                    this.gameObject.SetActive(true);
                    if (this.onShowing != null) this.onShowing.Invoke();
                    this.animator.SetBool(Constant.OPEN, true);
                    this.StartCoroutine(this.waitTillFinishAnimation(Constant.OPEN));
                    break;
                case States.Opened:
                    if (this.onShowed != null) this.onShowed.Invoke();
                    break;
                case States.Closing:
                    if (this.onClosing != null) this.onClosing.Invoke();
                    if (gameObject.activeInHierarchy) {
                        this.animator.SetBool(Constant.OPEN, false);
                        this.StartCoroutine(this.waitTillFinishAnimation(Constant.CLOSE));
                    }
                    break;
                case States.Closed:
                    if (this.onClosed != null) this.onClosed.Invoke();
                    this.gameObject.SetActive(false);
                    break;
                default:
                    throw new System.NotSupportedException(this.name + " trying to change to a not supported state");
            }
        }
        private IEnumerator waitTillFinishAnimation(string state) {
            bool isFinishTranstion = false;
            while (!isFinishTranstion) {
                if (!this.animator.IsInTransition(Constant.LAYER))
                    isFinishTranstion = animator.GetCurrentAnimatorStateInfo(Constant.LAYER).IsName(state) && animator.GetCurrentAnimatorStateInfo(Constant.LAYER).normalizedTime >= 1;
                yield return new WaitForEndOfFrame();
            }
            switch (this.currentState) {
                case States.Opening:
                    this.CurrentState = States.Opened;
                    break;
                case States.Opened:
                    break;
                case States.Closing:
                    this.CurrentState = States.Closed;
                    break;
                case States.Closed:
                    break;
                default:
                    throw new System.NotSupportedException(this.name + " trying to change to a not supported state");
            }
        }
        #endregion
    }
}
