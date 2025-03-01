using System;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Tasks.Actions;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Loot : MonoBehaviour
{
    [SerializeField] private Vector2 _speedRange = new Vector2(5f, 10f);
    [SerializeField] private float _despawnTimer;
    [SerializeField] private LootDrop _effect;
    [SerializeField] private UnityEvent OnCollected;

    private GameObject _player;
    private Rigidbody _rb;
    private float _speed;
    public float waitTime = 2;
    private float timer;
    private bool _isAttracted = false;

    public LootDrop LootEffect
    {
        get => _effect;
        set => _effect = value;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _speed = Random.Range(_speedRange.x, _speedRange.y);
        Instantiate(_effect.ArtPrefab, transform.position, transform.rotation, transform);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>=waitTime)
        {
            timer = 0;
            _isAttracted = true;
        }
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5f + transform.forward , ForceMode.Impulse );
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(_despawnTimer);
        
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_player == null || _isAttracted == false) return;
        Vector3 position = Vector3.Lerp(transform.position, _player.transform.position, Time.fixedDeltaTime * _speed);
        _rb.MovePosition(position);
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.collider.gameObject.TryGetComponent(out PlayerController player))
       {
           /* GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().isTrigger = true;*/
           Effect(player.gameObject);
           OnCollected.Invoke();

       }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.TryGetComponent(out PlayerController player))
        {
            _player = player.gameObject;
        }
    }

    protected virtual void Effect(GameObject affected)
    {
        _effect?.Activate(affected);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if(_player != null)
        {
            Debug.DrawLine(transform.position, _player.transform.position);
        }
    }
}
