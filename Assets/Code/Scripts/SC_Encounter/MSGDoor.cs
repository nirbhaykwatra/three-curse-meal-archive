using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSGDoor : MonoBehaviour
{
    [SerializeField] private Resources _resources;
    [SerializeField] private int _requiredCrystals;
    private Animator _doorAnimator;
    
    private void Start()
    {
        _doorAnimator = GetComponentInChildren<Animator>();
    }

    public void CheckDoor()
    {
        if (_resources.Crystal >= _requiredCrystals)
        {
            _doorAnimator.SetTrigger("Open");
        }
        else
        {
            _doorAnimator.SetTrigger("Close");
        }
    }
}
