using UnityEngine;
using UnityEngine.UI;

// TODO: replace placeholder with Unity LevelPlay (com.unity.services.levelplay) or AdMob rewarded ad
[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour
{
    public Button ShowAdButton;
    public Text StarsCountTxt;

    void Start()
    {
        ShowAdButton.interactable = true;
        if (ShowAdButton) ShowAdButton.onClick.AddListener(ShowRewardedVideo);
        StarsCountTxt.text = PlayerPrefs.GetInt(Constants.KEY_STARS_COUNT, 0).ToString();
    }

    void ShowRewardedVideo()
    {
        // Placeholder: grant reward directly until a real ad SDK is integrated
        GrantReward();
    }

    private void GrantReward()
    {
        int starsCount = PlayerPrefs.GetInt(Constants.KEY_STARS_COUNT, 0);
        PlayerPrefs.SetInt(Constants.KEY_STARS_COUNT, starsCount + Constants.STARS_PER_AD);
        if (StarsCountTxt != null)
            StarsCountTxt.text = PlayerPrefs.GetInt(Constants.KEY_STARS_COUNT, 0).ToString();
    }
}
