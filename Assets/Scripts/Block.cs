using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Vector2Int gridPos;
    const int gridSize = 10;
    public bool isQueued = false;
    [SerializeField] Block exploredFrom;
    public bool isPlaceable = true;


    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    public void SetExloredFromBlock(Block block)
    {
        exploredFrom = block;
    }

    public Block GetExploredFromBlock()
    {
        return exploredFrom;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isPlaceable == true)
        {
            FindObjectOfType<TowerSpawner>().PlaceTower(this);
        }
    }

    public void MakeBlockPlacable()
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.enabled = true;
        isPlaceable = true;
    }

    public void MakeBlockNotPlacable()
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.enabled = false;
        isPlaceable = false;
    }
}
