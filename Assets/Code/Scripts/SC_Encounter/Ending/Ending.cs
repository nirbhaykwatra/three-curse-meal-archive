using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Ending : MonoBehaviour
{
    [SerializeField] private float _waitBeforeEndScreen;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private UnityEvent AfterGameEnd;
    [SerializeField] private BoolEvent _isEnded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            _isEnded.Invoke(true);
            StartCoroutine(ShowEndScreen());
        }
    }
    

    private IEnumerator ShowEndScreen()
    {
        AfterGameEnd.Invoke();
        yield return new WaitForSeconds(_waitBeforeEndScreen);
        _endScreen.SetActive(true);
    }
}
