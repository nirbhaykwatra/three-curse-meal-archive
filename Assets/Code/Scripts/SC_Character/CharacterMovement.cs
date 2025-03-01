using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // private serialized fields
    [FoldoutGroup("Movement")]
    [SerializeField] protected float _speed = 5f;
    [FoldoutGroup("Movement")]
    [SerializeField] protected float _acceleration = 10f;
    [FoldoutGroup("Movement")]
    [SerializeField] protected float _turnSpeed = 10f;
    [FoldoutGroup("Movement")]
    [SerializeField] protected bool _controlRotation = true;    // character turns towards movement direction

    [FoldoutGroup("Airborne")]
    [SerializeField] protected float _gravity = -20f;       // custom gravity value
    [FoldoutGroup("Airborne")]
    [SerializeField] protected float _jumpHeight = 2.25f;   // peak height of jump
    [FoldoutGroup("Airborne")]
    [SerializeField] protected float _airControl = 0.1f;    // percentage of acceleration applied while airborne
    [FoldoutGroup("Airborne")]
    [SerializeField] protected bool _airTurning = true;     // character can turn while airborne

    [FoldoutGroup("Grounding")]
    [SerializeField] protected float _groundCheckOffset = 0.1f;     // height inside character where grounding ray starts
    [FoldoutGroup("Grounding")]
    [SerializeField] protected float _groundCheckDistance = 0.4f;   // distance down from offset position
    [FoldoutGroup("Grounding")]
    [SerializeField] protected float _maxSlopeAngle = 40f;          // maximum climbable slope, character will slip on anything higher
    [FoldoutGroup("Grounding")]
    [SerializeField] protected float _groundedFudgeTime = 0.25f;    // leeway time for players to still jump after leaving the ground
    [FoldoutGroup("Grounding")]
    [SerializeField] protected LayerMask _groundMask = 1 << 0;      // mask for layers considered the ground

    // public properties
    public float MoveSpeedMultiplier { get; set; } = 1f;
    public float Speed => _speed;
    public bool IsFudgeGrounded => Time.timeSinceLevelLoad < _lastGroundedTime + _groundedFudgeTime;

    // public-get protected-set properties
    public virtual Vector3 Velocity { get; protected set; }
    public Vector3 MoveInput { get; protected set; }
    public Vector3 LocalMoveInput { get; protected set; }
    public Vector3 LookDirection { get; protected set; }
    public bool HasMoveInput { get; protected set; }
    public bool IsGrounded { get; protected set; }
    public Vector3 SurfaceVelocity { get; protected set; }
    public bool CanMove { get; protected set; } = true;
    public Vector3 GroundNormal { get; protected set; } = Vector3.up;

    public bool CanRotate { get; set; } = true;

    // protected fields
    protected float _lastGroundedTime;

    // methods
    public virtual void Jump() { }
    public virtual void Dash() { }
    public virtual void SetMoveInput(Vector3 input) { }
    public virtual void SetLookDirection(Vector3 direction) { }
    public virtual void SetCanMove(bool canMove) { }
}