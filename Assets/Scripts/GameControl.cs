using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    Text _highScoreText;
    float _highScore;

    [SerializeField]
    Text _currentScoreText;
    float _currentScore;

    [SerializeField]
    Transform _playerTransform;

    private void Start()
    {
    }

    void Update()
    {
        _currentScore = Mathf.Floor(_playerTransform.position.x);
        _currentScore = Mathf.Max(0, _currentScore);
        _currentScoreText.text = "Score: " + _currentScore;
    }

    public void MainScreen()
    {
        //_playerTransform.position = new Vector3(0, _playerTransform.position.y, _playerTransform.position.z);
        //Time.timeScale = 0;
    }

    public void StartGame()
    {        
        //_playerTransform.position = new Vector3(0, _playerTransform.position.y, _playerTransform.position.z);
        //Time.timeScale = 1;
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
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
}
