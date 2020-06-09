using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectOption : MonoBehaviour
{
    public string SelectedPlayerId;
    public int StarsCost;
    public Button BtnExchange;
    public Button BtnSelect;
    public Image ImgIsSelected;
    public Image ImgCarBody;
    public Text StarsCostTxt;
    public SelectCarOptionsFactory SelectCarScreen;

    void Start()
    {
        // Map the ShowRewardedVideo function to the button’s click listener:
        BtnExchange.onClick.AddListener(ExchangeStars);
        BtnSelect.onClick.AddListener(SetAsCurrentPlayer);

        //Refresh UI
        RefreshUI();
    }

    public void RefreshUI()
    {
        //If locked
        bool isLocked = PlayerPrefs.GetInt(SelectedPlayerId, Constants.PLAYER_LOCKED) == Constants.PLAYER_LOCKED;
        if (SelectedPlayerId.Equals(SelectCarOptionsFactory.DEFAULT_PLAYER_SPRITE)) {
            isLocked = false;
        }
        BtnExchange.gameObject.SetActive(isLocked);        
        StarsCostTxt.gameObject.SetActive(isLocked);
        StarsCostTxt.text = "" + StarsCost;
        if (isLocked) {

            //Player locked
            BtnSelect.gameObject.SetActive(false);
            ImgIsSelected.gameObject.SetActive(false);
            return;
        }

        //Player unlocked        
        bool isSelected = PlayerPrefs.GetString(Constants.KEY_SELECTED_PLAYER, SelectCarOptionsFactory.DEFAULT_PLAYER_SPRITE) == SelectedPlayerId;
        BtnSelect.gameObject.SetActive(!isSelected);
        ImgIsSelected.gameObject.SetActive(isSelected);
    }

    void SetAsCurrentPlayer()
    {
        PlayerPrefs.SetString(Constants.KEY_SELECTED_PLAYER, SelectedPlayerId);
        SelectCarScreen.RefreshUI();
    }

    void ExchangeStars()
    {
        int stars = PlayerPrefs.GetInt(Constants.KEY_STARS_COUNT, 0);
        if (stars >= StarsCost) {
            PlayerPrefs.SetInt(Constants.KEY_STARS_COUNT, stars - StarsCost);
            PlayerPrefs.SetInt(SelectedPlayerId, Constants.PLAYER_UNLOCKED);
            SelectCarScreen.RefreshUI();
        }
    }
}
