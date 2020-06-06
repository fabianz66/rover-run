using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    Button myButton;

    void Start()
    {
        myButton = GetComponent<Button>();

        // Set interactivity to be dependent on the Placement’s status:
        myButton.interactable = Advertisement.IsReady(Constants.REWARDED_VIDEO_PLACEMENT_ID);

        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(Constants.ADS_GAME_ID, Constants.ADS_TEST_MODE);
    }

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        Advertisement.Show(Constants.REWARDED_VIDEO_PLACEMENT_ID);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == Constants.REWARDED_VIDEO_PLACEMENT_ID)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            Debug.Log("OnUnityAdsDidFinish:FINISHED");
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
            Debug.Log("OnUnityAdsDidFinish:SKIPPED");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
            Debug.Log("OnUnityAdsDidFinish:SKIPPED");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
