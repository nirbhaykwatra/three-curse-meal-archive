using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPart : MonoBehaviour
{
    private void Start()
    {
        gameObject.layer = 7;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 10 || collision.gameObject.layer == 8)
        Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
    }
}
