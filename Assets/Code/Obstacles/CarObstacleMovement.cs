using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacleMovement : MonoBehaviour
{
    //Final max velocity X
    private float _finalMaxVelocity = 8.0f;

    //Player rigid body
    private Rigidbody2D _rigidBody;

    public void Start() {
        _rigidBody = this.GetComponent<Rigidbody2D>();        
    }

    private void FixedUpdate()
    {
        _rigidBody.linearVelocity = Vector3.right * _finalMaxVelocity * Time.timeScale;
    }
}
