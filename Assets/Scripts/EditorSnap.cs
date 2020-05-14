using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Block))]

public class EditorSnap : MonoBehaviour
{
    [SerializeField] TextMesh label;
    Block block;
    Vector3 snapPos;
    int gridSize;

    private void Awake()
    {
        block = GetComponent<Block>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gridSize = block.GetGridSize();
    }

    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        snapPos = new Vector3(block.GetGridPos().x * gridSize, 0f, block.GetGridPos().y * gridSize);
        transform.position = snapPos;
    }

    private void UpdateLabel()
    {
        string labelText = (snapPos.x / gridSize).ToString() + ',' + (snapPos.z / gridSize).ToString();
        label.text = labelText;
        gameObject.name = labelText;
    }
}
