using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCtrl : SaiMonoBehaviour
{
    [Header("Block Ctrl")]
    public SpriteRenderer sprite;
    public BlockData blockData;
    public List<BlockCtrl> neighbors= new List<BlockCtrl>();
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadBlockData();
    }

    protected virtual void LoadModel()
    {
        if (sprite != null) return;
        Transform model = transform.Find("Model");
        sprite = model.GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + "LoadModel", gameObject);
    }

    protected virtual void LoadBlockData()
    {
        if (blockData != null) return;
        blockData = transform.Find("BlockData").GetComponent<BlockData>();
        Debug.Log(transform.name + "LoadBlockData", gameObject);
    }
}
