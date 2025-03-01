using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [FoldoutGroup("Setup")]
    [SerializeField] private float _radius;
    [FoldoutGroup("Setup")]
    [SerializeField] private LayerMask _bridgeMask;

    [FoldoutGroup("Explosion")]
    [SerializeField] private float _explosionForce;
    [FoldoutGroup("Explosion")]
    [SerializeField] private float _explosionRadius;
    [FoldoutGroup("Explosion")]
    [SerializeField] private float _upwardsModifier;

    private Collider[] _bridgePieces;
    private RaycastHit[] _bridgeRaycastHits;
    private Transform _origin;

    private void Start()
    {
        _origin = transform;
    }

    private void OnEnable()
    {
        _origin = transform;
    }

    public void Explode()
    {
        _bridgeRaycastHits = Physics.SphereCastAll(transform.position, _radius, Vector3.down, _radius, _bridgeMask);
        
        if(_bridgeRaycastHits == null) return;

        foreach (RaycastHit hit in _bridgeRaycastHits)
        {
            if (hit.collider.attachedRigidbody == null || _bridgeRaycastHits == null) return;
            
            hit.collider.attachedRigidbody.AddExplosionForce(_explosionForce, _origin.position, _explosionRadius, _upwardsModifier, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
