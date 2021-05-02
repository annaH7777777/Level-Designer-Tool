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


    private void Start()
    {
        grid = new Grid(8, 8, 1f);

    }

    public Grid GetGrid()
    {
        return grid;
    }


    private void OnMouseOver() //moved to SquareScript
    {
        AttemptToPlaceTileAt(GetSquareClicked());
        Debug.Log("tilespawner OnMouseEnter");
    }



    public void SetSelectedTile(Tile tileToSelect)
    {
       tile = tileToSelect;
    }

    public void UnselectTile()
    {
        Debug.Log("Unselect Tile");
        tile = null;
    }

    public void AttemptToPlaceTileAt(Vector3 gridPos)
    {
        SpawnTile(gridPos);
    }

    public Vector3 GetSquareClicked()
    {
        //Debug.Log(Input.mousePosition.x);
        //Debug.Log(Input.mousePosition.y);
        //Debug.Log(Input.mousePosition.z);
        Vector3 clickPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector3 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }
    private Vector3 SnapToGrid(Vector3 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newZ = Mathf.RoundToInt(rawWorldPos.z);
        //Debug.Log(rawWorldPos.z);
        //Debug.Log(newZ);
        return new Vector3(newX, 0, newZ);
    }
    private void SpawnTile(Vector3 pos)
    {
        //Debug.Log((int)(pos.x));
        //Debug.Log((pos.x));
        //Debug.Log((int)(pos.z));
        //Debug.Log((pos.z));
        Debug.Log("pos for tile " + grid.GetTile((int)(pos.x), (int)(pos.z)));
        if (tile != null && tile.tag != "Coin" && grid.PlaceTile((int)(pos.x), (int)(pos.z), tile))
        {
            tileInstance = Instantiate(tile.gameObject, pos, Quaternion.identity) as GameObject;
            
        }
        else if(tile != null && tile.tag == "Coin" && grid.GetTile((int)(pos.x), (int)(pos.z)) != null)
        {
            Debug.Log("Spawn tile coin");
            tileInstance = Instantiate(tile.gameObject, new Vector3(pos.x, 1.5f, pos.z), Quaternion.identity) as GameObject;
        }
        else
        {
            Debug.Log(grid.GetTile((int)(pos.x), (int)(pos.z)) + "Cant spawn");
            return;
        }
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
    public void DeleteTile(Vector3 pos)
    {
        //Debug.Log("TileSpawner Delete Tile");
        grid.DeleteTile((int)pos.x, (int)pos.z);
    }
}
