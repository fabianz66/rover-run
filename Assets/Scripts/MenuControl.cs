using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
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
}
