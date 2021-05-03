using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DimensionScript : MonoBehaviour
{
    public Camera m_OrthographicCamera;
    public Camera m_PerspectiveCamera;
    public List<Button> buttons;
    TileSpawner tileSpawner;
    public GameObject lines;
    public GameObject ballPrefab;
    private GameObject ball;
    GameObject[] allObjects;
    GameObject[] coinTiles;
    Text buttonText;
    public CanvasRenderer panel;
    public Text codeText;

    void Start()
    {
        m_OrthographicCamera.gameObject.SetActive(true);
        m_PerspectiveCamera.gameObject.SetActive(false);
        tileSpawner = FindObjectOfType<TileSpawner>();
        buttonText = gameObject.GetComponent<Button>().GetComponentInChildren<Text>();
    }

    private string GetButtonText()
    {
        return buttonText.text;
    }

    private void SetButtonText(string text)
    {
        buttonText.text = text;
    }

    private bool CheckStartEndTiles()
    {
        return (tileSpawner.GetGrid().HasStartTile() && tileSpawner.GetGrid().HasEndTile());
    }

    public void ChangeDimension()
    {
        if (GetButtonText() == "3D" && CheckStartEndTiles())
        {
            m_OrthographicCamera.gameObject.SetActive(false);
            m_PerspectiveCamera.gameObject.SetActive(true);
            SetButtonText("2D");
            lines.gameObject.SetActive(false);
            foreach(Button button in buttons)
            {
                button.gameObject.SetActive(false);
            }
            coinTiles = GameObject.FindGameObjectsWithTag("Coin");
            foreach(GameObject coinTile in coinTiles)
            {
                coinTile.GetComponent<Coin>().SetAnimation(true);
                coinTile.GetComponent<Coin>().SetIs3DScene(true);
            }
            GameObject[] platformTiles;
            platformTiles = GameObject.FindGameObjectsWithTag("Moving Platform");
            foreach (GameObject platformTile in platformTiles)
            {
                platformTile.GetComponent<Tile>().SetAnimation(true);
                platformTile.GetComponent<BoxCollider>().enabled = false;
                foreach (Transform child in platformTile.transform)
                {
                    if (child.tag == "Transparent")
                        child.gameObject.SetActive(false);
                    if (child.tag == "Moving")
                        child.gameObject.GetComponent<BoxCollider>().enabled = true;
                }
            }
            GameObject startTile = GameObject.FindGameObjectWithTag("Start tile");
            Vector3 startTilePos = startTile.transform.position;
            Vector3 ballPos = new Vector3(startTilePos.x, startTilePos.y + 1, startTilePos.z);
            ball = Instantiate(ballPrefab, ballPos, startTile.transform.rotation);
            Tile[] tiles = FindObjectsOfType<Tile>();
            foreach(Tile tile in tiles)
            {
                tile.SetIs3DScene(true);
            }
            SetAllGameObjects();
        }
        else
        {
            m_OrthographicCamera.gameObject.SetActive(true);
            m_PerspectiveCamera.gameObject.SetActive(false);
            SetButtonText("3D");
            lines.gameObject.SetActive(true);
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(true);
                tileSpawner.CheckButtons();
            }
            foreach (GameObject coinTile in coinTiles)
            {
                coinTile.gameObject.SetActive(true);
                coinTile.GetComponent<Coin>().SetAnimation(false);
                coinTile.GetComponent<Coin>().SetIs3DScene(false);
            }
            GameObject[] platformTiles;
            platformTiles = GameObject.FindGameObjectsWithTag("Moving Platform");
            foreach (GameObject platformTile in platformTiles)
            {
                platformTile.GetComponent<Tile>().SetAnimation(false);
                platformTile.GetComponent<BoxCollider>().enabled = true;
                foreach (Transform child in platformTile.transform)
                {
                    if (child.tag == "Transparent")
                        child.gameObject.SetActive(true);
                    if (child.tag == "Moving")
                        child.gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
            Tile[] tiles = FindObjectsOfType<Tile>();
            foreach (Tile tile in tiles)
            {
                tile.SetIs3DScene(false);
            }
            Destroy(ball);
        }
    }
    public void SetAllGameObjects()
    {
        GameObject[] startTiles = GameObject.FindGameObjectsWithTag("Start tile");
        GameObject[] endTiles = GameObject.FindGameObjectsWithTag("End tile");
        GameObject[] platformTiles = GameObject.FindGameObjectsWithTag("Platform");
        GameObject[] movingPlatformTiles = GameObject.FindGameObjectsWithTag("Moving Platform");
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        allObjects = startTiles.Concat(endTiles).Concat(platformTiles).Concat(movingPlatformTiles).Concat(coins).ToArray();
    }
    public GameObject[] GetGameObjects()
    {
        return allObjects;
    }

    public void GameObjectsToString()
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
    }
}
