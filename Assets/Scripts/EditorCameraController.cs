using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class EditorCameraController : MonoBehaviour
{
    private Camera _camera;
    public Transform target;
    public Vector2 orbitingAngles = new Vector2(45f, 0);
    public float rotationSpeed = 90f;
    
    [Header("Distance")]
    public float distance;
    public float distanceSensitivity;
    public float[] distances = new float[2] { 1f, 5f };
    
    [Range(-89f, 89f)]
    public float minVerticalAngle = -30f, maxVerticalAngle = 60f;

    private float _cDistance;
    private bool _orbit;
    private bool _orbiting;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private bool ManualRotation()
    {
        Vector2 input = new Vector2(Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal"));
        const float e = 0.001f;

        if (input.x < -e || input.x > e || input.y < -e || input.y > e) {
            orbitingAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            return true;
        }
        
        return false;
    }
    
    void ConstrainAngles () {
        orbitingAngles.x =
            Mathf.Clamp(orbitingAngles.x, minVerticalAngle, maxVerticalAngle);

        if (orbitingAngles.y < 0f) {
            orbitingAngles.y += 360f;
        }
        else if (orbitingAngles.y >= 360f) {
            orbitingAngles.y -= 360f;
        }
    }

    private void Update()
    {
        float zInput = Input.mouseScrollDelta.y;
        distance -= zInput;
        distance = Mathf.Clamp(distance, distances[0], distances[1]);
        _cDistance = Mathf.Lerp(_cDistance, distance, distanceSensitivity * Time.deltaTime);
        _orbiting = ManualRotation();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public Camera GetCamera()
    {
        return _camera;
    }

    private void LateUpdate()
    {
        Quaternion lookRotation = Quaternion.Euler(orbitingAngles);
        
        if (_orbiting) {
            ConstrainAngles();
            lookRotation = Quaternion.Euler(orbitingAngles);
        }

        if (!target) return;
        
        Vector3 targetPoint = target.position;
        Vector3 lookDirection = transform.forward;
        Vector3 lookPosition = targetPoint - lookDirection * _cDistance;
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }
}
