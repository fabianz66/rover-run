using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPositionUI : MonoBehaviour
{
    [SerializeField]
    public Text ImprezaDstText;
 
    [SerializeField]
    // Player transform to get the position
    public Transform PlayerTransform;

    //Class in charge of adding elements to the background
    private BgObjectSpawner BgObjectSpawner;

    // Start is called before the first frame update
    void Start()
    {
        //Get class that adds elements to the background
        BgObjectSpawner = GetComponent<BgObjectSpawner>();
        ImprezaDstText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {     
        //Get distance to impreza
        BgObjectGroup bog = BgObjectSpawner.imprezaBgGroup;
        if (bog != null)
        {
            int dist = (int)(bog.endPositionX - PlayerTransform.transform.position.x);
            if (dist > 0)
            {
                ImprezaDstText.text = "" + Mathf.Max(dist, 0);
            } else {
                ImprezaDstText.enabled = false;
            }
        }
    }
}
