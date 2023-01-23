using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector2 _movementInput;
    private float _cSpeed;

    public KeyCode sprintKey = KeyCode.LeftShift;
    public bool IsRunning => Input.GetKey(sprintKey);
    private const float SpeedMultiplier = 11;
    public float speed = 3.5f;
    public float runningSpeed = 5.5f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movementInput = Vector2.zero;
    }

    private void Update()
    {
        _cSpeed = GetFinalSpeed();
        _movementInput = HandleInput();
    }

    private Vector3 HandleInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 velocity = new Vector3(x, z);
        return velocity.normalized;
    }

    private float GetFinalSpeed()
    {
        return (IsRunning ? runningSpeed : speed) * SpeedMultiplier;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 direction = Vector3.right * _movementInput.x + Vector3.forward * _movementInput.y;
        Vector3 velocity = direction * _cSpeed;
        _rigidbody.AddForce(velocity, ForceMode.Force);
    }
}
