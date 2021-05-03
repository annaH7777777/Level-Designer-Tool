using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] Tile startTile;
    [SerializeField] Tile endTile;
    TileSpawner tileSpawner;
    Tile tile;

    void Start()
    {
        tile = startTile;
        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    public void SetEndTile()
    {
        tile = endTile;
    }

    public void SetStartTile()
    {
        tile = startTile;
    }

    public void SetTile()
    {
        tileSpawner.SetSelectedTile(tile);
    }
}
