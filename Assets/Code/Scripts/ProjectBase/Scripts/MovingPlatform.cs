﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// moves platforms using Rigidbodies
public class MovingPlatform : MonoBehaviour
{
    // points to loop through
    [SerializeField] private Vector3[] _points = new Vector3[] { -Vector3.right, Vector3.right };
    [SerializeField] private float _speed = 2f;
    // moves to next point within distance
    [SerializeField] private float _pointReachedDistance = 0.05f;
    // slows movement within distance
    [SerializeField] private float _easingDistance = 0.5f;

    // gets/sets velocity on appropriate rigidbody component
    public Vector3 Velocity
    {
        get
        {
            if (_is3D) return _rb3D.velocity;
            else return _rb2D.velocity;
        }

        set
        {
            if (_is3D) _rb3D.velocity = value;
            else _rb2D.velocity = value;
        }
    }
    public Vector3 NextPoint => _startPosition + _points[_pointIndex % _points.Length];
    public Vector3 PreviousPoint => _startPosition + _points[(_pointIndex + _points.Length - 1) % _points.Length];

    private Vector3 _startPosition;
    private int _pointIndex;
    private Rigidbody2D _rb2D;
    private Rigidbody _rb3D;
    private bool _is3D;

    private void Awake()
    {
        _startPosition = transform.position;
        // get both 2D and 3D rigidbodies and then set flag for which one was found
        _rb2D = GetComponent<Rigidbody2D>();
        _rb3D = GetComponent<Rigidbody>();
        if (_rb3D != null) _is3D = true;
    }

    private void FixedUpdate()
    {
        // checks if point is reached
        float distance = Vector3.Distance(transform.position, NextPoint);
        if (distance < _pointReachedDistance) _pointIndex++;

        // calculates movement direction/speed/easing
        Vector3 dir = (NextPoint - transform.position).normalized;
        float distanceToPrevious = Vector3.Distance(transform.position, PreviousPoint);
        float previousEasing = distanceToPrevious / _easingDistance;
        float nextEasing = distance / _easingDistance;
        float easing = Mathf.Min(previousEasing, nextEasing);
        easing = Mathf.Clamp(easing, 0.1f, 1f);
        Velocity = dir * _speed * easing;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
        Gizmos.color = Color.green;
        Vector3 origin = Application.isPlaying ? _startPosition : transform.position;
        for (int i = 0; i < _points.Length; i++)
        {
            Vector3 point = origin + _points[i];
            Vector3 nextPoint = origin + _points[(i + 1) % _points.Length];
            Gizmos.DrawWireSphere(point, 0.1f);
            Gizmos.DrawLine(point, nextPoint);
        }
    }
}
