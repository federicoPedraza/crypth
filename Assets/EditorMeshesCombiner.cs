using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponEditorController))]
public class EditorMeshesCombiner : MonoBehaviour
{
    private bool _hasConfirmed;
    public KeyCode combineKey = KeyCode.Space;
    public float confirmDelay;
    public float confirmTotalDelay = 1.5f;
    public bool IsConfirming => confirmDelay < confirmTotalDelay;
    public MeshFilter resultMesh;
    private WeaponEditorController _weaponEditorController;

    private void Start()
    {
        _weaponEditorController = GetComponent<WeaponEditorController>();
    }

    private void Update()
    {
        if (!_weaponEditorController.HasVoxels || _hasConfirmed)
        {
            confirmDelay = confirmTotalDelay;
            return;
        }
        
        if (Input.GetKey(combineKey))
            confirmDelay -= Time.deltaTime;
        else
            confirmDelay += Time.deltaTime * 2;

        confirmDelay = Mathf.Clamp(confirmDelay, 0, confirmTotalDelay);

        if (confirmDelay > 0) return;

        _hasConfirmed = true;
        Confirm();
    }

    private void Confirm()
    {
        MeshFilter[] meshes =  _weaponEditorController.Stop();
        CombineInstance[] combine = new CombineInstance[meshes.Length];

        int i = 0;
        while (i < meshes.Length)
        {
            combine[i].mesh = meshes[i].sharedMesh;
            combine[i].transform = meshes[i].transform.localToWorldMatrix;
            meshes[i].gameObject.SetActive(false);

            i++;
        }

        resultMesh.mesh = new Mesh();
        resultMesh.mesh.CombineMeshes(combine);
        resultMesh.gameObject.SetActive(true);
    }
}
