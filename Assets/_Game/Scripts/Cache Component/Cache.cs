
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    static Dictionary<Collider, IHIt> dict = new Dictionary<Collider, IHIt>();

    public static IHIt GetHit(Collider collider)
    {
        if (!dict.ContainsKey(collider))
        {
            dict.Add(collider, collider.GetComponent<IHIt>());
        }
        return dict[collider];
    }
}
