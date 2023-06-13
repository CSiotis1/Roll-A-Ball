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
    private int pickupCount;
    private Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPos;
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //Run the check pickups function
        CheckPickups();
        //Get the timer object
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
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
            //Decrement the pickup count
            pickupCount -= 1;
            //Run the check pickups function
            CheckPickups();
           
        }
    }

    void CheckPickups()
    {
        print("Pickups Left: " + pickupCount);
        if (pickupCount == 0)
        {
            timer.StopTimer();
            print("Good Work, Goober! Your time was: " + timer.GetTime());
        }
    }
}

