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
            GameObject[] coinTiles;
            coinTiles =GameObject.FindGameObjectsWithTag("Coin");
            foreach(GameObject coinTile in coinTiles)
            {
                coinTile.GetComponent<Tile>().Set3DScene();
            }
            GameObject[] platformTiles;
            platformTiles = GameObject.FindGameObjectsWithTag("Moving Platform");
            foreach (GameObject platformTile in platformTiles)
            {
                platformTile.GetComponent<Tile>().Set3DScene();
                foreach (Transform child in platformTile.transform)
                {
                    if (child.tag == "Transparent")
                        child.gameObject.SetActive(false);
                }
            }
            GameObject startTile = GameObject.FindGameObjectWithTag("Start tile");
            Vector3 startTilePos = startTile.transform.position;
            Vector3 ballPos = new Vector3(startTilePos.x, startTilePos.y + 1, startTilePos.z);
            ball = Instantiate(ballPrefab, ballPos, startTile.transform.rotation);
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
            GameObject[] coinTiles;
            coinTiles = GameObject.FindGameObjectsWithTag("Coin");
            foreach (GameObject coinTile in coinTiles)
            {
                coinTile.GetComponent<Tile>().Set2DScene();
            }
            GameObject[] platformTiles;
            platformTiles = GameObject.FindGameObjectsWithTag("Moving Platform");
            foreach (GameObject platformTile in platformTiles)
            {
                platformTile.GetComponent<Tile>().Set2DScene();
                foreach (Transform child in platformTile.transform)
                {
                    if (child.tag == "Transparent")
                        child.gameObject.SetActive(true);
                }
            }
            Destroy(ball);
        }
    }
}
