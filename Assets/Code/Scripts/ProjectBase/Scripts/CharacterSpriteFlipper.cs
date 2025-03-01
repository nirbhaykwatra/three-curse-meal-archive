using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement2D))]
public class CharacterSpriteFlipper : MonoBehaviour
{
    private CharacterMovement2D _movement;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _movement = GetComponent<CharacterMovement2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_movement.HasMoveInput) return;
        _renderer.flipX = _movement.MoveInput.x < 0f;
    }
}
