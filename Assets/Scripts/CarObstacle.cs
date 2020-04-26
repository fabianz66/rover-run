using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacle : MonoBehaviour
{
    //Acceleration applied to the obstacle
    private float _acceleration = 40.0f;

    //Final max velocity X
    private float _finalMaxVelocity = 10.0f;

    //Player rigid body
    private Rigidbody2D _rigidBody;

    public void Start() {
        _rigidBody = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_rigidBody.velocity.x < _finalMaxVelocity) {
            _rigidBody.AddForce(Vector2.right * _acceleration);
        }        
    }
}
