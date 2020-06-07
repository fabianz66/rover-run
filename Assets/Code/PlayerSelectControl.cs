using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelectControl : MonoBehaviour
{
    private List<PlayerSelectOption> AllPlayerOptions;

    public void RefreshUI() {
        foreach (PlayerSelectOption obj in AllPlayerOptions) {
            obj.RefreshUI();
        }
    }

    public void RegisterOption(PlayerSelectOption option) {
        if (AllPlayerOptions == null) {
            AllPlayerOptions = new List<PlayerSelectOption>();
        }
        AllPlayerOptions.Add(option);
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {
            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
        }
    }
}
