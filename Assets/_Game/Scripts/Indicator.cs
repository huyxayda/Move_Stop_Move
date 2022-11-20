using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private SpriteRenderer indicatorSprite;

    void Awake()
    {
        indicatorSprite = transform.GetComponent<SpriteRenderer>();
    }
    public void SetSpriteColor(Color color)
    {
        indicatorSprite.color = color;
    }
}
