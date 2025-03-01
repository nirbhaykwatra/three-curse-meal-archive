using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _ID;

    [SerializeField] private IntEvent _currentCheckpoint;

    public int ID
    {
        get => _ID;
        set => _ID = value;
    }

    public void SetCurrentCheckpoint()
    {
        _currentCheckpoint.Invoke(_ID);
        Debug.Log($"Checkpoint set! ID:{_currentCheckpoint.CurrentValue}");
    }
}
