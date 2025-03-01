using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliRingHP : MonoBehaviour
{
    private EnemyAttacks _check;
    [SerializeField] private float _broccoliHealth = 3f;
    private void OnTriggerEnter(Collider other)
    {
        _check = other.gameObject.GetComponent<EnemyAttacks>();
        if (_check == null) return;
        else
        {
            if (_broccoliHealth > 0) _broccoliHealth = _broccoliHealth - 1;
            if (_broccoliHealth == 0) Destroy(gameObject);
        }  
    }
}
