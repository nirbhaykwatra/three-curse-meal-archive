using System;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Tasks.Actions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Rock : MonoBehaviour
{
    [SerializeField] private float _floorCrackTime;
    
    [FoldoutGroup("Bridge Rock Variables", false)]
    [SerializeField] private bool _breakBridge = false;
    [SerializeField] private UnityEvent OnImpact;
    private bool _isFalling = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.TryGetComponent(out Floor floor))
        {
            _isFalling = false;
            OnImpact.Invoke();
            //floor.gameObject.GetComponent<Renderer>().material = floor.gameObject.GetComponentInParent<BreakableFloor>()._warningMaterial;
            floor.GetComponentInParent<BreakableFloor>().DisableTrigger();
            StartCoroutine(DestroyFloor(floor));
            
        }
        
        if (collision.collider.gameObject.TryGetComponent(out Ground ground) || collision.collider.gameObject.TryGetComponent(out Rock rock))
        {
            _isFalling = false;
        }

        if (collision.collider.gameObject.TryGetComponent(out Bridge bridge) && _breakBridge)
        {
            _isFalling = false;
            Destroy(collision.gameObject);
            bridge.GetComponentInParent<BridgeEncounter>().CollapseBridge();
            
            if (collision.collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.GetComponent<Health>().Kill();
            }
        }

        if (collision.collider.gameObject.TryGetComponent(out Health health))
        {
            if (_isFalling == false) return;
            else health.Kill();
        }
    }

    private IEnumerator DestroyFloor(Floor floor)
    {
        yield return new WaitForSeconds(_floorCrackTime);
        
        if(floor != null) floor.DestroyFloor();
    }
}
