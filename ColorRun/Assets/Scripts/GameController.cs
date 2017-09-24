using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour {

    #region FIELD
    public static GameController _instance;
    [SerializeField]
    private List<NodePosition> listNodePosition;
    [SerializeField]
    private Button buttonLeft;
    [SerializeField]
    private Button buttonRight;
    [SerializeField]
    private GameObject playerObject;

    private NodePosition nextLeftNode = new NodePosition();
    private NodePosition nextRightNode = new NodePosition();
    private Vector3 nextRightPos = new Vector3();
    private Vector3 nextLeftPos = new Vector3();

    private bool isCanSwap = false;

    [Header("Debug")]
    [SerializeField]
    private float jumpDuration = 0.5f;
    #endregion
    #region ENUM
    public enum LandType
    {
        LEFT,
        RIGHT
    }
    #endregion
    #region PROPERTY
    public static GameController Instance { get { return _instance; } }
    public List<NodePosition> ListNodePositions
    {
        get { return this.listNodePosition; }
        set
        {
            this.listNodePosition = value;
        }
    }
    #endregion
    #region MONO
    private void Awake()
    {
        _instance = this;

        this.playerObject.SetActive(false);
    }

    private void OnEnable()
    {
        this.buttonLeft.onClick.AddListener(this.onMoveLeft);
        this.buttonRight.onClick.AddListener(this.onMoveRight);
    }

    private void OnDisable()
    {
        this.buttonLeft.onClick.RemoveListener(this.onMoveLeft);
        this.buttonRight.onClick.RemoveListener(this.onMoveRight);
    }

    private void Update()
    {
        if (this.isCanSwap)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
                this.onMoveLeft();
            if (Input.GetKeyUp(KeyCode.RightArrow))
                this.onMoveRight();
        }
    }
    #endregion
    #region METHOD
    public void checkInitPlayer()
    {
        if (!this.isCanSwap && this.listNodePosition.Count > 0)
        {
            this.isCanSwap = true;
            Vector3 targetPoint = new Vector3(this.listNodePosition[0].transform.position.x, this.listNodePosition[0].transform.position.y, this.listNodePosition[0].transform.position.z - 1);
            this.playerObject.transform.position = targetPoint;
            this.playerObject.SetActive(true);
        }
    }

    private void onMoveRight()
    {
        if (!this.isCanSwap) return;

        this.findNextNodePosition();
        if (this.nextRightNode == null)
        {
            Debug.Log("Died");
            Debug.Break();
        } 
        Vector3 targetJump = new Vector3(this.nextRightNode.transform.position.x, this.nextRightNode.transform.position.y, this.nextRightNode.transform.position.z - 1);
        this.playerObject.transform.DOJump(targetJump, 1f, 1, this.jumpDuration);
        
        for(int i = 0; i < this.listNodePosition.Count; i++)
        {
            if (this.listNodePosition[i].transform.position.y <= this.nextRightNode.transform.position.y)
                this.listNodePosition.Remove(this.listNodePosition[i]);
        }
    }

    private void onMoveLeft()
    {
        if (!this.isCanSwap) return;

        this.findNextNodePosition();
        if (this.nextLeftNode == null) Debug.Break();
        Vector3 targetJump = new Vector3(this.nextLeftNode.transform.position.x, this.nextLeftNode.transform.position.y, this.nextLeftNode.transform.position.z - 1);
        this.playerObject.transform.DOJump(targetJump, 1f, 1, this.jumpDuration);

        for (int i = 0; i < this.listNodePosition.Count; i++)
        {
            if (this.listNodePosition[i].transform.position.y <= this.nextLeftNode.transform.position.y)
                this.listNodePosition.Remove(this.listNodePosition[i]);
        }
    }

    private void findNextNodePosition()
    {
        this.nextLeftNode = this.listNodePosition.Find(node => node.LantypeCurrent == LandType.LEFT && node.transform.position.y > this.playerObject.transform.position.y);
        this.nextRightNode = this.listNodePosition.Find(node => node.LantypeCurrent == LandType.RIGHT && node.transform.position.y > this.playerObject.transform.position.y);

        if (this.nextLeftNode == null) return;
        this.nextLeftPos = this.nextLeftNode.transform.position;
        if (this.nextRightNode == null) return;
        this.nextRightPos = this.nextRightNode.transform.position;

        if (this.nextLeftNode.transform.position.y > this.nextRightNode.transform.position.y)
            this.nextLeftNode = null;
        else if (this.nextLeftNode.transform.position.y < this.nextRightNode.transform.position.y)
            this.nextRightNode = null;
    }
    #endregion
}
