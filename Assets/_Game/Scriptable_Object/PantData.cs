using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PantData")]

public class PantData : ScriptableObject
{
    public List<Material> pants;

    public Material ChangePant(int random)
    {
        return pants[random];
    }
}
