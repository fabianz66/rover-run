using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float _topLanePosition;

    [SerializeField]
    public float _bottomLanePosition;

    [SerializeField]
    public bool _isAtBottomLane;

    //Player rigid body
    private Rigidbody2D _playerRigidBody;

    //Acceleration applied to the player
    private float _acceleration = 40.0f;

    //Initial max velocity X
    private float _initialMaxVelocity = 7.0f;

    //Current max velocity X
    private float _currentMaxVelocity = 0.0f;

    // By how much is _currentMaxVelocity increased every _speedIncreaseIntervalS
    private float _speedIncrease = 1.0f;

    //How often is _currentMaxVelocity inscreased in seconds
    private float _speedIncreaseIntervalS = 10.0f;

    //Records the time at which the game started
    private float _gameStartTimeS = 0.0f;

    //Distance run by player
    private float _totalDistance = 0.0f;


    public void Start()
    {
        _playerRigidBody = this.GetComponent<Rigidbody2D>();
        _currentMaxVelocity = _initialMaxVelocity;
        _gameStartTimeS = Time.time;        
    }

    void Update()
    {
        //How many time we should have increased _currentMaxVelocity so far
        float intervals = Mathf.Floor((Time.time - _gameStartTimeS) / _speedIncreaseIntervalS);

        //Increase _currentMaxVelocity
        _currentMaxVelocity = _initialMaxVelocity + intervals * _speedIncrease;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (_isAtBottomLane)
            {
                transform.position = new Vector3(transform.position.x, _topLanePosition, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, _bottomLanePosition, transform.position.z);
            }
            _isAtBottomLane = !_isAtBottomLane;
        }
    }

    void FixedUpdate()
    {
        // Add force to player if he hasn't reached _currentMaxVelocity
        if (_playerRigidBody.velocity.x < _currentMaxVelocity) {
            _playerRigidBody.AddForce(Vector2.right * _acceleration);
        }
        //Debug.Log("Velocity: " + _playerRigidBody.velocity.x);
        Debug.Log("POS X: " + _playerRigidBody.position.x);
    }
}
