
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

    static Dictionary<Collider, Camera> cam = new Dictionary<Collider, Camera>();

    public static Camera GetCamera(Collider collider)
    {
        if (!cam.ContainsKey(collider))
        {
            cam.Add(collider, collider.GetComponent<Camera>());
        }
        return cam[collider];
    }
}
