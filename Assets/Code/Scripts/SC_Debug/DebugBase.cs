using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class DebugBase<T> : MonoBehaviour
{
    [SerializeField] protected T _value;
    [SerializeField] protected KeyCode _key;
    [SerializeField] protected UnityEvent<T> _onDebug;

    public abstract void CallDebug();
}
