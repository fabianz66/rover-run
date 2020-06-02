using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("POSITION: " + this.transform.position.y);
        if (this.transform.position.y < -4) {
            Destroy(this);
        }
    }
}
