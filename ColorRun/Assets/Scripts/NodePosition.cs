using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePosition : MonoBehaviour {


    #region FIELD
    [SerializeField]
    private GameController.LandType landType = GameController.LandType.RIGHT;

    private int rowIndex = 0;
    #endregion
    #region PROPERTY
    public GameController.LandType LantypeCurrent { get { return this.landType; } set { this.landType = value; } }
    public int RowIndex { get { return this.rowIndex; } }
    #endregion
    #region MONO
    private void OnEnable()
    {
        GameController.Instance.ListNodePositions.Add(this);
        GameController.Instance.checkInitPlayer();
    }

    private void OnDisable()
    {
        if (GameController.Instance.ListNodePositions.Contains(this))
            GameController.Instance.ListNodePositions.Remove(this);
    }
    #endregion
    #region METHOD
    #endregion
}
 