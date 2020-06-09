using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public SpriteRenderer PlayerSpriteRenderer;

    void Start()
    {
        string player_sprite = PlayerPrefs.GetString(Constants.KEY_SELECTED_PLAYER, SelectCarOptionsFactory.DEFAULT_PLAYER_SPRITE);
        PlayerSpriteRenderer.sprite = Resources.Load<Sprite>(player_sprite);        
    }
}
