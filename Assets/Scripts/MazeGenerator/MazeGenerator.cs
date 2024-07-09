using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell mazeCellPrefab;
    [SerializeField] private int mazeWidth;
    [SerializeField] private int mazeHeight;
    [SerializeField] private float mazeGenerationSpeed; 

    private MazeCell[,] _mazeGrid;

    public int MazeWidth => mazeWidth;
    public int MazeHeight => mazeHeight;

    IEnumerator Start()
    {
        GenerateMazeGrid();

        yield return GenerateMaze(null, _mazeGrid[0, 0]);
    }

    private IEnumerator GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell,currentCell);

        yield return new WaitForSeconds(mazeGenerationSpeed);

        MazeCell nextCell;
        
        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                yield return GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        var x = (int)currentCell.transform.position.x;
        var z = (int)currentCell.transform.position.z;

        if (x + 1 < mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];
            if (!cellToRight.IsVisited)
            {
                yield return cellToRight;
            }
        }
        
        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];
            if (!cellToLeft.IsVisited)
            {
                yield return cellToLeft;
            }
        }
        
        if (z + 1 < mazeHeight)
        {
            var cellToFront = _mazeGrid[x, z + 1];
            if (!cellToFront.IsVisited)
            {
                yield return cellToFront;
            }
        }
        
        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];
            if (!cellToBack.IsVisited)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null) return;

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }
        
        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }
        
        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }
        
        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
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
