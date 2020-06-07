using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Sprite Perico;
    public Sprite LrPickupYellow;
    public Sprite LrPickupBlue;
    public Sprite LrPickupRedStripped;
    public Sprite LrPickupBlack;
    public SpriteRenderer PlayerSpriteRenderer;

    void Start()
    {
        string player = PlayerPrefs.GetString(Constants.KEY_SELECTED_PLAYER);
        switch (player) {
            case Constants.PLAYER_PERICO:
                PlayerSpriteRenderer.sprite = Perico;
                break;
            case Constants.PLAYER_LR_PICKUP_YELLOW:
                PlayerSpriteRenderer.sprite = LrPickupYellow;
                break;
            case Constants.PLAYER_LR_PICKUP_BLUE:
                PlayerSpriteRenderer.sprite = LrPickupBlue;
                break;
            case Constants.PLAYER_LR_PICKUP_RED_STRIPES:
                PlayerSpriteRenderer.sprite = LrPickupRedStripped;
                break;
            case Constants.PLAYER_LR_PICKUP_BLACK:
                PlayerSpriteRenderer.sprite = LrPickupBlack;
                break;
        }
    }
}
