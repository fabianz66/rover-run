using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObjectImage : MonoBehaviour
{
    public SpriteRenderer spriteRendered;

    public void SetSprite(Sprite sprite)
    {
        spriteRendered.sprite = sprite;
    }
}
