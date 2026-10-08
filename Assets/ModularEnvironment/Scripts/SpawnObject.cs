using System.Security.Cryptography;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private LayerMask allowedLayer;
    private bool isPlacing;
    private GameObject currentPrefab;

    private void Update()
    {
        if (!isPlacing)
            return;

        UpdatePreview();

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
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
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, allowedLayer))
        {
            currentPrefab.transform.position = hitInfo.point;
            currentPrefab.transform.rotation = Quaternion.LookRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void CancelPlacement()
    {
        Destroy(currentPrefab);
    }

    public void SelectObject(GameObject gameObject)
    {
        currentPrefab = gameObject;

        Instantiate(currentPrefab, transform.position, transform.rotation);

        isPlacing = true;
    }
}
