using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{
    private float length, startpos;

    [SerializeField]
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float camX = (cam.transform.position.x);

        if (camX > startpos + length)
        {
            startpos += length;
        }
        else if (camX < startpos - length)
        {
            startpos -= length;
        }

        transform.position = new Vector3(startpos, transform.position.y, transform.position.z);
    }
}
