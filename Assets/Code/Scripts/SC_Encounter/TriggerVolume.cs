using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerVolume : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnExit;
    private Encounter _encounter;

    private void Start()
    {
        _encounter = GetComponentInParent<Encounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            player.CurrentEncounterID = _encounter.ID;
            OnEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            OnExit.Invoke();
        }
    }

    public void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
