using System;
using UnityEngine;

public class BgObject
{
    public enum TYPE { ROAD_SIGN_GREEN, BILLBOARD, IMAGE, FINISH_LINE, INCREASE_SPEED };
    public readonly TYPE Type;
    public string Text { get; }
    public Sprite Sprite { get; }

    public BgObject(BgObject.TYPE type, String text, Sprite sprite)
    {
        this.Type = type;
        this.Text = text;
        this.Sprite = sprite;
    }
}
