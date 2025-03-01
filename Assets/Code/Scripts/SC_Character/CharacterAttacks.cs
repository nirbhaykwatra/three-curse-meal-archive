using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAttacks : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private GameObject _dashParticle;
    public UnityEvent OnStartLightAttack;
    public UnityEvent OnStopLightAttack;
    public UnityEvent OnShoot;
    public UnityEvent OnPepperBlast;
    public UnityEvent OnBroccoliRing;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void LightAttack()
    {
        _animator.SetTrigger("LightAttack");
    }
    public void Dash()
    {
        if(_animator.GetFloat("Speed") > 0.5) _animator.SetTrigger("Dash");
        _dashParticle.GetComponent<ParticleSystem>().Play();
    }
    /*public void StopDashTrail()
    {
        _dashParticle.SetActive(false);
        _dashParticle.SetActive(true);
    }*/

    // animation event
    public void SlowMovementIn()
    {
        _animator.gameObject.GetComponent<CharacterMovement3D>().MoveSpeedMultiplier = 0.1f;
    }

    // animation event
    public void SlowMovementOut()
    {
        _animator.gameObject.GetComponent<CharacterMovement3D>().MoveSpeedMultiplier = 1f;
    }

    // animation event
    public void ActivateRootMotion()
    {
        _animator.applyRootMotion = true;
    }

    // animation event
    public void DeactivateRootMotion()
    {
        _animator.applyRootMotion = false;
    }

    // animation event
    public void StartLightAttack()
    {
        _animator.ResetTrigger("LightAttack");

        OnStartLightAttack.Invoke();
    }

    // animation event
    public void StopLightAttack()
    {
        OnStopLightAttack.Invoke();
    }

    public void ShootPea()
    {
        OnShoot.Invoke();
    }

    // pepper blast
    public void AE_PepperBlast()
    {
        OnPepperBlast.Invoke();
    }

    // broccoli ring
    public void AE_BroccoliRing()
    {
        OnBroccoliRing.Invoke();
    }
}
