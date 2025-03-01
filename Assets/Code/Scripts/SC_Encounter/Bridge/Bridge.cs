using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bridge : MonoBehaviour
{
    private List<Rigidbody> _bridgePieces = new List<Rigidbody>();

    private void Start()
    {
        foreach (Rigidbody piece in GetComponentsInChildren<Rigidbody>())
        {
            _bridgePieces.Add(piece);
            piece.isKinematic = false;
        }
    }

    private void OnEnable()
    {
        foreach (Rigidbody piece in GetComponentsInChildren<Rigidbody>())
        {
            _bridgePieces.Add(piece);
            piece.isKinematic = false;
        }
    }

    public void Fall()
    {
        foreach (Rigidbody piece in _bridgePieces)
        {
            piece.isKinematic = false;          
        }
    }
}
