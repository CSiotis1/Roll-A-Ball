using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public float speed = 2000.0f;
    public string playerName = "Goober";
    public bool isGameOver = false;
    public Vector3 startingPos;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPos;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver)
        {
            health = health - 1;
            print("Health is " + health);
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pick Up")
        {
            Destroy(collision.gameObject);
        }    
        print(collision.gameObject.name);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pick Up")
        {
            Destroy(other.gameObject);
        }
    }
}

