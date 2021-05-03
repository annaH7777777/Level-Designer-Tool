using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grid
{
    private int width;
    private int heigth;
    private Tile[,] gridArray;
    private float cellSize;
    private bool hasStartTile = false;
    private bool hasEndTile = false;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.heigth = heigth;
        this.cellSize = cellSize;

        gridArray = new Tile[width, height];
        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = null;
            }
        }
    }
    private bool CheckCoordinates(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < gridArray.GetLength(0) && y < gridArray.GetLength(1))
            return true;
        else
            return false;
    }
    public Tile GetTile(int x, int y)
    {
        if (CheckCoordinates(x,y))
            return gridArray[x, y];
        else
            return null;
    }
    public bool PlaceTile(int x, int y, Tile tile)
    {
        if (CheckCoordinates(x, y) && gridArray[x, y] == null && CheckTileInGrid(tile))
        {
            gridArray[x, y] = tile;
            return true;
        }
        return false;
    }
    public void DeleteTile(int x, int y)
    {
        if (CheckCoordinates(x, y) && gridArray[x, y] != null)
        {
            if (gridArray[x, y].tag == "Start tile")
            {
                hasStartTile = false;
            }
            else if (gridArray[x, y].tag == "End tile")
                hasEndTile = false;
            gridArray[x, y] = null;
        }
    }
    public bool CheckTileInGrid(Tile tile)
    {
        if (!hasStartTile && tile.tag == "Start tile")
        {
            hasStartTile = true;
        }
        else if (hasStartTile && tile.tag == "Start tile")
        {
            return false;
        }
        else if (!hasEndTile && tile.tag == "End tile")
        {
            hasEndTile = true;
        }
        else if (hasEndTile && tile.tag == "End tile")
        {
            return false;
        }
        return true;
    }
    public bool HasStartTile()
    {
        return hasStartTile;
    }
    public bool HasEndTile()
    {
        return hasEndTile;
    }
}
