using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
    [SerializeField] private MazeGenerator mazeGenerator;
    [SerializeField] private int minX;
    [SerializeField] private int minZ;

    private void Start()
    {
        transform.position = new Vector3(mazeGenerator.MazeWidth / 2,GetBiggestNumber(),mazeGenerator.MazeWidth / 2);
    }

    private int GetBiggestNumber()
    {
        var newY = mazeGenerator.MazeHeight < mazeGenerator.MazeWidth ? mazeGenerator.MazeWidth : mazeGenerator.MazeHeight;

        if (newY <= 10)
        {
            newY = 11;
        }

        return newY;
    }
}
