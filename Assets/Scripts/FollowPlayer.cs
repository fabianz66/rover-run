using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    public Transform _player;

    [SerializeField]
    public float _offsetX;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(_player.position.x + _offsetX, transform.position.y, transform.position.z);
    }
}
