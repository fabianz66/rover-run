using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacleUI : MonoBehaviour
{
    [SerializeField]
    public Sprite[] CarSprites;
    [SerializeField]
    public Sprite[] TireSprites;
    [SerializeField]
    public SpriteRenderer LeftTireSprite;
    [SerializeField]
    public SpriteRenderer RightTireSprite;
    [SerializeField]
    public SpriteRenderer CarSprite;

    void Start()
    {        
        int carPos = Random.Range(0, CarSprites.Length);
        int tirePos = Random.Range(0, TireSprites.Length);
        LeftTireSprite.sprite = TireSprites[tirePos];
        RightTireSprite.sprite = TireSprites[tirePos];
        CarSprite.sprite = CarSprites[carPos];
    }
}
