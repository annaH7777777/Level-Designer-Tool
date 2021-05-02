using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareScript : MonoBehaviour
{
    TileSpawner tileSpawner;
    bool isClicked = false;

    void Start()
    {
        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    //private void OnMouseEnter()
    //{
    //    Debug.Log("squarescript OnMouseEnter");
    //    tileSpawner.AttemptToPlaceTileAt(tileSpawner.GetSquareClicked());
    //}

}
