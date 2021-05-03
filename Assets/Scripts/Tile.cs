using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    bool isClicked = false;
    public bool is3dScene = false;
    bool hasCoin = false;
    public TileSpawner tileSpawner;
    public GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetHasCoin(bool b)
    {
        hasCoin = b;
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
        tileSpawner.UnselectTile();
        //Debug.Log("OnMouseDown");
        if (gameObject.tag == "Start tile" || gameObject.tag == "End tile")
            tileSpawner.CheckButtons();

    }
    private void OnMouseEnter()
    {
        Debug.Log("tilespawner getCoinSelect" + tileSpawner.GetCoinSelected());
        Debug.Log("has coin " + hasCoin);
        if (tileSpawner.GetCoinSelected() && !hasCoin)
        {
            GameObject coin = Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation) as GameObject;
            coin.GetComponent<Coin>().SetTile(this);
            hasCoin = true;
        }
    }
    private void OnMouseOver()
    {
        //Debug.Log("OnMouseOver " + gameObject.transform.position);
        
        if (!is3dScene && Input.GetMouseButtonDown(1))
        {
            if (gameObject.tag != "Coin")
                tileSpawner.DeleteTile(gameObject.transform.position);
            if (gameObject.tag == "Start tile" || gameObject.tag == "End tile")
                tileSpawner.CheckButtons();
            Destroy(gameObject);
        }
    }

 

    private void OnMouseExit()
    {
        if (isClicked == false)
        {
            //Debug.Log("OnMouseExit  " + gameObject.transform.position);
            if (gameObject.tag != "Coin")
                tileSpawner.DeleteTile(gameObject.transform.position);
            if (gameObject.tag == "Start tile" || gameObject.tag == "End tile")
                tileSpawner.CheckButtons();
            Destroy(gameObject);
            //Debug.Log("Destroyed");
        }

    }
}
