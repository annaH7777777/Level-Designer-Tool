using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody rb;
    public float jumpHeight = 100f;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

       rb.AddForce(movement * speed * Time.deltaTime);
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
        else if(other.tag == "End tile")
        {
            Destroy(gameObject);
        }
    }
}
