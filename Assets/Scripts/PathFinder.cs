using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Dictionary<Vector2Int, Block> grid = new Dictionary<Vector2Int, Block>();
    [SerializeField] Block startBlock;
    [SerializeField] Block endBlock;
    [SerializeField] bool isRunning = true;

    List<Block> shortestPath = new List<Block>();

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    Queue<Block> queue = new Queue<Block>();



    public List<Block> GetShortestPath()
    {
        if (shortestPath.Count == 0)
        {
            LoadBlocks();
            BreadthFirstSearch();
            FindShortestPath();
        }
        return shortestPath;
    }

    private void LoadBlocks()
    {
        var blocks = FindObjectsOfType<Block>();
        foreach (Block block in blocks)
        {
            bool isOverlaping = grid.ContainsKey(block.GetGridPos());
            if (isOverlaping)
            {
                Debug.LogWarningFormat("Block overlaping: {0}", block);
            }
            else
            {
                grid.Add(block.GetGridPos(), block);
            }
        }
    }



    private void BreadthFirstSearch()
    {
        queue.Enqueue(startBlock);
        while (queue.Count > 0 && isRunning)
        {
            var searchCenter = queue.Dequeue();
            searchCenter.isQueued = true;
            Debug.Log("Checking " + searchCenter);
            SearchEndBlock(searchCenter);
        }
    }

    private void SearchEndBlock(Block searchCenter)
    {
        if (searchCenter.GetGridPos() == endBlock.GetGridPos())
        {
            Debug.LogFormat("Search completed. {0} is End block", searchCenter);
            isRunning = false;
        }
        else
        {
            Debug.Log("Searching from " + searchCenter);
            foreach (Block block in GetNeighbourBlocks(searchCenter))
            {
                if (!block.isQueued)
                {
                    queue.Enqueue(block);
                    block.isQueued = true;
                    Debug.Log(block + " is queued.");
                    block.SetExloredFromBlock(searchCenter);
                }
            }
        }
    }


    private List<Block> GetNeighbourBlocks(Block block)
    {
        List<Block> neighbourBlocks = new List<Block>();
        foreach(Vector2Int direction in directions)
        {
            Vector2Int exploringCoordinates = block.GetGridPos() + direction;
            try
            {
                neighbourBlocks.Add(grid[exploringCoordinates]);
            }
            catch
            {
                //do nothings
            }
        }
        return neighbourBlocks;
    }


    private void FindShortestPath()
    {
        Block currentBlock = endBlock;
        currentBlock.MakeBlockNotPlacable();
        while (currentBlock.GetExploredFromBlock())
        {
            shortestPath.Add(currentBlock);
            currentBlock = currentBlock.GetExploredFromBlock();
            currentBlock.MakeBlockNotPlacable();
        }
        shortestPath.Add(startBlock);
        shortestPath.Reverse();
    }
}
