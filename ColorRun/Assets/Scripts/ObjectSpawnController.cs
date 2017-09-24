using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour {

    #region FIELD
    [SerializeField]
    private List<GroundBlockControl> blocks;
    [SerializeField]
    private float smallWait = 0.2f;
    [SerializeField]
    private float largerWait = 0.6f;
    [SerializeField]
    private List<Transform> positions;
    #endregion
    #region PROPERTY
    #endregion
    #region MONO
    private void Start()
    {
        StartCoroutine(this.SpawnBegin());
    }
    #endregion
    #region METHOD
    /// <summary>
    /// Spawn block and trap
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnBegin()
    {
        while (true)
        {
            int landIndex = UnityEngine.Random.Range(0, positions.Count);//0 == Left, 1 == RIGHT
            Transform spawnPosition = this.positions[landIndex];
            int index = UnityEngine.Random.Range(0, this.blocks.Count);

            if (this.blocks[index].BlockTypeCurrent == GroundBlockControl.BlockType.SMALL)
                yield return new WaitForSeconds(smallWait);
            else
                yield return new WaitForSeconds(largerWait);
            
            GroundBlockControl blockSpawned = this.blocks[index].Spawn(spawnPosition.position, Quaternion.identity);
            if (landIndex == 0)
                blockSpawned.LandTypeCurrent = GameController.LandType.LEFT;
            else
                blockSpawned.LandTypeCurrent = GameController.LandType.RIGHT;

            //Spawn block in safe land to avoid trap
            if (this.isSetTrap())
            {
                blockSpawned.SetTrap(true);

                GroundBlockControl blockSafe = new GroundBlockControl();
                if (landIndex == 0)
                {
                    blockSafe = this.blocks[index].Spawn(this.positions[1].position, Quaternion.identity);
                    blockSafe.LandTypeCurrent = GameController.LandType.RIGHT;
                }
                else
                {
                    blockSafe = this.blocks[index].Spawn(this.positions[0].position, Quaternion.identity);
                    blockSafe.LandTypeCurrent = GameController.LandType.LEFT;
                }
            }

            if (this.blocks[index].BlockTypeCurrent == GroundBlockControl.BlockType.SMALL)
                yield return new WaitForSeconds(smallWait);
            else
                yield return new WaitForSeconds(largerWait);
        }
    }

    /// <summary>
    /// Random trap
    /// </summary>
    /// <returns></returns>
    private bool isSetTrap()
    {
        int rd = Random.Range(0, 10);
        if (rd < 2)
            return true;
        return false;
    }
    #endregion
}
