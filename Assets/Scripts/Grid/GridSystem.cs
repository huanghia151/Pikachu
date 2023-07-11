using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : GridAbstract
{
    [Header("Grid System")]
    public int width = 18;
    public int height = 11;
    public float offsetX = 0.19f;
    public BlockProfile blockProfile;
    public List<Node> nodes;
    public List<int> nodeIds;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        InitGridSystem();
        LoadBlockProfile();
    }

    protected virtual void LoadBlockProfile()
    {
        if (blockProfile != null) return;
        blockProfile = Resources.Load<BlockProfile>("Pikachu");
        Debug.Log(transform.name + "LoadBlockProfile", gameObject);
    }
    protected override void Start()
    {
        SpawnBlocks();
        FindNodesNeighbors();
        FindBlocksNeighbors();
    }

    protected virtual void FindNodesNeighbors()
    {
        int x, y;
        foreach (Node node in nodes)
        {
            x = node.x;
            y = node.y;
            node.up = GetNodeByXY(x,y+1);
            node.right = GetNodeByXY(x + 1, y);
            node.down = GetNodeByXY(x, y - 1);
            node.left = GetNodeByXY(x - 1, y);
        }
    }

    protected virtual Node GetNodeByXY(int x,int y)
    {
        foreach(Node node in nodes)
        {
            if (node.x == x && node.y == y) return node;
        }
        return null;
    }

    protected virtual void FindBlocksNeighbors()
    {
        foreach(Node node in nodes)
        {
            if (node.blockCtrl == null) continue;
            node.blockCtrl.neighbors.Add(node.up.blockCtrl);
            node.blockCtrl.neighbors.Add(node.right.blockCtrl);
            node.blockCtrl.neighbors.Add(node.down.blockCtrl);
            node.blockCtrl.neighbors.Add(node.left.blockCtrl);
        }
    }
    protected virtual void InitGridSystem()
    {
        if (nodes.Count > 0) return;

        int nodeId = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Node node = new Node
                {
                    x = x,
                    y = y,
                    posX = x - (offsetX * x),
                    nodeId = nodeId,
                };
                nodes.Add(node);
                nodeIds.Add(nodeId);
                nodeId++;
            }
        }
    }
    protected virtual void SpawnNodes()
    {
        Vector3 pos = Vector3.zero;
        foreach (Node node in nodes)
        {
            if (node.x == 0) continue;
            if (node.y == 0) continue;
            if (node.x == width - 1) continue;
            if (node.y == height - 1) continue;
            pos.x = node.posX;
            pos.y = node.y;
            Transform block = this.ctrl.blockSpawner.Spawn(BlockSpawner.BLOCK, pos, Quaternion.identity);
            BlockCtrl blockCtrl = block.GetComponent<BlockCtrl>();

            block.gameObject.SetActive(true);
        }
    }

    protected virtual void SpawnBlocks()
    {
        Vector3 pos = Vector3.zero;
        int blockCount = 4;
        foreach (Sprite sprite in blockProfile.sprites)
        {
            for (int i = 0; i < blockCount; i++)
            {
                Node node = GetRandomNode();
                pos.x = node.posX;
                pos.y = node.y;
                Transform block = this.ctrl.blockSpawner.Spawn(BlockSpawner.BLOCK, pos, Quaternion.identity);
                BlockCtrl blockCtrl = block.GetComponent<BlockCtrl>();
                blockCtrl.blockData.SetSprite(sprite);
                LinkNodeBlock(node, blockCtrl);
                block.name = "Block_" + node.x.ToString() + "_" + node.y.ToString();

                block.gameObject.SetActive(true);
            }
        }
    }

    protected virtual Node GetRandomNode()
    {
        Node node;
        int randId;
        int nodeCount = nodes.Count;
        for(int i = 0;i<nodeCount;i++)
        {
            randId = Random.Range(0, nodeIds.Count);
            node = nodes[nodeIds[randId]];
            nodeIds.RemoveAt(randId);

            if (node.x == 0) continue;
            if (node.y == 0) continue;
            if (node.x == width - 1) continue;
            if (node.y == height - 1) continue;
            if (node.blockCtrl == null) return node;
        }
        Debug.LogWarning("Node can't found");
        return null;
    }
    protected virtual void LinkNodeBlock(Node node, BlockCtrl blockCtrl)
    {
        blockCtrl.blockData.SetNode(node);
        node.blockCtrl = blockCtrl;
    }

}
