using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        m_OrthographicCamera.gameObject.SetActive(true);
        m_PerspectiveCamera.gameObject.SetActive(false);
        tileSpawner = FindObjectOfType<TileSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeDimension()
    {
        //Debug.Log("Camera");
        if (gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text == "3D" && tileSpawner.GetGrid().HasStartTile() && tileSpawner.GetGrid().HasEndTile())
        {
            m_OrthographicCamera.gameObject.SetActive(false);
            //m_OrthographicCamera.enabled = false;
            m_PerspectiveCamera.gameObject.SetActive(true);
            //m_PerspectiveCamera.enabled = true;
            //Debug.Log("CameraPers " + m_PerspectiveCamera.enabled);
            gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text = "2D";
            lines.gameObject.SetActive(false);
            foreach(Button button in buttons)
            {
                button.gameObject.SetActive(false);
            }
            
            coinTiles =GameObject.FindGameObjectsWithTag("Coin");
            foreach(GameObject coinTile in coinTiles)
            {
                coinTile.GetComponent<Coin>().SetAnimationOn();
                coinTile.GetComponent<Coin>().Set3DScene();
            }
            GameObject[] platformTiles;
            platformTiles = GameObject.FindGameObjectsWithTag("Moving Platform");
            foreach (GameObject platformTile in platformTiles)
            {
                platformTile.GetComponent<Tile>().SetAnimationOn();
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
                tile.Set3DScene();
            }
            tileSpawner.SetAllGameObjects();
            
        }
        else
        {
            //Debug.Log("dimention");
            m_OrthographicCamera.gameObject.SetActive(true);
            //m_OrthographicCamera.enabled = true;
            m_PerspectiveCamera.gameObject.SetActive(false);
            //m_PerspectiveCamera.enabled = false;
            gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text = "3D";
            lines.gameObject.SetActive(true);
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(true);
            }
            //GameObject[] coinTiles;
            //coinTiles = GameObject.FindGameObjectsWithTag("Coin");
            //Debug.Log("coin0 " + coinTiles[0]);
            foreach (GameObject coinTile in coinTiles)
            {
                coinTile.gameObject.SetActive(true);
                coinTile.GetComponent<Coin>().SetAnimationOff();
                coinTile.GetComponent<Coin>().Set2DScene();
            }
            GameObject[] platformTiles;
            platformTiles = GameObject.FindGameObjectsWithTag("Moving Platform");
            foreach (GameObject platformTile in platformTiles)
            {
                platformTile.GetComponent<Tile>().SetAnimationOff();
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
                tile.Set2DScene();
            }
            Destroy(ball);
        }
    }
}
