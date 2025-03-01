using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDebugs : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Collider _floor = GetComponent<Collider>();
        
        Gizmos.color = Color.green;
        Gizmos.DrawCube(_floor.bounds.center, new Vector3(_floor.bounds.size.x, _floor.bounds.size.y, _floor.bounds.size.z));
    }
}
