using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    public Text TitleText;

    [SerializeField]
    public Text SubtitleText;

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
        TitleText.enabled = false;
        SubtitleText.enabled = false;
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

        // Show message
        StartCoroutine(ShowSubtitleText("Recorre las 7 provincias y llega a Impreza", 0.0f, 4.0f));
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenFbImpreza()
    {
        Application.OpenURL("https://www.facebook.com/RepuestosLandRoverImpreza");
    }

    public void PauseResumeGame()
    {
        if (gameOver) return;

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            TitleText.enabled = false;
            playerAudio.UnPause();
            PauseResumeButton.image.sprite = ResumedSprite;
        }
        else
        {
            Time.timeScale = 0;
            TitleText.text = "PAUSA";            
            TitleText.enabled = true;
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
        TitleText.text = "¡PERDISTE!\nDISTANCIA RECORRIDA: " + (int)playerTransform.position.x;
        TitleText.enabled = true;
        SubtitleText.enabled = false;
        gameOver = true;
        playerAudio.Stop();
        Camera.main.GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();        
        Time.timeScale = 0.0f;
    }

    public void GameCompleted()
    {
        TitleText.text = "¡LLEGASTE!\nDISTANCIA RECORRIDA: " + (int)playerTransform.position.x;
        TitleText.enabled = true;
        SubtitleText.enabled = false;
        gameOver = true;
        playerAudio.Stop();
        Camera.main.GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
        Time.timeScale = 0.0f;
    }

    IEnumerator ShowSubtitleText(string text, float delay, float duration = 5.0f)
    {
        yield return new WaitForSeconds(delay);
        SubtitleText.text = text;
        SubtitleText.enabled = true;
        yield return new WaitForSeconds(duration);
        SubtitleText.enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
