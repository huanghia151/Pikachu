using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerCtrl : SaiMonoBehaviour
{
    [Header("Grid Manager Ctrl")]
    private static GridManagerCtrl instance;
    public static GridManagerCtrl Instance => instance;

    public BlockSpawner blockSpawner;
    public IPathFinding pathfinding;
    public BlockCtrl firstBlock;
    public BlockCtrl lastBlock;
    protected override void Awake()
    {
        base.Awake();
        if (GridManagerCtrl.instance != null) Debug.LogError("Only 1 GridManagerCtrl allow to exist");
        GridManagerCtrl.instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpawner();
        LoadPathfinding();
    }
    protected virtual void LoadSpawner()
    {
        if (blockSpawner != null) return;
        blockSpawner = transform.Find("BlockSpawner").GetComponent<BlockSpawner>();
        Debug.Log(transform.name + "LoadSpawner", gameObject);
    }
    protected virtual void LoadPathfinding()
    {
        if (pathfinding != null) return;
        pathfinding = transform.GetComponentInChildren<IPathFinding>();
        Debug.Log(transform.name + "LoadPathfinding", gameObject);
    }

    
    public virtual void SetNode(BlockCtrl blockCtrl)
    {
        if(firstBlock != null && lastBlock != null)
        {
            pathfinding.FindPath(firstBlock, lastBlock);

            firstBlock = null;
            lastBlock = null;
            Debug.Log("Reset blocks");
            return;
        }

        if(firstBlock == null)
        {
            firstBlock = blockCtrl;
            return;
        }

        lastBlock = blockCtrl;
    }
}
