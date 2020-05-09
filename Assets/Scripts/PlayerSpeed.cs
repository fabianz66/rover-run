using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    [SerializeField]
    public GameObject gameController;

    //Player rigid body
    private Rigidbody2D rigidBody;

    //Acceleration applied to the player
    private float acceleration = 40.0f;

    //Initial max velocity X
    private float initialMaxVelocity = 8.0f;

    //Final max velocity X
    public float finalMaxVelocity = 14.0f;

    //Current max velocity X
    private float currentMaxVelocity = 0.0f;

    // By how much is _currentMaxVelocity increased every _speedIncreaseIntervalS
    private float speedIncrease = 1.0f;

    //How often is _currentMaxVelocity inscreased in seconds
    private float speedIncreaseIntervalS = 8.0f;

    //Records the time at which the game started
    private float gameStartTimeS = 0.0f;

    //This flags determines wether the car moves or not
    private bool playerMoving = false;

    //Initial delay to start moving according to the music
    private float initialDelayS = 2f;

    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();        
        currentMaxVelocity = initialMaxVelocity;
        gameStartTimeS = Time.time + initialDelayS;
        this.GetComponent<Animator>().enabled = false;
        Invoke("StartMoving", initialDelayS);//Initial delay to start moving and animating
    }

    void Update()
    {
        //How many time we should have increased _currentMaxVelocity so far
        float elapsedTimeS = (Time.time - gameStartTimeS);
        float intervals = Mathf.Floor(elapsedTimeS / speedIncreaseIntervalS);

        //Increase _currentMaxVelocity
        currentMaxVelocity = initialMaxVelocity + intervals * speedIncrease;
        currentMaxVelocity = Mathf.Min(currentMaxVelocity, finalMaxVelocity);
    }

    void FixedUpdate()
    {
        //Wait for a little bit
        if (playerMoving == false) return;

        // Add force to player if he hasn't reached _currentMaxVelocity
        if (rigidBody.velocity.x < currentMaxVelocity)
        {
            rigidBody.AddForce(Vector2.right * acceleration);
        }

        Debug.Log("Rotation Z: " + rigidBody.transform.rotation.z);
    }

    void StartMoving() {
        playerMoving = true;
        this.GetComponent<Animator>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Car" || Mathf.Abs(rigidBody.transform.rotation.z) > 0.9f)
        {
            gameController.GetComponent<GameControl>().GameOver();
        }
    }
}
