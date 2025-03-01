using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private float _value;
    
    private Rigidbody _rigidbody;
    private Vector3 _groundNormal = Vector3.up;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.AddForce(_groundNormal * _value);
    }
}
