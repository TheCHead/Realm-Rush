using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] int numberOfTowers = 3;
    [SerializeField] Tower towerPrefab;

    Queue<Tower> myTowers = new Queue<Tower>();


    public void PlaceTower(Block block)
    {
        
        if (myTowers.Count < numberOfTowers)
        {
            InstantiateTower(block);
        }
        else
        {
            MoveTower(block);
        }
    }

    private void InstantiateTower(Block block)
    {
        Tower newTower = Instantiate(towerPrefab, block.transform.position, Quaternion.identity);
        newTower.transform.parent = transform;
        block.isPlaceable = false;
        newTower.SetBaseBlock(block);
        myTowers.Enqueue(newTower);
    }

    private void MoveTower(Block block)
    {
        Tower towerToMove = myTowers.Dequeue();
        towerToMove.GetBaseBlock().isPlaceable = true;
        
        towerToMove.transform.position = block.transform.position;
        towerToMove.SetBaseBlock(block);
        block.isPlaceable = false;

        myTowers.Enqueue(towerToMove);
    }
}
