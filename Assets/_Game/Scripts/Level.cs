using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    public NavMeshData NavMeshData;
    public Transform startPoint;
    public Transform cube1, cube2;


    public void OnInit()
    {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(NavMeshData);
    }
}
