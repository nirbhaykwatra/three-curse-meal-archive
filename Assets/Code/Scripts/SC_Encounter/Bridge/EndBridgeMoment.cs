using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndBridgeMoment : MonoBehaviour
{
    [SerializeField] private Health _player;
    [SerializeField] private float _playerHealthToEndMoment;
    [SerializeField] private float _endMomentDelay;
    [SerializeField] private UnityEvent OnMomentEnd;

    public void EndMoment()
    {
        StartCoroutine(End());
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(_endMomentDelay);
        
        if (_player.Percentage <= _playerHealthToEndMoment)
        {
            OnMomentEnd.Invoke();
        }
    }
}
