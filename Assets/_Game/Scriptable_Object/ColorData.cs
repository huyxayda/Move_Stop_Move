using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ColorType { Red, Green, Blue , Yellow} 

[CreateAssetMenu(menuName = "Data/ColorData")]

public class ColorData : ScriptableObject
{
    public List<Material> colors;

    public Material ChangeColor(int random)
    {
        return colors[random];
    }
}
