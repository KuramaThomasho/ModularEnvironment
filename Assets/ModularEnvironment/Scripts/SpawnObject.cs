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
    private float rotationOffset = 0f;
    private Quaternion surfaceRotation;
    private Vector3 lastSurfaceNormal;
    private bool hasSurfaceAlignment = false;

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

        if (!hasHit || currentPrefab == null)
        {
            hasSurfaceAlignment = false;
            return;
        }

        currentPrefab.transform.position = hit.point;

        // Align to the surface only when first hitting it
        if (!hasSurfaceAlignment ||
            Vector3.Angle(lastSurfaceNormal, hit.normal) > 5f)
        {
            surfaceRotation = Quaternion.FromToRotation(
                currentPrefab.transform.up,
                hit.normal
            ) * currentPrefab.transform.rotation;

            lastSurfaceNormal = hit.normal;
            rotationOffset = 90f;
            hasSurfaceAlignment = true;
        }

        // Rotate around the wall/floor normal using the right joystick
        float joystickX = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

        rotationOffset += joystickX * 90f * Time.deltaTime;

        currentPrefab.transform.rotation =
            Quaternion.AngleAxis(rotationOffset, hit.normal)
            * surfaceRotation;
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
