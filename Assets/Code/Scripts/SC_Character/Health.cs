using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [BoxGroup("Health")]
    [ProgressBar(0, "_max", r: 255, g: 0, b: 0, Height = 30)]
    [SerializeField] private float _current;
    [BoxGroup("Health")]
    [SerializeField] private float _max = 100f;
    [BoxGroup("Health")]
    [SerializeField] private float _min = 0f;
        
    public UnityEvent OnReset;
    public UnityEvent OnDeath;
    public UnityEvent OnDamaged;
    [SerializeField] private TextMeshProUGUI _readout;

    private PlayerController _player;
    private CharacterMovement3D _characterMovement;

    public float MinimumHealth
    {
        get => _min;
        set => _min = value;
    }
    public float Percentage => _current * 100 / _max;
    public bool IsAlive => _current > 0f;

    private void Start()
    {
        _player = GetComponent<PlayerController>();
        _characterMovement = GetComponent<CharacterMovement3D>();
        _current = _max;
        if (_readout != null)
        {
            _readout.text = Convert.ToString(_current);
        }
    }

    public void Damage(float amount, GameObject attacker, float _knockbackMultiplier = 0f)
    {
        if (_current >= _min)
        {
            _current = Mathf.Clamp(_current - amount, 0f, _max);
            //_readout.text = Convert.ToString(_current);
            DamageInfo damageInfo = new DamageInfo();
            damageInfo.Attacker = attacker;
            damageInfo.Victim = gameObject;
            damageInfo.Damage = amount;
            _characterMovement.OnDamage(damageInfo, _knockbackMultiplier);
            OnDamaged.Invoke();
        }
    }

    public void Damage(float amount)
    {
        Damage(amount, gameObject);
    }

    public void Heal(float amount)
    {
        _current = Mathf.Clamp(_current + amount, 0f, _max);
    }

    public void Respawn(Transform spawn)
    {
        //if (_player == null) return;
        //_player.transform.position = spawn.position;
        gameObject.transform.position = spawn.position; 
    }

    public void ResetHealth()
    {
        _current = _max;
        OnReset.Invoke();
    }

    public void Kill()
    {
        _current = 0f;
    }

    private void Update()
    {
        if (!IsAlive) OnDeath.Invoke();
    }

    public void CallDeathEvent()
    {
        OnDeath.Invoke();
    }
}
