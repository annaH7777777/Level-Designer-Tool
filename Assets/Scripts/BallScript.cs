using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody rb;
    public float jumpHeight = 100f;
    Vector3 startPos;
    public TileSpawner tileSpawner;
    bool exitedCollider = true;

    // Start is called before the first frame update
    void Start()
    {
        tileSpawner = FindObjectOfType<TileSpawner>();
        rb = GetComponent<Rigidbody>();
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

       // Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = new Vector3(moveHorizontal, rb.velocity.y, moveVertical);

       //rb.AddForce(movement * speed * Time.deltaTime);
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
            tileSpawner.panel.gameObject.SetActive(true);
            tileSpawner.GameObjectsToString();
        }
        

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Moving" && !exitedCollider)
        {
            Debug.Log("ontriggerExit " + collision.collider.transform.position);
            exitedCollider = true;
            transform.SetParent(null);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Moving" && exitedCollider)
        {
            Debug.Log("ontriggerEnter " + collision.collider.transform.position);
            transform.SetParent(collision.collider.transform);
            exitedCollider = false;
        }
    }
}
