using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    bool isClicked = false;
    public bool is3dScene = false;
    public TileSpawner tileSpawner;
    Tile tile;

    public void SetTile(Tile tile)
    {
        this.tile = tile;
    }
    private void Start()
    {
        tileSpawner = FindObjectOfType<TileSpawner>();
        //tile = tileSpawner.GetTile((int)transform.position.x, (int)transform.position.z).GetComponent<Tile>();
        Debug.Log("Coin start" + tile.transform.position);
    }
    public void SetAnimationOn()
    {
        gameObject.GetComponent<Animator>().SetBool("is3dScene", true);

    }
    public void SetAnimationOff()
    {
        gameObject.GetComponent<Animator>().SetBool("is3dScene", false);
    }
    public void Set3DScene()
    {
        is3dScene = true;
    }
    public void Set2DScene()
    {
        is3dScene = false;
    }
    private void OnMouseDown()
    {
        //Debug.Log("OnMouseDown " + gameObject.transform.position);
        isClicked = true;
        tileSpawner.SetCoinUnselected();
    }
    private void OnMouseOver()
    {
        //Debug.Log("Coin OnMouseOver " + gameObject.transform.position);
        if (!is3dScene && Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
            tile.SetHasCoin(false);
            tileSpawner.SetCoinUnselected();

        }
    }

    private void OnMouseExit()
    {
        //Debug.Log("Coin OnMouseExit 1 " + gameObject.transform.position);
        if (isClicked == false)
        {
            Debug.Log("Coin OnMouseExit coin " + gameObject.transform.position);
            Destroy(gameObject);
            tile.SetHasCoin(false);
            Debug.Log("Coin OnMouseExit tile" + tile.transform.position);
        }
    }
}
