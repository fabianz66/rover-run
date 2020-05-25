using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConesFix : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.name == "Cone(Clone)" || collision.gameObject.name == "Car(Clone)")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 400f);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Cone(Clone)" || collision.gameObject.name == "Car(Clone)")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 400f);
        }
    }
}
