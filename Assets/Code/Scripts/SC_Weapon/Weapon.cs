using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    float atkKnck = 1;
    private float _knockbackMultiplier;
    [SerializeField] private float _damage = 20f;
    [SerializeField] float KnockbackOnAttack1 = 2;
    [SerializeField] float KnockbackOnAttack2 = 4;
    [SerializeField] float KnockbackOnAttack3 = 6;
    [SerializeField] bool _enemy = true;
    [SerializeField] ParticleSystem _clawMark;
    

    public float KnockbackMultiplier
    {
        get => _knockbackMultiplier;
        set => _knockbackMultiplier = value;
    }

    private Transform _characterRoot;
    private Targetable _targetable;

    private void Awake()
    {
        _knockbackMultiplier = atkKnck;
        _characterRoot = GetComponentInParent<Health>().transform;
        _targetable = GetComponentInParent<Targetable>();
        if (_characterRoot == null) return;
        if (_targetable == null) return;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Health health) || other == this.gameObject.GetComponentInParent<Collider>()) return;
        if (other.TryGetComponent(out Targetable target))
        {
            if (target.Team != _targetable.Team)
            {
                if (other.TryGetComponent(out Rigidbody rigidbody)) other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.GetComponent<Health>().Damage(_damage, _characterRoot.gameObject, _knockbackMultiplier);
                other.GetComponentInChildren<OnHitParticle>().gameObject.GetComponentInChildren<ParticleSystem>()?.Play();
                if(_enemy) _clawMark.Play();
                other.GetComponent<Animator>().SetTrigger("Stagger");
            }
        }
    }
    
    public void Attack3()
    {
        _knockbackMultiplier = KnockbackOnAttack3;
    }
    public void Attack2()
    {
        _knockbackMultiplier = KnockbackOnAttack2;
    }
    public void Attack1()
    {
        _knockbackMultiplier = KnockbackOnAttack1;
    }
}
