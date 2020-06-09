using System;
public static class Constants
{
    //PREFERENCES
    public const string KEY_OBSTACLES_DISTANCE = "OBSTACLES_DISTANCE";
    public const float OBSTACLES_DISTANCE_EASY = 2.5f;
    public const float OBSTACLES_DISTANCE_DIFFICULT = 2.195f;

    //STARS
    public const string KEY_STARS_COUNT = "STARS_COUNT";
    public const int STARS_PER_AD = 100;

    //PLAYER SELECT PREFERENCES
    public const string KEY_SELECTED_PLAYER = "SELECTED_PLAYER";
    public const int PLAYER_LOCKED = 0;
    public const int PLAYER_UNLOCKED = 1;   

    //ADS
#if UNITY_IOS
    public const string ADS_GAME_ID = "3635552";
#elif UNITY_ANDROID
    public const string ADS_GAME_ID = "3635553";
#endif
    public const string BANNER_PLACEMENT_ID = "rover_run_banner";
    public const string REWARDED_VIDEO_PLACEMENT_ID = "rewardedVideo";
    public const bool ADS_TEST_MODE = false;

}
