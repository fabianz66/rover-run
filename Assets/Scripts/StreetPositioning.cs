using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetPositioning: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            transform.position = new Vector2(transform.position.x + 30, transform.position.y);
        }
    }
}
