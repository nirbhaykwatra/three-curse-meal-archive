using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private float _spawnTimer;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private float _timeToSpawnEvent;
    [SerializeField] private float _maxSpawnedEntities;
    [SerializeField] private List<Transform> _spawnTransforms = new List<Transform>();
    [SerializeField] private bool _addRandomRotation = false;
    [SerializeField] private UnityEvent OnSpawned;
    [SerializeField] private bool _isActive = false;
    
    private List<Vector3> _spawnPoints = new List<Vector3>();
    private List<Health> _spawnedEntities = new List<Health>();
    private float _timer;
    
    public bool IsActive { get => _isActive; set => _isActive = value; }

    private void Start()
    {
        foreach (Transform transform in _spawnTransforms)
        {
            _spawnPoints.Add(transform.position);
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnTimer)
        {
            if (_isActive && _spawnedEntities.Count <= _maxSpawnedEntities)
            {
                Health _spawnedEntity = Instantiate(_spawnObject, GetRandomSpawn(), Quaternion.identity, _parentTransform).GetComponent<Health>();
                _spawnedEntities.Add(_spawnedEntity);
            }
            
            if (_isActive && _spawnedEntities.Count <= _maxSpawnedEntities && _addRandomRotation)
            {
                Health _spawnedEntity = Instantiate(_spawnObject, GetRandomSpawn(), Quaternion.Euler(Random.Range(0f, 360f),  Random.Range(0f, 360f), Random.Range(0f, 360f)), _parentTransform).GetComponent<Health>();
                _spawnedEntities.Add(_spawnedEntity);
            }
            _timer = 0f;
        }
    }

    public void TriggerNextSpawn()
    {
        if (_isActive) StartCoroutine(OnSpawn());
    }

    private IEnumerator OnSpawn()
    {
        yield return new WaitForSeconds(_timeToSpawnEvent);
        
        OnSpawned.Invoke();
    }

    private Vector3 GetRandomSpawn()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
    }
}
