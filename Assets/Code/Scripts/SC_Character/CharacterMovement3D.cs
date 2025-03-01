using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

#pragma warning disable 649

// 3D implentation 
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement3D : CharacterMovement
{
    #region SerializedFields
    
    [FoldoutGroup("Dashing")] 
    [SerializeField] private float _dashDistance = 3.5f;
    [FoldoutGroup("Dashing")] 
    [SerializeField] private float _dashDuration = 0.3f;
    [FoldoutGroup("Dashing")] 
    [SerializeField] private float _dashCheckOffset = 0.3f;
    [FoldoutGroup("Dashing")] 
    [SerializeField] private float _dashCooldownInSeconds = 1.0f;
    [FoldoutGroup("Dashing")] 
    [SerializeField] private float _dashDamage = 10f;
    [FoldoutGroup("Dashing")] 
    [SerializeField] private UnityEvent OnDash;

    [FoldoutGroup("Combat")]
    [FoldoutGroup("Combat/Damage")]
    [SerializeField] private UnityEvent Damage;
    [Header("Knockback")]
    [FoldoutGroup("Combat/Knockback")] 
    private float _selfKnockbackMultiplier = 0f;
    [FoldoutGroup("Combat/Knockback")] 
    [SerializeField] private float _damageOnCollsion = 50;
    [FoldoutGroup("Combat/Knockback")] 
    [SerializeField] private float _damageVelocity = 10f;
    [FoldoutGroup("Combat/Abilities")] 
    [Required("Ability not plugged in!", InfoMessageType.Error)]
    [SerializeField] private PepperBlast _pepperBlast;

    #endregion Serialized fields

    #region Properties
    public override Vector3 Velocity { get => _rigidbody.velocity; protected set => _rigidbody.velocity = value; }
    
    public float KnockbackMultiplier { get => _selfKnockbackMultiplier; set => _selfKnockbackMultiplier = value; }

    // properties
    public float TurnSpeedMultiplier { get; set; } = 1f;
    public float GravityMultiplier { get; private set; } = 1f;
    private Vector3 _groundCheckStart => transform.position + transform.up * _groundCheckOffset;
    private Vector3 _dashCheckStart => transform.position + transform.up + transform.forward * _dashCheckOffset;
    
    #endregion Properties

    #region PrivateFields
    
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;
    private Vector3 _dashDestination;
    private Vector3 _dashLookDir;
    private RaycastHit _dashCheckRay;
    private Ray _hitRay;
    private bool _dashHit;
    private float _timer;
    private bool _canDash;

    private Health _health;
    private List<Collider> _colliders = new List<Collider>();

    #endregion PrivateFields

    #region SfxImplementation
    private FMOD.Studio.EventInstance playerDashSfx;
    public FMODUnity.EventReference playerDashSfxEventPath;

    #endregion


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.useGravity = false;
        
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;

        _health = GetComponent<Health>();
        
        LookDirection = transform.forward;
        _timer = _dashCooldownInSeconds;

        _canDash = true;

        playerDashSfx = FMODUnity.RuntimeManager.CreateInstance(playerDashSfxEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerDashSfx, transform, GetComponent<Rigidbody>());
    }
    
    #region MoveLookMethods
    
    public override void SetMoveInput(Vector3 input)
    {
        if(!CanMove)
        {
            MoveInput = Vector3.zero;
            return;
        }

        input = Vector3.ClampMagnitude(input, 1f);
        // set input to 0 if small incoming value
        HasMoveInput = input.magnitude > 0.1f;
        input = HasMoveInput ? input : Vector3.zero;
        // remove y component of movement but retain overall magnitude
        Vector3 flattened = new Vector3(input.x, 0f, input.z);
        flattened = flattened.normalized * input.magnitude;
        MoveInput = flattened;
        // finds movement input as local direction rather than world direction
        LocalMoveInput = transform.InverseTransformDirection(MoveInput);
    }

    public void SetLookPosition(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        SetLookDirection(direction);
    }

    // sets character look direction, flattening y-value
    public override void SetLookDirection(Vector3 direction)
    {
        if (!CanMove || direction.magnitude < 0.1f) return;
        LookDirection = new Vector3(direction.x, 0f, direction.z).normalized;
    }
    
    #endregion MoveLookMethods
    
    #region Mechanics

    // attempts a jump, will fail if not grounded
    public override void Jump()
    {
        if (!CanMove || !IsFudgeGrounded) return;
        // calculate jump velocity from jump height and gravity
        float jumpVelocity = Mathf.Sqrt(2f * -_gravity * _jumpHeight);
        // override current y velocity but maintain x/z velocity
        Velocity = new Vector3(Velocity.x, jumpVelocity, Velocity.z);
    }

    public override void Dash()
    {
        if (!IsFudgeGrounded || !_canDash) return;

        _timer = 0f;
        OnDash.Invoke();
        
        StartCoroutine(DashRoutine(_dashDistance, _dashDuration));

        playerDashSfx.start(); //sfx implementation
    }

    // interpolates player position to dash location using physics
    private IEnumerator DashRoutine(float distance, float duration, float checkRadius = 0.25f, float checkOffset = 1f)
    {
        // disable movement/gravity
        CanMove = true;
        GravityMultiplier = 1f;

        // find start/end positions
        Vector3 direction = MoveInput.magnitude > 0.1f ? MoveInput : transform.forward;
        Vector3 start = transform.position;
        // TODO: check if destination is in floor/wall
        Vector3 destination = direction * distance + start;

        // check if dash will intersect with wall
        Ray ray = new Ray(start + Vector3.up * checkOffset, direction);
        if(Physics.SphereCast(ray, checkRadius, out RaycastHit hit, distance - checkRadius, _groundMask))
        {
            destination = start + (direction * hit.distance);
            Debug.DrawLine(ray.origin, hit.point, Color.cyan, 1f);
        }

        if (Physics.Raycast(ray, out RaycastHit hitInfo, distance, _groundMask))
        {
            destination = start + (direction * hit.distance);
            Debug.DrawLine(ray.origin, hitInfo.point, Color.cyan, 1f);
        }
        
        // if (Physics.Raycast(ray, out RaycastHit enemyInfo, distance))
        // {
        //     if (enemyInfo.collider.gameObject.layer == _blastLayerMask)
        //     {
        //         destination = start + direction * distance;
        //         enemyInfo.collider.GetComponent<Health>().Damage(_dashDamage);
        //     }
        // }

        Debug.DrawLine(start, destination, Color.magenta, 1f);

        // interpolate along dash line
        float velocity = distance / duration;
        duration = Vector3.Distance(start, destination) / velocity;
        float progress = 0f;
        float timer = 0f;
        while(progress < 1f)
        {
            timer += Time.deltaTime;
            progress = timer / duration;

            Vector3 position = Vector3.Lerp(start, destination, progress);
            _rigidbody.MovePosition(position);

            yield return null;
        }

        //re-enable movement/gravity
        CanMove = true;
        GravityMultiplier = 1f;
    }

    public void OnDamage(DamageInfo damageInfo, float knockbackMultiplier)
    {
        Vector3 knockbackDir = (damageInfo.Victim.transform.position - damageInfo.Attacker.transform.position).normalized;
        //Debug.Log("Knockback multiplier: " + knockbackMultiplier);
        _rigidbody.AddForce(knockbackDir * knockbackMultiplier, ForceMode.Impulse);
        //Debug.Log($"{gameObject.name} health: {_health.Percentage}");
    }

    public void OnDamage(DamageInfo damageInfo)
    {
        //Debug.Log("Stupid OnDamage was called");
        OnDamage(damageInfo, _selfKnockbackMultiplier);
    }

    public void PepperBlast()
    {
        _pepperBlast.PepperBlastAbility(transform.position, this.gameObject);
    }

    public void RangedAttack(Vector3 target, GameObject projectile, float duration = 0.5f)
    {
        GameObject instance = Instantiate(projectile, transform.position + Vector3.up + transform.forward, transform.rotation);
        instance.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f - (transform.position - target).normalized * 10f, ForceMode.Impulse);
        //StartCoroutine(RangedAttackRoutine(instance, duration));
    }

    #endregion Mechanics
    
    #region NavMeshMethods

    // path to destination using navmesh
    public void MoveTo(Vector3 destination, float stoppingDistance = 0.5f)
    {
        if (!_navMeshAgent.isActiveAndEnabled || !_navMeshAgent.isOnNavMesh) return;
        float distance = Vector3.Distance(destination, transform.position);        
        if(distance < stoppingDistance || destination == null) Stop();
        else _navMeshAgent.SetDestination(destination);
    }

    public void MoveAway(Vector3 start, float stoppingDistance = 0.5f)
    {
        Vector3 destination = transform.position - start;
        Vector3 moveTo = Vector3.MoveTowards(_navMeshAgent.gameObject.transform.position, destination, -_speed * Time.deltaTime);

        if ((_navMeshAgent.gameObject.transform.position - destination).magnitude >= stoppingDistance)
        {
            Stop();
        }
        else
        {
            _navMeshAgent.SetDestination(destination);
        }
    }

    // stop all movement
    public void Stop()
    {
        if (!_navMeshAgent.isActiveAndEnabled || !_navMeshAgent.isOnNavMesh) return;
        _navMeshAgent.ResetPath();
        SetMoveInput(Vector3.zero);        
    }
    
    #endregion NavMeshMethods

    // allows character to move and rotate
    public override void SetCanMove(bool canMove)
    {
        CanMove = canMove;
        if (!CanMove) Stop();
    }

    private void FixedUpdate()
    {
        // find flattened movement vector based on ground normal
        Vector3 input = MoveInput;
        Vector3 right = Vector3.Cross(transform.up, input);
        Vector3 forward = Vector3.Cross(right, GroundNormal);

        // calculates direction movement velocity
        Vector3 targetVelocity = forward * (_speed * MoveSpeedMultiplier);
        if (!CanMove) targetVelocity = Vector3.zero;
        // adds velocity of surface under character, if character is stationary
        targetVelocity += SurfaceVelocity * (1f - Mathf.Abs(MoveInput.magnitude));
        // calculates acceleration required to reach desired velocity and applies air control if not grounded
        Vector3 velocityDiff = targetVelocity - Velocity;
        velocityDiff.y = 0f;
        float control = IsGrounded ? 1f : _airControl;
        Vector3 acceleration = velocityDiff * (_acceleration * control);
        // zeros acceleration if airborne and not trying to move (allows for nice jumping arcs)
        if (!IsGrounded && !HasMoveInput) acceleration = Vector3.zero;
        // add gravity
        acceleration += GroundNormal * _gravity * GravityMultiplier;

        _rigidbody.AddForce(acceleration);

        // rotates character towards movement direction
        if (_controlRotation && (IsGrounded || _airTurning))
        {
            float turnDirection = -Mathf.Sign(Vector3.Cross(LookDirection, transform.forward).y);
            float turnMagnitude = 1f - Mathf.Clamp01(Vector3.Dot(transform.forward, LookDirection));
            float angularVelocity = turnMagnitude * _turnSpeed * TurnSpeedMultiplier * turnDirection;
            _rigidbody.angularVelocity = new Vector3(0f, angularVelocity, 0f);
        }
        
        //dashing
        Vector3 dashStart = _rigidbody.position;
        _dashDestination = dashStart + LookDirection * _dashDistance;
    }

    private void Update()
    {
        // check for the ground every frame
        IsGrounded = CheckGrounded();

        // overrides current input with pathing direction if MoveTo has been called
        if (_navMeshAgent.hasPath && _navMeshAgent.path.corners != null && _navMeshAgent.path.corners.Length >= 2)
        {
            Vector3 nextPathPoint = _navMeshAgent.path.corners[1];
            Vector3 pathDir = (nextPathPoint - transform.position).normalized;
            SetMoveInput(pathDir);
            SetLookDirection(pathDir);
        }
        
        //dash cooldown
        _timer += Time.deltaTime;
        if (_timer < _dashCooldownInSeconds)
        {
            _canDash = false;
        }
        else
        {
            _canDash = true;
        }
        
        // syncs navmeshagent position with character position
        _navMeshAgent.nextPosition = transform.position;
        
        Debug.DrawLine(_rigidbody.position, _dashDestination);
        
    }
    
    #region PhysicsChecks

    private bool CheckGrounded()
    {
        // raycast to find ground
        bool hit = Physics.Raycast(_groundCheckStart, -transform.up, out RaycastHit hitInfo, _groundCheckDistance, _groundMask);

        // set default ground surface normal and SurfaceVelocity
        GroundNormal = Vector3.up;
        SurfaceVelocity = Vector3.zero;

        // if ground wasn't hit, character is not grounded
        if (!hit) return false;

        // gets velocity of surface underneath character if applicable
        if (hitInfo.rigidbody != null) SurfaceVelocity = hitInfo.rigidbody.velocity;

        // test angle between character up and ground, angles above _maxSlopeAngle are invalid
        bool angleValid = Vector3.Angle(transform.up, hitInfo.normal) < _maxSlopeAngle;
        if (angleValid)
        {
            // record last time character was grounded and set correct floor normal direction
            _lastGroundedTime = Time.timeSinceLevelLoad;
            GroundNormal = hitInfo.normal;
            return true;
        }

        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //ContactPoint contact = collision.GetContact(0);
            //transform.position = contact.point - (transform.position - contact.normal);
            if (!gameObject.TryGetComponent(out PlayerController player))
            {
                if (collision.relativeVelocity.magnitude > _damageVelocity)
                {
                    _health.Damage(_damageOnCollsion);
                    if (gameObject.TryGetComponent(out EnemyAttacks enemy))
                    {
                        gameObject.GetComponent<Animator>().SetTrigger("Stagger");
                    }
                }
                _rigidbody.velocity = Vector3.zero;
            }
        }
    }
    
    #endregion PhysicsChecks

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(_groundCheckStart, -transform.up * _groundCheckDistance);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_dashCheckStart, 0.5f);
                     
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_dashDestination + Vector3.up, 0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_dashCheckStart, _dashDestination + Vector3.up);
    }
}