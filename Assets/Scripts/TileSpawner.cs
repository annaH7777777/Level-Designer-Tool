using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TileSpawner : MonoBehaviour
{
    public Tile tile;
    GameObject tileInstance;
    Grid grid;
    public Button startButton;
    bool isCoinSelected = false;
    public CanvasRenderer panel;
    GameObject[] allObjects;
    public Text codeText;


    private void Start()
    {
        grid = new Grid(8, 8, 1f);
    }
    public void SetAllGameObjects() //maybe another script
    {
        GameObject[] startTiles = GameObject.FindGameObjectsWithTag("Start tile");
        GameObject[] endTiles = GameObject.FindGameObjectsWithTag("End tile");
        GameObject[] platformTiles = GameObject.FindGameObjectsWithTag("Platform");
        GameObject[] movingPlatformTiles = GameObject.FindGameObjectsWithTag("Moving Platform");
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        allObjects = startTiles.Concat(endTiles).Concat(platformTiles).Concat(movingPlatformTiles).Concat(coins).ToArray();
    }
    public GameObject[] GetGameObjects() //maybe another script
    {
        return allObjects;
    }

    public void GameObjectsToString() //maybe another script
    {
        string text = "";
        foreach (GameObject tile in allObjects)
        {
            text += tile.tag.ToString();
            text += "-";
            text += tile.transform.position.ToString();
            text += "; ";
        }
        codeText.text = StringProvider.Encode(text);
        Debug.Log(StringProvider.Decode(codeText.text));
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
        if (tile != null && tile.tag != "Coin" && grid.PlaceTile((int)(pos.x), (int)(pos.z), tile))
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
        else if(tile != null && tile.tag == "Coin" && grid.GetTile((int)(pos.x), (int)(pos.z)) != null)
        {
            //Debug.Log("Spawn tile coin");
            tileInstance = Instantiate(tile.gameObject, new Vector3(pos.x, 1.5f, pos.z), Quaternion.identity) as GameObject;
        }
        else
        {
            //Debug.Log(grid.GetTile((int)(pos.x), (int)(pos.z)) + "Cant spawn");
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
    public Tile GetTile(int x, int y)
    {
       return grid.GetTile(x, y);
    }
}
