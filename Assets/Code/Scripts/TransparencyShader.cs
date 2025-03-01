using System;
using System.Collections;
using System.Collections.Generic;
using ParadoxNotion;
using UnityEngine;

public class TransparencyShader : MonoBehaviour
{
    [SerializeField] private LayerMask _transparentLayer;
    [SerializeField] private GameObject _origin;
    [SerializeField] private Material _changeMaterial;
    private void FixedUpdate()
    {
        if (Physics.Linecast(_origin.transform.position, gameObject.transform.position, out RaycastHit hitInfo, _transparentLayer))
        {
            Debug.DrawLine(_origin.transform.position, gameObject.transform.position, Color.red);
            hitInfo.collider.gameObject.GetComponent<Renderer>().material = _changeMaterial;
        }
    }
}
