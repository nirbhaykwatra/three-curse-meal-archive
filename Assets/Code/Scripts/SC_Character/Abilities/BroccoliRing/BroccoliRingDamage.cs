using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliRingDamage : MonoBehaviour
{
    [SerializeField] private float _damage = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Health health) || other == this.gameObject.GetComponentInParent<Collider>()) return;
        if (other.TryGetComponent(out PlayerController player)) return;
        else other.GetComponent<Health>().Damage(_damage);
    }
}
