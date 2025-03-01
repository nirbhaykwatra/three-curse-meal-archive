using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#pragma warning disable 649

// sends input from PlayerInput to attached CharacterMovement components
public class PlayerController : MonoBehaviour
{
    // initial cursor state
    [SerializeField] private CursorLockMode _cursorMode = CursorLockMode.Locked;
    // make character look in Camera direction instead of MoveDirection
    [SerializeField] private bool _lookInCameraDirection;
    [Required("Health Bar is not plugged in!", InfoMessageType.Warning)]
    [SerializeField] private Image _healthBar;

    [SerializeField] private Resources _resources;
    [SerializeField] private BoolEvent _hasGameEnded;
    [SerializeField] private Vector3Event _playerMoveInput;
    [Required("Damage Overlay material is not plugged in!", InfoMessageType.Warning)]
    [SerializeField] private Material _damageOverlay;

    [FoldoutGroup("Checkpoint System Variables")]
    [SerializeField] private IntEvent _currentCheckpoint;
    [FoldoutGroup("Checkpoint System Variables"), InfoBox("Transforms must be placed in order, according to checkpoint IDs", InfoMessageType.Warning)]
    [SerializeField] private List<Transform> _playerSpawns = new List<Transform>();

    private int _currentEncounterID;
    private IInteraction[] _interface;

    //external references
    private CharacterMovement3D _characterMovement;
    private CharacterAnimations _characterAnimations;
    private CharacterAttacks _characterAttacks;
    private PlayerInput _playerInput;
    private SpecialAttacks _specialAttacks;
    private Vector2 _moveInput;
    private Vector2 _zoomInput;
    private Health _health;
    private Button _dash;
    private Button _attack;

    public int CurrentEncounterID
    {
        get => _currentEncounterID;
        set => _currentEncounterID = value;
    }

    public Transform Checkpoint
    {
        get => GetPlayerSpawn();
    }

    private void Awake()
    {
        _specialAttacks = GetComponent<SpecialAttacks>();
        _characterMovement = GetComponent<CharacterMovement3D>();
        _characterAttacks = GetComponent<CharacterAttacks>();
        _playerInput = GetComponent<PlayerInput>();
        _health = GetComponent<Health>();
        _dash = GameObject.Find("Dash")?.GetComponent<Button>();
        _attack = GameObject.Find("Attack")?.GetComponent<Button>();
        Cursor.lockState = _cursorMode;
    }

    private void Start()
    {
        transform.position = GetPlayerSpawn().position;
        _playerMoveInput.Invoke(new Vector3(0, 0, 0));
        _hasGameEnded.Invoke(false);
        _resources.ResetResourcesOnRespawn();
    }

    private Transform GetPlayerSpawn()
    {
        Transform playerSpawn = transform;
        switch (_currentCheckpoint.CurrentValue)
        {
            case 0:
                playerSpawn =  _playerSpawns[0];
                break;
            
            case 1:
                playerSpawn = _playerSpawns[1];
                break;

            case 2:
                playerSpawn = _playerSpawns[2];
                break;

            case 3:
                playerSpawn = _playerSpawns[3];
                break;

            case 4:
                playerSpawn = _playerSpawns[4];
                break;
        }

        return playerSpawn;
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void OnDash()
    {
        _characterMovement.Dash();
        _characterAttacks.Dash();
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(_dash.gameObject, pointer, ExecuteEvents.submitHandler);
    }

    public void OnBlast()
    {
        _characterMovement.PepperBlast();
    }
    public void OnLightAttack()
    {
        _characterAttacks.LightAttack();
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(_attack.gameObject, pointer, ExecuteEvents.submitHandler);
        //_characterMovement.LightAttack();
    }

    private void Update()
    {
        if (_characterMovement == null) return;

        // find correct right/forward directions based on main camera rotation
        Vector3 up = Vector3.up;
        Vector3 right = Camera.main.transform.right;
        Vector3 forward = Vector3.Cross(right, up);
        Vector3 moveInput = forward * _moveInput.y + right * _moveInput.x;

        // send player input to character movement
        if (_hasGameEnded.CurrentValue == true)
        {
            _playerInput.enabled = false;
            _characterMovement.SetMoveInput(new Vector3(0, 0, 1));
        }
        else
        {
            _characterMovement.SetMoveInput(moveInput);
            _characterMovement.SetLookDirection(moveInput);
            if (_lookInCameraDirection) _characterMovement.SetLookDirection(Camera.main.transform.forward);
        }

        if (_healthBar == null) return;
        _healthBar.fillAmount = _health.Percentage / 100;
        
        DamageOverlay();
    }

    private void DamageOverlay()
    {
        if (_health.Percentage > 30)
        {
            _damageOverlay.SetFloat("Falloff", 100f);
            _damageOverlay.SetFloat("Strenght", 0f);
            _damageOverlay.SetFloat("BlinkSpeed", 0f);
            _damageOverlay.SetFloat("_Intensity", 0f);

        }
        
        if (_health.Percentage <= 30 && _health.Percentage > 20)
        {
            _damageOverlay.SetFloat("Falloff", 5.5f);
            _damageOverlay.SetFloat("Strenght", 7f);
            _damageOverlay.SetFloat("BlinkSpeed", 5f);
            _damageOverlay.SetFloat("_Intensity", 1f);
        }
        
        if (_health.Percentage <= 20 && _health.Percentage > 10)
        {
            _damageOverlay.SetFloat("Falloff", 4f);
            _damageOverlay.SetFloat("Strenght", 8f);
            _damageOverlay.SetFloat("BlinkSpeed", 6f);
            _damageOverlay.SetFloat("_Intensity", 1f);
        }
        
        if (_health.Percentage <= 10 && _health.Percentage > 0)
        {
            _damageOverlay.SetFloat("Falloff", 3f);
            _damageOverlay.SetFloat("Strenght", 9f);
            _damageOverlay.SetFloat("BlinkSpeed", 8f);
            _damageOverlay.SetFloat("_Intensity", 1f);
        }
    }

    public void Respawn()
    {
        transform.position = GetPlayerSpawn().position;
        _health.ResetHealth();
        _damageOverlay.SetFloat("_Intensity", 0f);
    }
}
