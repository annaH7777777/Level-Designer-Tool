using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] Tile startTile;
    [SerializeField] Tile endTile;
    Tile tile;
    // Start is called before the first frame update
    void Start()
    {
        tile = startTile;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Debug.Log("tile " +tile);
        FindObjectOfType<TileSpawner>().SetSelectedTile(tile);
    }
    
    //private void OnMouseDown()
    //{
    //    var buttons = FindObjectsOfType<ButtonScript>();
    //    foreach (ButtonScript button in buttons)
    //    {
    //        button.GetComponent<SpriteRenderer>().color = new Color32(41, 41, 41, 255);
    //    }
    //    GetComponent<SpriteRenderer>().color = Color.white;
    //    FindObjectOfType<TileSpawner>().SetSelectedDefender(tile);
    //}
}
