using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoControl : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenZamoraEngineer()
    {
        Application.OpenURL("https://zamora.engineer");
    }

    public void OpenImprezaLtda()
    {
        Application.OpenURL("https://www.repuestosimpreza.com");
    }
}
