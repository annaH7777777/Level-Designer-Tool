using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 100f;
    Rigidbody rb;
    Vector3 startPos;
    TileSpawner tileSpawner;
    DimensionScript dimensionScript;
    Transform colliderParent;

    void Start()
    {
        tileSpawner = FindObjectOfType<TileSpawner>();
        dimensionScript = FindObjectOfType<DimensionScript>();
        rb = GetComponent<Rigidbody>();
        startPos = gameObject.transform.position;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(moveHorizontal, rb.velocity.y, moveVertical);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpHeight);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plane")
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            transform.position = startPos;
        }
        else if(other.tag == "Coin")
        {
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "End tile")
        {
            Destroy(gameObject);
            dimensionScript.panel.gameObject.SetActive(true);
            dimensionScript.GameObjectsToString();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Moving")
        {
            colliderParent = collision.collider.GetComponentInParent<Transform>();
            transform.SetParent(collision.collider.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Moving")
        {
            if(colliderParent == collision.collider.GetComponentInParent<Transform>())
                transform.SetParent(null);
        }
    }
}
