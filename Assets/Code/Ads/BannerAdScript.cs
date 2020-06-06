using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdScript : MonoBehaviour
{  
    void Start()
    {
        //Start ads sdk
        Advertisement.Initialize(Constants.ADS_GAME_ID, Constants.ADS_TEST_MODE);        
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        //Set position
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_RIGHT);

        //Wait til ready
        while (!Advertisement.IsReady(Constants.BANNER_PLACEMENT_ID))
        {
            yield return new WaitForSeconds(0.5f);
        }

        //Show the AD!
        Advertisement.Banner.Show(Constants.BANNER_PLACEMENT_ID);
    }
}
