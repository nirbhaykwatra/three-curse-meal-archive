using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using ParadoxNotion.Serialization.FullSerializer;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Destructible : MonoBehaviour
{
    [FoldoutGroup("Variables")]
    [SerializeField] private float _despawnTimer = 5f;
    [FoldoutGroup("Variables")]
    [SerializeField] private PepperBlast _pepperBlast;
    [FoldoutGroup("Variables")]
    [SerializeField] private bool _slowMoMode = false;
    [FoldoutGroup("Loot")]
    [SerializeField] private GameObject _loot;
    [FoldoutGroup("Loot")]
    [Required("Need Loot prefab!", InfoMessageType.Error)]
    [SerializeField] private LootDrop[] _lootEffects;
    
    private Collider[] _colliders;
    private bool _isDestroyed = false;
    private bool _isColliding = false;

    public UnityEvent OnHit;

    private void Start()
    {
        _colliders = GetComponentsInChildren<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDestroyed == true) return;
        if (other.TryGetComponent(out Weapon weapon) && _isDestroyed == false)
        {
            _isDestroyed = true;
            OnHit.Invoke();
            if (_loot != null && _lootEffects.Length > 0)
            {
                GameObject lootObject = Instantiate(_loot, transform.position, Quaternion.identity);
                Loot loot = lootObject.GetComponentInChildren<Loot>();
                loot.LootEffect = _lootEffects[Random.Range(0, _lootEffects.Length)];
                //_loot.GetComponent<Rigidbody>().AddForce(Vector3.up + transform.forward * 0.5f, ForceMode.Impulse );
            }
            
            foreach (Collider body in _colliders)
            {
                body.GetComponent<Rigidbody>().isKinematic = false;
                body.enabled = true;
            }
           
        }
    }

    
    private void Blast (Vector3 direction, float force)
    {
        OnHit.Invoke();
        GameObject lootObject = Instantiate(_loot, transform.position, Quaternion.identity);
        Loot loot = lootObject.GetComponentInChildren<Loot>();
        if (_lootEffects.Length > 0) loot.LootEffect = _lootEffects[Random.Range(0, _lootEffects.Length)];
        //_loot.GetComponent<Rigidbody>().AddForce(Vector3.up + transform.forward * 0.5f, ForceMode.Impulse );
        foreach (Collider body in _colliders)
        {
            body.GetComponent<Rigidbody>().isKinematic = false;
            body.enabled = true;
            body.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
        }
    }

    public void Blast(Vector3 direction)
    {
        Blast(direction, _pepperBlast.PropBlastForce);
    }
    
}
