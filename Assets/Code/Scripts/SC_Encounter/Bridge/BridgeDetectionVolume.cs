using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeDetectionVolume : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public BridgeEntityCount _entitiesOnBridge;
    
    private List<Collider> _entities = new List<Collider>();
    private bool _playerOnBridge;
    
    public int EntityCount
    {
        get => _entities.Count;
    }

    public bool PlayerOnBridge => _playerOnBridge;

    private void Start()
    {
        _entitiesOnBridge._entityCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            _entities.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            _entities.Remove(other);
        }
    }

    private void Update()
    {
        //Debug.Log($"Entity count: {_entities.Count} | Player on Bridge: {_playerOnBridge}");

        _entitiesOnBridge.EntityCount = _entities.Count;

        if (_entities.Contains(_player.GetComponent<Collider>()))
        {
            _playerOnBridge = true;
        }
        else
        {
            _playerOnBridge = false;
        }
    }
}
