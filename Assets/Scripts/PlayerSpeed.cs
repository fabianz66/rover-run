using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    //Player rigid body
    private Rigidbody2D _rigidBody;

    //Acceleration applied to the player
    private float _acceleration = 40.0f;

    //Initial max velocity X
    private float _initialMaxVelocity = 7.0f;

    //Final max velocity X
    private float _finalMaxVelocity = 12.0f;

    //Current max velocity X
    private float _currentMaxVelocity = 0.0f;

    // By how much is _currentMaxVelocity increased every _speedIncreaseIntervalS
    private float _speedIncrease = 1.0f;

    //How often is _currentMaxVelocity inscreased in seconds
    private float _speedIncreaseIntervalS = 10.0f;

    //Records the time at which the game started
    private float _gameStartTimeS = 0.0f;

    //This flags determines wether the car moves or not
    private bool _playerMoving = false;

    //Initial delay to start moving according to the music
    private float _initialDelayS = 2f;

    void Start()
    {
        _rigidBody = this.GetComponent<Rigidbody2D>();        
        _currentMaxVelocity = _initialMaxVelocity;
        _gameStartTimeS = Time.time + _initialDelayS;
        this.GetComponent<Animator>().enabled = false;
        Invoke("StartMoving", _initialDelayS);//Initial delay to start moving and animating
    }

    void Update()
    {
        //How many time we should have increased _currentMaxVelocity so far
        float elapsedTimeS = (Time.time - _gameStartTimeS);
        float intervals = Mathf.Floor(elapsedTimeS / _speedIncreaseIntervalS);

        //Increase _currentMaxVelocity
        _currentMaxVelocity = _initialMaxVelocity + intervals * _speedIncrease;
        _currentMaxVelocity = Mathf.Min(_currentMaxVelocity, _finalMaxVelocity);
    }

    void FixedUpdate()
    {
        //Wait for a little bit
        if (_playerMoving == false) return;

        // Add force to player if he hasn't reached _currentMaxVelocity
        if (_rigidBody.velocity.x < _currentMaxVelocity)
        {
            _rigidBody.AddForce(Vector2.right * _acceleration);
        }
    }

    void StartMoving() {
        _playerMoving = true;
        this.GetComponent<Animator>().enabled = true;
    }
}
