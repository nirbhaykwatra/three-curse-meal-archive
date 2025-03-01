using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Altar : MonoBehaviour, IInteraction
{
    [SerializeField] private UnityEvent OnSetCheckpoint;
    [SerializeField] private GameObject[] _candles;
    [SerializeField] private IntEvent _currentCheckpoint;
    [SerializeField] private GameObject _savingIcon;
    [SerializeField] private float _savingIconTimer = 3f;
    private Checkpoint _checkpoint;
    private void Awake()
    {
        _checkpoint = GetComponent<Checkpoint>();
        if (_currentCheckpoint.CurrentValue == _checkpoint.ID)
            LightCandles();
    }

    public void Interact()
    {
        _checkpoint.SetCurrentCheckpoint();
        OnSetCheckpoint.Invoke();
    }
    public void LightCandles()
    {
        foreach (var item in _candles)
        {
            item.SetActive(true);
        }
    }
    public void ActivateSavingIcon()
    {
        _savingIcon.SetActive(true);
        StartCoroutine(SavingIconTimer());
    }
    public IEnumerator SavingIconTimer()
    {
        float time = 0;
        while(time < _savingIconTimer)
        {
            yield return new WaitForSeconds(1.0f);
            time++;
            Debug.Log(time);
        }
        _savingIcon.SetActive(false);
        yield return null;
    }
}
