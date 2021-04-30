using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    public void SetEndText()
    {
       
        gameObject.GetComponentInChildren<Text>().text = "End";
        gameObject.GetComponent<Button>().image.color = Color.red;
        gameObject.GetComponent<ButtonScript>().SetEndTile();
    }
    public void SetStartText()
    {
       
        gameObject.GetComponentInChildren<Text>().text = "Start";
        gameObject.GetComponent<Button>().image.color = Color.green;
        gameObject.GetComponent<ButtonScript>().SetStartTile();
    }
}
