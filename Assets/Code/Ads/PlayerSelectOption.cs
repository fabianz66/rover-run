using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class PlayerSelectOption : MonoBehaviour, IUnityAdsListener
{
    public string SelectedPlayerId;    
    public Button BtnShowAd;
    public Button BtnSelect;
    public Image ImgIsSelected;
    public PlayerSelectControl PlayerSelectController;

    void Start()
    {
        //We want El perico always unlocked
        PlayerPrefs.SetInt(Constants.PLAYER_PERICO, Constants.PLAYER_UNLOCKED);        

        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(Constants.ADS_GAME_ID, Constants.ADS_TEST_MODE);
      
        // Map the ShowRewardedVideo function to the button’s click listener:
        BtnShowAd.onClick.AddListener(ShowRewardedVideo);
        BtnSelect.onClick.AddListener(SetAsCurrentPlayer);

        //Refresh UI
        RefreshUI();

        //Registers itself
        PlayerSelectController.RegisterOption(this);
    }

    public void RefreshUI()
    {
        bool isLocked = PlayerPrefs.GetInt(SelectedPlayerId, Constants.PLAYER_LOCKED) == Constants.PLAYER_LOCKED;
        BtnShowAd.gameObject.SetActive(isLocked);
        BtnShowAd.interactable = Advertisement.IsReady(Constants.REWARDED_VIDEO_PLACEMENT_ID);
        if (isLocked) {

            //Player locked
            BtnSelect.gameObject.SetActive(false);
            ImgIsSelected.gameObject.SetActive(false);
            return;
        }

        //Player unlocked        
        bool isSelected = PlayerPrefs.GetString(Constants.KEY_SELECTED_PLAYER, Constants.PLAYER_DEFAULT) == SelectedPlayerId;
        BtnSelect.gameObject.SetActive(!isSelected);
        ImgIsSelected.gameObject.SetActive(isSelected);
    }

    void SetAsCurrentPlayer()
    {
        Debug.Log("SETTING AS CURRENT PLAYER: " + SelectedPlayerId);
        PlayerPrefs.SetString(Constants.KEY_SELECTED_PLAYER, SelectedPlayerId);
        PlayerSelectController.RefreshUI();
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
            BtnShowAd.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            Debug.Log("SETTING AS CURRENT PLAYER: " + SelectedPlayerId);
            PlayerPrefs.SetInt(SelectedPlayerId, Constants.PLAYER_UNLOCKED);
            RefreshUI();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
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
