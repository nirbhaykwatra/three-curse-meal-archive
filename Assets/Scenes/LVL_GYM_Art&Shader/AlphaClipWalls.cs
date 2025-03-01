using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaClipWalls : MonoBehaviour
{
    //private List<RaycastHit> _obstructions = new List<RaycastHit><RaycastHit>();
    private List<RaycastHit> _obstructions;
    
    [SerializeField] private GameObject _target;
    [SerializeField] private LayerMask _myLayerMask;
    [SerializeField] private float _fadeOutTimer;
    [SerializeField] private float _fadeOutSpeed;
    [SerializeField] private float _radius = 1f;
    float _translucence = 0.5f;
    float _opaque = 1f;

    private void Awake()
    {
        _obstructions = new List<RaycastHit>();
        
    }
    private void FixedUpdate()
    {
        SetWallTransperent();
    }

    private void SetWallTransperent()
    {
        RaycastHit[] hits = new RaycastHit[5];
        int hit;
        Vector3 direction = _target.transform.position - transform.position;
        List<RaycastHit> toRemove = new List<RaycastHit>();

    Debug.DrawRay(transform.position, direction * 10, Color.yellow, 0.1f);

        float distance = Vector3.Distance(_target.transform.position, transform.position);
        hit = Physics.SphereCastNonAlloc(transform.position, _radius, direction.normalized, hits, distance, _myLayerMask);

        foreach (RaycastHit obs in hits)
        {
            if (hits.Length > 0 && !_obstructions.Contains(obs))
            {
                _obstructions.Add(obs);
                ChangeObjectTransperent(obs.collider.gameObject, _translucence);
            }
        }



        for (int i = 0; i < _obstructions.Count; i++)
        {
            if (!Array.Exists<RaycastHit>(hits, x => x.collider == _obstructions[i].collider))
            {
                toRemove.Add(_obstructions[i]);
            }
        }
        foreach (RaycastHit nonObs in toRemove)
        {
            ChangeObjectTransperent(nonObs.collider.gameObject, _opaque);
            _obstructions.Remove(nonObs);
        }
    }
    private void ChangeObjectTransperent(GameObject obs, float v)
    {
        _fadeOutTimer += _fadeOutSpeed * Time.deltaTime;
        Renderer renderer = obs.GetComponent<Renderer>();

        renderer.material.SetFloat("transperency", Mathf.Lerp(_translucence, _opaque, _fadeOutTimer/10));

        if (!Mathf.Approximately(1f, v))
        {
            renderer.material.SetFloat("transperency", Mathf.Lerp(_opaque, _translucence, _fadeOutTimer/10));
        }

    }
}