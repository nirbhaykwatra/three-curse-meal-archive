using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class KillZVolume : MonoBehaviour
{
    [SerializeField] private Transform _playerRespawn;
    [SerializeField] private UnityEvent OnKillZ;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Health>().Kill();
        }
        if(other.GetComponent<PlayerController>() != null)
        {
            other.transform.position = _playerRespawn.position;
            OnKillZ.Invoke();
        }

    }
}
