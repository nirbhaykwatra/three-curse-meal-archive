using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelLoad : MonoBehaviour
{
    [SerializeField] private UnityEvent _loadUnload;
    
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        _loadUnload.Invoke();
    }
}
