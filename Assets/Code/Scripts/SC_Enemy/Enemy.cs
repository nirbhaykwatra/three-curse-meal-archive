using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IResetable
{
    [SerializeField] private List<GameObject> _loot;
    public UnityEvent<Enemy> OnKilled;
    private Health _health;
    private Vector3 _spawnPosition;

    private void Start()
    {
        _spawnPosition = transform.position;
        _health = GetComponent<Health>();
        _health.OnDeath.AddListener(Death);
    }

    private void Death()
    {
        OnKilled.Invoke(this);
        foreach (GameObject loot in _loot)
        {
            Instantiate(loot, transform.position + transform.up + Vector3.back, Quaternion.identity);
        }
        _health.OnDeath.RemoveListener(Death);
    }

    public void Reset()
    {
        _health.OnDeath.AddListener(Death);
        transform.position = _spawnPosition;
        _health.ResetHealth();
    }
}
