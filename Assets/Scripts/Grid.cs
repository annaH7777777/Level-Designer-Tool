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
    public GameObject linePrefab;
    public bool hasStartTile = false;
    public bool hasEndTile = false;

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
                //if (x != 0)
                   //Instantiate(linePrefab, new Vector2(x-0.5f, y), new Vector2(0, 0));
                 //Handles.DrawLine(GetWoldPosition(x - 0.5f, y - 0.5f), GetWoldPosition(x - 0.5f, y + 0.5f));
                //Debug.DrawLine(GetWoldPosition(x-0.5f, y-0.5f), GetWoldPosition(x-0.5f, y + 0.5f), Color.white, 100f);
                //if (y != 0)
                   // Handles.DrawLine(GetWoldPosition(x - 0.5f, y - 0.5f), GetWoldPosition(x + 0.5f, y - 0.5f));
                //Debug.DrawLine(GetWoldPosition(x-0.5f, y-0.5f), GetWoldPosition(x + 0.5f, y-0.5f), Color.white, 100f);
                gridArray[x, y] = null;
            }
        }
    }
    private void Start()
    {

    }
    private Vector3 GetWoldPosition(float x, float y)
    {
        return new Vector3(x, 0, y) * cellSize;
    }
    public Tile GetTile(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < gridArray.GetLength(0) && y < gridArray.GetLength(1))
            return gridArray[x, y];
        else
            return null;
    }
    public bool PlaceTile(int x, int y, Tile tile)
    {
        //Debug.Log("place tile x " + x);
        //Debug.Log("place tile y " + y);
        //Debug.Log("wid " + gridArray.GetLength(0));
        //Debug.Log("height " + gridArray.GetLength(1));
        if (x >= 0 && y >= 0 && x < gridArray.GetLength(0) && y < gridArray.GetLength(1) && tile.tag != "Coin" && gridArray[x, y] == null && CheckTileInGrid(tile))
        {
            gridArray[x, y] = tile;
            Debug.Log("Place Tile " +tile + x +" " + y);
            return true;
        }
        return false;
    }
    public void DeleteTile(int x, int y)
    {
        //Debug.Log("delete tile x " + x);
        //Debug.Log("delete tile y " + y);
        //Debug.Log("Delete tile " + gridArray[x, y]);
        if (x >= 0 && y >= 0 && x < gridArray.GetLength(0) && y < gridArray.GetLength(1) && gridArray[x, y] != null)
        {
            if (gridArray[x, y].tag == "Start tile")
            {
                //Debug.Log("Delete tile");
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
            Debug.Log("Check tile");
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
