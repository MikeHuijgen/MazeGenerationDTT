using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell mazeCellPrefab;
    [SerializeField] private int mazeWidth;
    [SerializeField] private int mazeHeight;

    private MazeCell[,] _mazeGrid;

    private void Start()
    {
        GenerateMazeGrid();
    }

    private void GenerateMazeGrid()
    {
        _mazeGrid = new MazeCell[mazeWidth, mazeHeight];

        for (var x = 0; x < mazeWidth; x++)
        {
            for (var z = 0; z < mazeHeight; z++)
            {
                _mazeGrid[x, z] = Instantiate(mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }
}
