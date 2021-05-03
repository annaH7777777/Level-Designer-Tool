using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    bool isClicked = false;
    bool is3dScene = false;
    TileSpawner tileSpawner;
    Tile tile;

    private void Start()
    {
        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    public void SetTile(Tile tile)
    {
        this.tile = tile;
    }

    public void SetAnimation(bool b)
    {
        gameObject.GetComponent<Animator>().SetBool("is3dScene", b);
    }

    public void SetIs3DScene(bool b)
    {
        is3dScene = b;
    }
    
    private void OnMouseDown()
    {
        isClicked = true;
        tileSpawner.SetCoinUnselected();
    }
    private void OnMouseOver()
    {
        if (!is3dScene && Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
            tile.SetHasCoin(false);
            tileSpawner.SetCoinUnselected();
        }
    }

    private void OnMouseExit()
    {
        if (isClicked == false)
        {
            Destroy(gameObject);
            tile.SetHasCoin(false);
        }
    }
}
