using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System;

[System.Serializable]
public class QRCodeObject
{
    public string QRObjectName;
    public GameObject prefab;
    public Vector3 rotation;
}
public class QRCodeManager : MonoBehaviour
{

    [Header("QR code objects")]
    [SerializeField] private List<QRCodeObject> qrCodes = new List<QRCodeObject>();


    //Adding listeners for the QR code tracking event.
    void Start()
    {
        MRUK.Instance.SceneSettings.TrackableAdded.AddListener(OnQRCodeTracked);
        //Debug.Log("Adding Listeners");
    }

    public void OnQRCodeTracked(MRUKTrackable qrCode)
    {
        if (qrCode.TrackableType != OVRAnchor.TrackableType.QRCode)
        {
            //If not a Valid QR code, return
            return;
        }

        //Getting the URL in string form from QR code
        string qrURL = qrCode.MarkerPayloadString;

        foreach (QRCodeObject qRCodeObject in qrCodes)
        {
            if (qrURL.Contains(qRCodeObject.QRObjectName))
            {
                QRObjectSpawner(qrCode, qRCodeObject);
                return;
            }
        }
    }



    private void QRObjectSpawner(MRUKTrackable qrCode, QRCodeObject qrCodeObject)
    {
        Vector3 targetPosition = qrCode.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(qrCode.transform.forward, qrCode.transform.up);

        GameObject spawned = Instantiate(qrCodeObject.prefab, targetPosition, targetRotation);

        spawned.transform.Rotate(qrCodeObject.rotation);
    }


//    public void examplefunction(MRUKTrackable qrCode)
//    {
//        //getting the url in string form from qr code
//        //as long as the qr code url has the keyword you would like, any url would work and any free application qr code generator would work too
//        //i found that using canva works pretty well without any logos on the qr code.

//        //this line is to get the qr url and make it into a string.
//        string qrurl = qrCode.MarkerPayloadString;

//        //this checks if it is an actual qr code that is trackable.
//        if (qrCode.TrackableType != OVRAnchor.TrackableType.QRCode)
//        {
//            Debug.Log("qr not correct");
//            return;
//        }

//        //after which this triggers if the url has the keyword you are looking for
//        if (qrurl.Contains("spacelab"))
//        {
//            QRObjectSpawner(qrCode, physicalObjects[(int)QRCodeType.Spacelab]);
//            //this part spawns the object, use the list that is set public for the script.all prefabs can be added to it and keep in mind the enum order when adding things.

//            Debug.Log("object spawned at qr code");
//        }
//    }
}
