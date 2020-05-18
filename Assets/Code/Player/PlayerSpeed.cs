using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    [SerializeField]
    public GameObject gameController;
   
    [SerializeField]
    public Rigidbody2D playerRigidBody;

    //Acceleration applied to the player
    private float acceleration = 200f;

    //Final max velocity X
    private float maxVelocity = 13.0f;

    //This flags determines wether the car moves or not
    private bool playerMoving = false;

    //Initial delay to start moving according to the music
    private float initialDelayS = 2f;

    void Start()
    {
        this.GetComponent<Animator>().enabled = false;
        Invoke("StartMoving", initialDelayS);//Initial delay to start moving and animating
    }

    void FixedUpdate()
    {
        //Wait for a little bit
        if (playerMoving == false) return;

        // Add force to player if he hasn't reached _currentMaxVelocity
        if (playerRigidBody.velocity.x < maxVelocity)
        {
            playerRigidBody.AddForce(Vector2.right * acceleration);
        }
    }

    void StartMoving() {
        playerMoving = true;
        this.GetComponent<Animator>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerMoving == false) return;

        if (collision.gameObject.name == "Car(Clone)" || Mathf.Abs(playerRigidBody.transform.rotation.z) > 0.9f)
        {
            gameController.GetComponent<GameControl>().GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerMoving == false) return;

        if (collision.gameObject.name == "FinishLine")
        {
            gameController.GetComponent<GameControl>().GameCompleted();
        }

        if (collision.gameObject.name == "IncreaseSpeed")
        {
            maxVelocity = 15.0f;
        }
    }
}
