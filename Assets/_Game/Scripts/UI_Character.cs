using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Character : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;


    private Transform mainCameraTrans;
    public List<string> enemyNames = new List<string> 
    { 
        "Harry Potter",
        "Jack5cu", 
        "Jack 100cu", 
    };
    public Character cha;
    private void Start()
    {
        mainCameraTrans = Camera.main.transform;
        PickNameInList();
    }

    private void LateUpdate()
    {
        SetLevel(cha.level);

        //UI cua Character luon doi dien voi camera
        transform.LookAt(transform.position + mainCameraTrans.rotation * Vector3.forward, mainCameraTrans.rotation * Vector3.up);
    }

    public void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }

    public void PickNameInList()
    {
        string randomName = enemyNames[Random.Range(0, enemyNames.Count)];
        nameText.text = randomName;
    }
}
