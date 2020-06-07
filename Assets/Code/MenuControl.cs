using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [SerializeField]
    public Button MuteUnmuteButton;

    [SerializeField]
    public Sprite MutedSprite;

    [SerializeField]
    public Sprite UnmutedSprite;

    [SerializeField]
    public Text VersionText;

    private void Start()
    {
        //Initial UI
        if (AudioListener.volume == 0)
        {
            MuteUnmuteButton.image.sprite = MutedSprite;
        }
        else
        {
            MuteUnmuteButton.image.sprite = UnmutedSprite;
        }

        //Resume time
        Time.timeScale = 1.0f;

        //Show app version
        VersionText.text = Application.version;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenFbImpreza()
    {
        Application.OpenURL("https://www.facebook.com/RepuestosLandRoverImpreza");
    }

    public void OpenZamoraEngineer()
    {
        Application.OpenURL("https://www.zamora.engineer");
    }

    public void StartGameEasy()
    {
        PlayerPrefs.SetFloat(Constants.KEY_OBSTACLES_DISTANCE, Constants.OBSTACLES_DISTANCE_EASY);
        SceneManager.LoadScene("GameScene");        
    }

    public void StartGameDifficult()
    {
        PlayerPrefs.SetFloat(Constants.KEY_OBSTACLES_DISTANCE, Constants.OBSTACLES_DISTANCE_DIFFICULT);
        SceneManager.LoadScene("GameScene");
    }

    public void PlayerSelect()
    {
        SceneManager.LoadScene("PlayerSelect");
    }

    public void MuteUnmute()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            MuteUnmuteButton.image.sprite = UnmutedSprite;
        }
        else
        {
            AudioListener.volume = 0;
            MuteUnmuteButton.image.sprite = MutedSprite;
        }
    }

    public void OpenInfo()
    {
        SceneManager.LoadScene("Info");
    }

    void Update()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {
            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitGame();
            }
        }
    }
}
