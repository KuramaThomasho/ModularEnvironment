using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine.Rendering;
using Unity.VisualScripting;

//Add to ENUM when creating new QR code types
public enum QRCodeType
{
    Chair,
    Table
}

public class QRCodeManager : MonoBehaviour
{
    public GameObject debugObject;
    public List<GameObject> physicalObjects;

    //Adding listeners for the QR code tracking event.
    void Start()
    {
        MRUK.Instance.SceneSettings.TrackableAdded.AddListener(OnQRCodeTracked);
        MRUK.Instance.SceneSettings.TrackableAdded.AddListener(OnChairQRCodeTracked);
        Debug.Log("Adding Listeners");
    }

    public void OnQRCodeTracked(MRUKTrackable qrCode)
    {
        //Getting the URL in string form from QR code
        string qrURL = qrCode.MarkerPayloadString;

        if (qrCode.TrackableType != OVRAnchor.TrackableType.QRCode)
        {
            Debug.Log("QR not correct");
            return;
        }

        if (qrURL.Contains("bgn"))
        {
            QRObjectSpawner(qrCode, debugObject);
            Debug.Log("Object spawned at QR code");
        }

    }

    public void OnChairQRCodeTracked(MRUKTrackable qrCode)
    {
        //Getting the URL in string form from QR code
        string qrURL = qrCode.MarkerPayloadString;

        if (qrCode.TrackableType != OVRAnchor.TrackableType.QRCode)
        {
            Debug.Log("QR not correct");
            return;
        }
        Debug.Log("QR code tracked with URL: " + qrURL);
        if (qrURL.Contains("chair"))
        {
            Debug.Log("Its a chair!");
            QRObjectSpawner(qrCode, physicalObjects[(int)QRCodeType.Chair]);
        }
    }

    private void QRObjectSpawner(MRUKTrackable qrCode, GameObject prefab)
    {
        Vector3 targetPosition = qrCode.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(qrCode.transform.forward, qrCode.transform.up);

        GameObject spawned = Instantiate(prefab, targetPosition, targetRotation);
        spawned.transform.Rotate(0, 90, 0);
    }

    //public void ExampleFunction(MRUKTrackable qrCode)
    //{
    //    Getting the URL in string form from QR code
    //    As long as the QR code url has the keyword you would like, any URL would work and any free application QR code generator would work too
    //    I found that using canva works pretty well without any logos on the QR code.

    //    This line is to get the QR url and make it into a string.
    //    string qrURL = qrCode.MarkerPayloadString;

    //    This checks if it is an actual QR code that is trackable.
    //    if (qrCode.TrackableType != OVRAnchor.TrackableType.QRCode)
    //    {
    //        Debug.Log("QR not correct");
    //        return;
    //    }

    //    After which this triggers if the URL has the keyword you are looking for
    //    if (qrURL.Contains("bgn"))
    //    {
    //        QRObjectSpawner(qrCode, physicalObjects[(int)QRCodeType.Thing]);
              //This part spawns the object, use the list that is set public for the script. All prefabs can be added to it and keep in mind the ENUM order when adding things.

    //        Debug.Log("Object spawned at QR code");
    //    }

    //}
}
