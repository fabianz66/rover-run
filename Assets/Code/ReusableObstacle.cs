using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReusableObstacle : MonoBehaviour
{
    public Queue<GameObject> _reusableQueue;

    void OnBecameInvisible()
    {
        //_reusableQueue.Enqueue(this.gameObject);
    }
}
