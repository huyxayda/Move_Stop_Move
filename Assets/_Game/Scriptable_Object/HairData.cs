using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/HairData")]
public class HairData : ScriptableObject
{
    public List<GameObject> hair = new List<GameObject>();

    public GameObject GetHair(int random)
    {
        return hair[random];
    }
}
