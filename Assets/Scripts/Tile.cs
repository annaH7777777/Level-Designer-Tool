using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    bool isClicked = false;
    bool is3dScene = false;
    bool hasCoin = false;
    TileSpawner tileSpawner;
    [SerializeField] GameObject coinPrefab;
    GameObject coin;

    void Start()
    {
        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    public void SetHasCoin(bool b)
    {
        hasCoin = b;
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
        tileSpawner.UnselectTile();
        if (gameObject.tag == "Start tile" || gameObject.tag == "End tile")
            tileSpawner.CheckButtons();
    }
    private void OnMouseEnter()
    {
        if (tileSpawner.GetCoinSelected() && !hasCoin)
        {
            coin = Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation) as GameObject;
            coin.GetComponent<Coin>().SetTile(this);
            hasCoin = true;
        }
    }
    private void OnMouseOver()
    {
        if (!is3dScene && Input.GetMouseButtonDown(1))
        {
            if (hasCoin)
            {
                Destroy(coin);
                hasCoin = false;
            }
            else
            {
                tileSpawner.DeleteTile(gameObject.transform.position);
                if (gameObject.tag == "Start tile" || gameObject.tag == "End tile")
                    tileSpawner.CheckButtons();
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseExit()
    {
        if (isClicked == false)
        {
            tileSpawner.DeleteTile(gameObject.transform.position);
            if (gameObject.tag == "Start tile" || gameObject.tag == "End tile")
                tileSpawner.CheckButtons();
            Destroy(gameObject);
        }
    }
}
