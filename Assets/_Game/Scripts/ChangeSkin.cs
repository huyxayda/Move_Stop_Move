using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public List<GameObject> hair = new List<GameObject>();
    public Transform hairHolder;

    private void Start()
    {
        ChangeHair();
    }

    public void ChangeHair()
    {
        int randomHair = Random.Range(0, hair.Count);
        Instantiate(hair[randomHair], hairHolder);
    }
}
