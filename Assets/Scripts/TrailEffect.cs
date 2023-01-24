using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    [Range(0, 0.5f)]
    public float sensitivity;
    public float speed;
    public float threshold = 1f;

    private Rigidbody _rigidbody;
    private TrailRenderer[] _trails;
    private float _cTime;

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
        _trails = GetComponentsInChildren<TrailRenderer>();
    }

    private void Update()
    {
        float magnitude = _rigidbody.velocity.magnitude;
        float dTime = magnitude > threshold ? magnitude : 0;
        _cTime = Mathf.Lerp(_cTime, dTime, speed * Time.deltaTime);
        for (int i = 0; i < _trails.Length; i++)
        {
            _trails[i].time = _cTime * sensitivity;
        }
    }
}
