using System;
public static class Constants
{
    //PREFERENCES
    public const string KEY_OBSTACLES_DISTANCE = "OBSTACLES_DISTANCE";
    public const float OBSTACLES_DISTANCE_EASY = 2.5f;
    public const float OBSTACLES_DISTANCE_DIFFICULT = 2.195f;

    //PLAYER SELECT PREFERENCES
    public const string KEY_SELECTED_PLAYER = "SELECTED_PLAYER";
    public const string PLAYER_PERICO = "PERICO";
    public const string PLAYER_LR_PICKUP_YELLOW = "LR_PICKUP_YELLOW";
    public const string PLAYER_LR_PICKUP_BLUE = "LR_PICKUP_BLUE";
    public const string PLAYER_LR_PICKUP_RED_STRIPES = "LR_PICKUP_RED_STRIPES";
    public const string PLAYER_LR_PICKUP_BLACK = "LR_PICKUP_BLACK";
    public const string PLAYER_DEFAULT = PLAYER_PERICO;
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
    public const bool ADS_TEST_MODE = true;

}
