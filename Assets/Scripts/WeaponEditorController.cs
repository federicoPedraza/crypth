using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponEditorController : MonoBehaviour
{
    public bool enabled = true;
    private EditorCameraController _camera;
    public Transform bounds;
    public EditorCameraController cameraEditor;
    public GameObject voxelCube;
    public GameObject voxelCubeGhost;
    public LayerMask editorLayer;

    private Vector3 _mousePoint;
    private GameObject _currentCubeGhost;
    private GameObject _currentCubeTarget;
    private bool _activeTarget;

    private Dictionary<GameObject, Vector3> Grid = new Dictionary<GameObject, Vector3>();
    public bool HasVoxels => Grid.Count > 0; 

    private void Awake()
    {
        _camera = FindObjectOfType<EditorCameraController>();
        if (!_currentCubeGhost)
            _currentCubeGhost = Instantiate(voxelCubeGhost, transform);
    }

    private void Update()
    {
        if (!enabled) return;
        RegisterMouseInput();

        if (Input.GetMouseButtonDown(2))
            cameraEditor.SetTarget(_currentCubeTarget ? _currentCubeTarget.transform : bounds);

        if (!_activeTarget || !_currentCubeTarget) return;
        
        if (Input.GetMouseButtonDown(0))
            AddVoxelCube(_currentCubeGhost.transform.position, _currentCubeTarget);

        if (Input.GetMouseButtonDown(1))
            RemoveCube(_currentCubeTarget);
    }

    private void AddVoxelCube(Vector3 position, GameObject target)
    {
        Vector3 positionOnGrid = GetVoxelPosition(target.transform, position);
        if (Grid.ContainsValue(positionOnGrid)) return;

        GameObject nVoxel = Instantiate(voxelCube, transform);
        nVoxel.transform.position = positionOnGrid;
        Grid.Add(nVoxel, positionOnGrid);
    }

    private void RemoveCube(GameObject target)
    {
        if (!Grid.ContainsKey(target)) return;
        Grid.Remove(target);

        if (cameraEditor.target == target.transform)
        {
            Transform lastGridVoxel = null;
            if (HasVoxels)
                lastGridVoxel = Grid.Keys.Last().transform;
            cameraEditor.SetTarget(lastGridVoxel ? lastGridVoxel : bounds);
        }
        
        Destroy(target);
    }

    public MeshFilter[] Stop()
    {
        if (!HasVoxels) return null;

        enabled = false;
        return transform.GetComponentsInChildren<MeshFilter>();
    }

    private Vector3 GetVoxelPosition(Transform neighbour, Vector3 input)
    {
        float cellSize = -0.1f;
        Vector3 nPos = neighbour.position;
        Vector3 d = nPos - input;
        Vector3 side = Vector3.zero;
        if (d.x >= 0.049f)
            side = Vector3.right;
        else if (d.x <= -0.049f)
            side = -Vector3.right;
        else if (d.y >= 0.049f)
            side = Vector3.up;
        else if (d.y <= -0.049f)
            side = -Vector3.up;
        else if (d.z >= 0.049f)
            side = Vector3.forward;
        else if (d.z <= 0 - .049f)
            side = -Vector3.forward;

        side = nPos + (side * cellSize);

        return side;
    }

    private void RegisterMouseInput()
    {
        Ray ray = _camera.GetCamera().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, editorLayer))
        {
            _currentCubeGhost.transform.position = hit.point;
            _currentCubeTarget = hit.collider.gameObject;
            _activeTarget = true;
            return;
        }

        _activeTarget = true;
    }
    

    private void OnDrawGizmosSelected()
    {
        if (!_camera) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_camera.gameObject.transform.position, _mousePoint);
    }
}
