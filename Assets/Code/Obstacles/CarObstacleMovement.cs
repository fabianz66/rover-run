using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacleMovement : MonoBehaviour
{
    //Acceleration applied to the obstacle
    private float _acceleration = 100.0f;

    //Final max velocity X
    private float _finalMaxVelocity = 6.0f;

    //Player rigid body
    private Rigidbody2D _rigidBody;

    public void Start() {
        _rigidBody = this.GetComponent<Rigidbody2D>();        
    }

    private void FixedUpdate()
    {
        //if (_rigidBody.velocity.x < _finalMaxVelocity)
        //{
        //    _rigidBody.AddForce(Vector2.right * _acceleration * Time.timeScale);
        //}
        _rigidBody.velocity = Vector3.right * _finalMaxVelocity * Time.timeScale;
    }
}
