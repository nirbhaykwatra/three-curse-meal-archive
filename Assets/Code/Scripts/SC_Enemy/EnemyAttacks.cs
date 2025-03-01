using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttacks : MonoBehaviour
{
    [SerializeField] private float _dampTime = 0.1f;
    private Animator _enemyAnimator;

    public UnityEvent OnStartAttack;
    public UnityEvent OnStopAttack;

    private void Start()
    {
        _enemyAnimator = GetComponent<Animator>();
    }

    public void EnemyAttack()
    {
       // _enemyAnimator.applyRootMotion = true;
        _enemyAnimator.SetTrigger("Attack");
    }
    public void Death()
    {
       // _enemyAnimator.applyRootMotion = true;
        _enemyAnimator.SetBool("IsDead", true);
    }

    // animation event
    public void StartEnemyAttack()
    {
       // _enemyAnimator.applyRootMotion = true;
        _enemyAnimator.ResetTrigger("Attack");

        OnStartAttack.Invoke();
    }

    // animation event
    public void StopEnemyAttack()
    {
       // _enemyAnimator.applyRootMotion = false;
        OnStopAttack.Invoke();
    }
}
