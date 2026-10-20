using System.Security.Cryptography;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private float rayDistance = 10f;
    public EnvironmentRaycastManager envRayManager;
    public Transform rayStartPoint;
    private bool isPlacing;
    private GameObject currentPrefab;

    private void Update()
    {
        if (!isPlacing)
            return;

        UpdatePreview();

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            isPlacing = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            CancelPlacement();
        }
    }

    private void UpdatePreview()
    {
        Ray ray = new Ray(rayStartPoint.position, rayStartPoint.forward);

        bool hasHit = envRayManager.Raycast(ray, out var hit, rayDistance);

        if (hasHit)
        {
            currentPrefab.transform.position = hit.point;
            currentPrefab.transform.rotation = Quaternion.LookRotation(Vector3.up, hit.normal);
        }
    }

    private void CancelPlacement()
    {
        DestroyImmediate(currentPrefab);

        isPlacing = false;
    }

    public void SelectObject(GameObject gameObject)
    {
        if (isPlacing)
            return;

        if (gameObject == null)
        {
            Debug.LogError("No prefab was assigned to SelectObject!");
            return;
        }

        Vector3 spawnPosition;

        if (rayStartPoint != null)
        {
            spawnPosition = rayStartPoint.position + rayStartPoint.forward * 2f;
        }
        else
        {
            spawnPosition = transform.position;
        }

        currentPrefab = Instantiate(gameObject, spawnPosition, Quaternion.identity);

        isPlacing = true;
    }
}
