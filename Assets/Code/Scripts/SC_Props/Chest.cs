using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject[] _loot;
    [SerializeField] private GameObject _spawnPoint;
    private bool _full = true;
    private Animator _animator;
    public UnityEvent _opened;

    private FMOD.Studio.EventInstance chestOpen; //For chest sfx
    public FMODUnity.EventReference chestOpenEventPath;


    private void Start() //For Chest SFX
    {
        _animator = GetComponent<Animator>();
        chestOpen = FMODUnity.RuntimeManager.CreateInstance(chestOpenEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(chestOpen, transform, GetComponent<Rigidbody>());
    } //For Chest SFX

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && _full == true)
        {
            chestOpen.start();
            FMODUnity.RuntimeManager.PlayOneShot(chestOpenEventPath, transform.position);
            _full = false;
            _animator.SetTrigger("IsOpened");
        }
    }
    private void OnOpened()
    {
        foreach (var obj in _loot)
        {
            Instantiate(obj, _spawnPoint.transform.position, Quaternion.identity);
        }
        _opened.Invoke();
    }
}
