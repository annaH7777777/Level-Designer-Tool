using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TileSpawner : MonoBehaviour
{
    public Tile tile;
    GameObject tileInstance;
    Grid grid;
    public Button startButton;
    bool isCoinSelected = false;

    private void Start()
    {
        grid = new Grid(8, 8, 1f);
    }

    public void CheckButtons()
    {
        if (grid.HasStartTile() && !grid.HasEndTile())
        {
            startButton.GetComponent<StartButtonScript>().SetEndText();
            startButton.gameObject.SetActive(true);
        }
        else if (grid.HasStartTile() && grid.HasEndTile())
            startButton.gameObject.SetActive(false);
        else
        {
            startButton.GetComponent<StartButtonScript>().SetStartText();
            startButton.gameObject.SetActive(true);
        }
    }

    public Grid GetGrid()
    {
        return grid;
    }


    public void SetSelectedTile(Tile tileToSelect)
    {
       tile = tileToSelect;
       isCoinSelected = false;
    }
    public void SetCoinSelected()
    {
        isCoinSelected = true;
        tile = null;
    }
    public void SetCoinUnselected()
    {
        isCoinSelected = false;
    }
    public bool GetCoinSelected()
    {
        return isCoinSelected;
    }

    public void UnselectTile()
    {
        tile = null;
    }

    private void OnMouseOver()
    {
        AttemptToPlaceTileAt(GetSquareClicked());
    }

    public void AttemptToPlaceTileAt(Vector3 gridPos)
    {
        SpawnTile(gridPos);
    }

    public Vector3 GetSquareClicked()
    {
        Vector3 clickPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector3 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }

    private Vector3 SnapToGrid(Vector3 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newZ = Mathf.RoundToInt(rawWorldPos.z);
        return new Vector3(newX, 0, newZ);
    }

    private void SpawnTile(Vector3 pos)
    {
        if (tile != null && grid.PlaceTile((int)(pos.x), (int)(pos.z), tile))
        {
            tileInstance = Instantiate(tile.gameObject, pos, Quaternion.identity) as GameObject;
            if (tileInstance.tag == "Moving Platform")
            {
                foreach (Transform child in tileInstance.transform)
                {
                    if (child.tag == "Moving")
                        child.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
        else
        {
            return;
        }
    }

    public void DeleteTile(Vector3 pos)
    {
        grid.DeleteTile((int)pos.x, (int)pos.z);
    }

    public Tile GetTile(int x, int y)
    {
       return grid.GetTile(x, y);
    }
}
