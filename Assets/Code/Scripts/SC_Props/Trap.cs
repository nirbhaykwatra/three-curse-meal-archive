using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float _damageOnImpact;
    [SerializeField] private float _damageOverTime;
    [SerializeField] private float _damageInterval;

    private List<Collider> _entities = new List<Collider>();
    private float _timer;
    private bool _trapped;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.TryGetComponent(out Health health))
        {
            _entities.Add(other);
            other.GetComponent<Health>().Damage(_damageOnImpact);
        }
        if (other.TryGetComponent(out PlayerController player))
        {
            other.GetComponent<CharacterMovement3D>().MoveSpeedMultiplier = .5f;
        }

       
    }

    private void OnTriggerStay(Collider other)
    {
        _trapped = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            _entities.Remove(other);
            _trapped = false;
        }
        if (other.TryGetComponent(out PlayerController player))
        {
            other.GetComponent<CharacterMovement3D>().MoveSpeedMultiplier = 1f;
        }
    }

    private void Update()
    {
        if (_trapped)
        {
            _timer += Time.deltaTime;
            if (_timer >= _damageInterval)
            {
                _timer = 0f;
                foreach (Collider entity in _entities)
                {
                    entity.gameObject.GetComponent<Health>().Damage(_damageOverTime);
                }
            }
        }
    }
}
