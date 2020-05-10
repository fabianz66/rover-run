using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BgObjectText : MonoBehaviour
{
    public Text textUI;

    public void SetText(string text) {
        this.textUI.text = text;
    }
}
