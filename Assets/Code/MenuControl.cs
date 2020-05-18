using System.Collections;
using System.Collections.Generic;
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
        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenFbImpreza()
    {
        Application.OpenURL("https://www.facebook.com/RepuestosLandRoverImpreza");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");        
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
}
