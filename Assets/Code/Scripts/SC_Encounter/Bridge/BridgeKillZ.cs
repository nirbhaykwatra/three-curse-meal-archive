using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BridgeKillZ : MonoBehaviour
{
    [SerializeField] private Transform _teleportPoint;
    [SerializeField] private UnityEvent OnTeleported;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            other.transform.position = _teleportPoint.position;
            OnTeleported.Invoke();
        }
        if(other.TryGetComponent(out Enemy enemy))
        {
            other.GetComponent<Health>().Kill();
        }
    }
}
