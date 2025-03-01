using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeIgnoreKillZ : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out BridgeKillZ killZ)) return;
        else Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
    }
}
