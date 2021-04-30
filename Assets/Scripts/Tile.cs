using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    bool isClicked = false;
    public bool is3dScene = false;
    public TileSpawner tileSpawner;
    // Start is called before the first frame update
    void Start()
    {
        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set3DScene()
    {
        gameObject.GetComponent<Animator>().SetBool("is3dScene", true);
        //is3dScene = true;
    }
    public void Set2DScene()
    {
        gameObject.GetComponent<Animator>().SetBool("is3dScene", false);
        is3dScene = false;
    }
    private void OnMouseDown()
    {
        isClicked = true;
        tileSpawner.UnselectTile();
        //Debug.Log("OnMouseDown");
        if(gameObject.tag == "Start tile" || gameObject.tag == "End tile")
            tileSpawner.CheckButtons();
     
    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1)){
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
            //Debug.Log("Tile pos " + gameObject.transform.position);
            if (gameObject.tag != "Coin")
                tileSpawner.DeleteTile(gameObject.transform.position);
            if (gameObject.tag == "Start tile" || gameObject.tag == "End tile")
                tileSpawner.CheckButtons();
            Destroy(gameObject);
            //Debug.Log("Destroyed");
        }

    }
}
