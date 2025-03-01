using System;
using System.Collections;
using System.Collections.Generic;
using ParadoxNotion;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BreakableFloor : MonoBehaviour, IResetable
{
    [SerializeField] private float _breakTimer;
    [SerializeField] private float _rockSpawnHeight;
    [SerializeField] private bool _breakWithRock;
    [SerializeField] private BreakableFloorObjects _variables;
    [SerializeField] private LayerMask _ignoreLayer;
    [SerializeField] private UnityEvent OnWarning;
    [SerializeField] private UnityEvent OnBreak;
    [SerializeField] private Material _idleMaterial;
    [SerializeField] public Material _warningMaterial;
    private bool _isBroken = false;
    private int _floorArtInd;
    private GameObject _instantiatedFloor;

    private void Start()
    {
        if (_variables._floorArt != null)
        {
            _floorArtInd = Random.Range(0, _variables._floorArt.Count);
            _variables._floorArt[_floorArtInd].GetComponent<Renderer>().material = _idleMaterial;
            _instantiatedFloor = Instantiate(_variables._floorArt[_floorArtInd], transform.position, transform.rotation, transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _ignoreLayer) return;
        if(_isBroken) return;
        _isBroken = true;
        _instantiatedFloor.GetComponent<Renderer>().material = _warningMaterial;
        StartCoroutine(BreakFloor());
        OnWarning.Invoke();
    }
    private IEnumerator BreakFloor()
    {
        yield return new WaitForSeconds(_breakTimer);
        
        OnBreak.Invoke();

        if (_breakWithRock)
        {
            SpawnRock();
        }
        if (!_breakWithRock)
        {
            DestroyFloor();
        }
    }

    public void DisableTrigger()
    {
        GetComponent<Collider>().enabled = false;
    }

    public void DestroyFloor()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        GetComponent<Collider>().enabled = false;
        foreach (Collider item in colliders)
        {
            item.enabled = false;
        }
        GetComponentInChildren<MeshRenderer>().enabled = false;
        Destroy(GetComponentInChildren<Floor>().gameObject);
    }

    public void Reset()
    {
        foreach(Rock rock in GetComponentsInChildren<Rock>()) 
        {
            Destroy(rock.gameObject);
        }
        _isBroken = false;
        //gameObject.SetActive(true);
        if (_variables._floorArt != null && GetComponentInChildren<Floor>() == null)
        { 
            _floorArtInd = Random.Range(0, _variables._floorArt.Count);
            //_variables._floorArt[_floorArtInd].GetComponent<Renderer>().material = _idleMaterial;
            _instantiatedFloor = Instantiate(_variables._floorArt[_floorArtInd], transform.position, transform.rotation, transform);
            _instantiatedFloor.GetComponent<Renderer>().material = _idleMaterial;
        }
        GetComponent<Collider>().enabled = true;
    }

    public void SpawnRock()
    {
        _variables.SetRockArt();
        Instantiate(_variables._rockArt, gameObject.transform.position + (transform.up * _rockSpawnHeight), Quaternion.Euler(Random.Range(0f, 360f),  Random.Range(0f, 360f), Random.Range(0f, 360f)), transform);
    }

}
