using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    public Text TitleText;

    //[SerializeField]
    public GameObject TitleBg;

    [SerializeField]
    public Button MuteUnmuteButton;

    [SerializeField]
    public Sprite MutedSprite;

    [SerializeField]
    public Sprite UnmutedSprite;

    [SerializeField]
    public Button PauseResumeButton;

    [SerializeField]
    public Sprite PausedSprite;

    [SerializeField]
    public Sprite ResumedSprite;

    [SerializeField]
    public AudioClip GameCompletedMusic;

    //Player information
    [SerializeField]
    public GameObject player;
    private Transform playerTransform;
    private AudioSource playerAudio;

    // If the user lost
    private bool gameOver = false;

    private void Start()
    {
        //Initial UI
        SetTitle(null);
        if (AudioListener.volume == 0) {
            MuteUnmuteButton.image.sprite = MutedSprite;
        } else {
            MuteUnmuteButton.image.sprite = UnmutedSprite;
        }        

        // Get all player information
        playerAudio = player.GetComponent<AudioSource>();
        playerTransform = player.GetComponent<Transform>();        

        //Resume time
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseResumeGame()
    {
        if (gameOver) return;

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            SetTitle(null);
            playerAudio.UnPause();
            PauseResumeButton.image.sprite = ResumedSprite;
        }
        else
        {
            Time.timeScale = 0;
            SetTitle("PAUSA");
            PauseResumeButton.image.sprite = PausedSprite;
            playerAudio.Pause();
        }
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

    public void GameOver()
    {
        SetTitle("¡PERDISTE!\nDISTANCIA RECORRIDA: " + (int)playerTransform.position.x);
        gameOver = true;
        playerAudio.Stop();
        Camera.main.GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();        
        Time.timeScale = 0.0f;
    }

    public void GameCompleted()
    {
        SetTitle("¡LLEGASTE!");
        gameOver = true;
        playerAudio.Stop();
        Camera.main.GetComponent<AudioSource>().clip = GameCompletedMusic;
        Camera.main.GetComponent<AudioSource>().Play();
        Time.timeScale = 0.0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void SetTitle(string text)
    {
        if (text != null)
        {
            TitleText.text = text;
            TitleText.enabled = true;
            TitleBg.SetActive(true);
        }
        else
        {
            TitleText.text = text;
            TitleText.enabled = false;
            TitleBg.SetActive(false);
        }        
    }
}
