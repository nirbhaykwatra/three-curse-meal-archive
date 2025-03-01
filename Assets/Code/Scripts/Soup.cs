using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soup : MonoBehaviour
{
    private GameObject _black;
    [SerializeField] GameObject _spawnPoint;
    private void Awake()
    {
        _black = GameObject.Find("Black");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            other.GetComponent<Health>().Damage(10);
            other.transform.position = _spawnPoint.transform.position;
        }
        if (other.TryGetComponent(out Enemy enemy))
        {
            other.GetComponent<Health>().Kill();
        }

        if (other.TryGetComponent(out Rock rock))
        {
            Destroy(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            collision.gameObject.GetComponent<Health>().Kill();
        }
        
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8 || collision.gameObject.layer == 10) 
           Physics.IgnoreCollision(GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
    }
}    
