using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstSearch : GridAbstract, IPathFinding
{
    [Header("BFS")]
    public Queue<BlockCtrl> queue = new Queue<BlockCtrl>();
    public Dictionary<BlockCtrl, BlockCtrl> cameFrom = new Dictionary<BlockCtrl, BlockCtrl>();

    public virtual void FindPath(BlockCtrl startBlock, BlockCtrl targetBlock)
    {
        Debug.Log("FindPath");

        queue.Enqueue(startBlock);
        cameFrom[startBlock] = startBlock;

        while (queue.Count > 0)
        {
            BlockCtrl current = queue.Dequeue();

            if (current == targetBlock)
            {
                break;
            }

            foreach (BlockCtrl neighbor in current.neighbors)
            {
                if(IsValidPosition(neighbor) && !cameFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }            
            }
        }
        ShowCameFrom();
    }

    protected virtual void ShowCameFrom()
    {
        foreach(var pair in cameFrom)
        {
            BlockCtrl key = pair.Key;
            BlockCtrl value = pair.Value;

            Debug.Log("Left: " + key.ToString() + ",Right: " + value.ToString());
        }
    }

    private bool IsValidPosition(BlockCtrl block)
    {
        return true;
    }
}
