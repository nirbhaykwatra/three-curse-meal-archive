using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _damage = 5f;
    [SerializeField] private float _knockback = 3f;
    
    private Transform _shooter;
    //private Targetable _targetable;
    public void SpawnBullet(GameObject cannon)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != null) 
        { 
            other.GetComponent<Health>().Damage(_damage, gameObject, _knockback);
        }
    }
}
