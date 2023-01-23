using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelCube : MonoBehaviour
{
    public bool removable = true;
    public Vector3 position;

    private void Start()
    {
        position = transform.localPosition;
    }
}
