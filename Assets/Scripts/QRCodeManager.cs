using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine.Rendering;
using Unity.VisualScripting;


public class QRCodeManager : MonoBehaviour
{
    public GameObject debugObject;
    public GameObject Chair;
    public List<GameObject> furnitureList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MRUK.Instance.SceneSettings.TrackableAdded.AddListener(OnQRCodeTracked);
        MRUK.Instance.SceneSettings.TrackableAdded.AddListener(OnChairQRCodeTracked);
    }

    public void OnQRCodeTracked(MRUKTrackable qrCode)
    {
        if (qrCode.TrackableType != OVRAnchor.TrackableType.QRCode)
        {
            Debug.Log("QR not correct");
            return;
        }
        string qrURL = qrCode.MarkerPayloadString;
        if (qrURL.Contains("bgn"))
        {
            QRObjectSpawner(qrCode, debugObject);
            Debug.Log("Object spawned at QR code");
        }
        
    }

    public void OnChairQRCodeTracked(MRUKTrackable qrCode)
    {
        if (qrCode.TrackableType != OVRAnchor.TrackableType.QRCode)
        {
            Debug.Log("QR not correct");
            return;
        }

        string qrURL = qrCode.MarkerPayloadString;
        Debug.Log("QR code tracked with URL: " + qrURL);
        if (qrURL.Contains("chair"))
        {
            Debug.Log("Its a chair!");
            QRObjectSpawner(qrCode, Chair);
        }
    }

    private void QRObjectSpawner(MRUKTrackable qrCode, GameObject prefab)
    {
        Vector3 targetPosition = qrCode.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(-qrCode.transform.forward, qrCode.transform.up);

        GameObject spawned = Instantiate(prefab, targetPosition, targetRotation);

        float width = qrCode.PlaneRect.Value.width;
        float height = qrCode.PlaneRect.Value.height;
        Vector3 targetScale = new Vector3(width, height, 0);

        spawned.transform.localScale = targetScale;
        spawned.transform.parent = qrCode.transform;
    }
}
