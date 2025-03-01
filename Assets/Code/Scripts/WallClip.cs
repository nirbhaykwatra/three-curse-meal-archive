using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClip : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.Kill();
        }
    }
}
