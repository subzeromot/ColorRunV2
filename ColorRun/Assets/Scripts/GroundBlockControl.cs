using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlockControl : MonoBehaviour {

    #region FIELD
    [SerializeField]
    private BlockType blockType = BlockType.SMALL;
    [SerializeField]
    private List<NodePosition> listNodes;
    [SerializeField]
    private List<TrapControl> listTraps;
    [SerializeField]
    private GameController.LandType landType = GameController.LandType.RIGHT;

    private TrapControl currentTrap;
    private bool isSpawnTrap = false;
    #endregion
    #region ENUM
    public enum BlockType
    {
        SMALL,
        LARGER
    }
    #endregion
    #region PROPERTY
    public BlockType BlockTypeCurrent { get { return this.blockType; } }
    public List<NodePosition> ListNodes { get { return this.listNodes; } }
    public bool IsSpawnTrap { get { return this.isSpawnTrap; } set { this.isSpawnTrap = value; } }
    public GameController.LandType LandTypeCurrent
    {
        get { return this.landType; }
        set
        {
            this.landType = value;
            for(int i = 0; i < this.listNodes.Count; i++)
            {
                this.listNodes[i].LantypeCurrent = value;
            }
        }
    }
    #endregion
    #region MONO
    private void OnBecameInvisible()
    {
        this.gameObject.Recycle();
        this.currentTrap = null;
    }

    #endregion
    #region METHOD
    public void SetTrap(bool isSet)
    {
        if (isSet)
        {
            int rdTrap = UnityEngine.Random.Range(0, this.listTraps.Count);
            int rdNode = UnityEngine.Random.Range(0, this.listNodes.Count);
            Vector3 pos = new Vector3(this.listNodes[rdNode].transform.position.x, this.listNodes[rdNode].transform.position.y, -1);
            this.currentTrap = this.listTraps[rdTrap].Spawn(pos, Quaternion.identity);
        }
    }
    #endregion
}
