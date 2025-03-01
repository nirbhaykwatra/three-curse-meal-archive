using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Encounter : MonoBehaviour
{
    [FoldoutGroup("Encounter Setup Variables", false)]
    [SerializeField] private int _ID;
    [FoldoutGroup("Encounter Setup Variables", false)]
    [SerializeField] protected UnityEvent OnReset;
    [FoldoutGroup("Encounter Setup Variables", false)]
    [SerializeField] protected UnityEvent OnStarted;
    [FoldoutGroup("Encounter Setup Variables", false)]
    [SerializeField] protected UnityEvent OnFinished;
    [FoldoutGroup("Encounter Setup Variables", false)]
    [SerializeField] protected UnityEvent OnIsCompleted;
    [FoldoutGroup("Encounter Setup Variables", false)]
    //[SerializeField] private LayerMask _enemyLayer;
    [FoldoutGroup("Encounter Setup Variables", false)]
    //[SerializeField] private bool _resetRoomOnRespawn = true;

    private List<Enemy> _spawnedEnemies = new List<Enemy>();
    private List<Enemy> _currentEnemies = new List<Enemy>();
    [ShowInInspector][ReadOnly]private bool _isCompleted = false;

    public bool IsCompleted
    {
        get => _isCompleted;
        set => _isCompleted = value;
    }


    public int ID
    {
        get => _ID;
        set => _ID = value;
    }

    private void Start()
    {
        Enemy[] spawnedEnemies = GetComponentsInChildren<Enemy>();

        foreach (Enemy enemy in spawnedEnemies)
        {
            _spawnedEnemies.Add(enemy);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _currentEnemies.Add(enemy);
            enemy.OnKilled.AddListener(OnEnemyKilled);
        }

        if (other.TryGetComponent(out PlayerController player))
        {
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _currentEnemies.Remove(enemy);
            enemy.OnKilled.RemoveListener(OnEnemyKilled);
        }
    }

    public virtual void StartEncounter()
    {
        OnStarted.Invoke();

        StartCoroutine(EncounterRoutine());
    }

    private IEnumerator EncounterRoutine()
    {
        // foreach (Enemy enemy in _spawnedEnemies)
        // {
        //     _currentEnemies.Add(enemy);
        // }

        while (_currentEnemies.Count > 0) yield return null;

        if (_currentEnemies.Count <= 0)
        {
            //_isCompleted = true;
            OnFinished.Invoke();
        }
    }

    public void ResetEncounter()
    {
        if (IsCompleted==true)
        {
            OnIsCompleted.Invoke();
        }

        OnReset.Invoke();
        _currentEnemies.Clear();
        foreach (Enemy enemy in _spawnedEnemies)
        {
            _currentEnemies.Add(enemy);
        }
        IsCompleted = false;
        IResetable[] resetables = GetComponentsInChildren<IResetable>();

        foreach (IResetable resetable in resetables)
        {
            resetable.Reset();
        }
    }

    private void OnEnemyKilled(Enemy enemy)
    {
        if (_currentEnemies.Contains(enemy)) _currentEnemies.Remove(enemy);
        Debug.Log(_currentEnemies.Count);
        
        enemy.OnKilled.RemoveListener(OnEnemyKilled);
    }

}
