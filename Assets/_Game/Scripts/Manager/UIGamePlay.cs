using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetAliveCharacter();
    }
    [SerializeField] TextMeshProUGUI aliveText;

    public void SetAliveCharacter()
    {
        int alive = LevelManager.Instance.enemyRemain;
        aliveText.text = "Alive: " + alive.ToString();
    }
}
