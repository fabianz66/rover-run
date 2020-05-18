using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPositionUI : MonoBehaviour
{
    [SerializeField]
    public Text CurrentProvinceText;

    [SerializeField]
    public Text NextProvinceText;

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
        NextProvinceText.text = "";
        CurrentProvinceText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BgObjectGroup bog = BgObjectSpawner.GetBgObjectGroup(PlayerTransform.position.x);
        if (bog == null) {
            return;            
        }

        //Update current province
        CurrentProvinceText.text = bog.name;

        //Get next province
        bog = BgObjectSpawner.GetNextBgObjectGroup(PlayerTransform.position.x);
        if (bog != null)
        {
            int dist = (int)(bog.startPositionX - PlayerTransform.transform.position.x);
            NextProvinceText.text = bog.name + " en: " + Mathf.Max(dist, 0);
        } else {
            NextProvinceText.enabled = false;
        }

        //Get distance to impreza
        bog = BgObjectSpawner.imprezaBgGroup;
        if (bog != null)
        {
            int dist = (int)(bog.startPositionX - PlayerTransform.transform.position.x);
            if (dist > 0)
            {
                ImprezaDstText.text = bog.name + " en: " + Mathf.Max(dist, 0);
            } else {
                ImprezaDstText.enabled = false;
            }
        }
    }
}
