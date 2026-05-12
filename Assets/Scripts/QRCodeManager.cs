using UnityEngine;
using Meta.XR.MRUtilityKit;


public class QRCodeManager : MonoBehaviour
{
    public GameObject debugObject;
    public GameObject Chair;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MRUK.Instance.SceneSettings.TrackableAdded.AddListener(OnQRCodeTracked);
    }

    public void OnQRCodeTracked(MRUKTrackable qrCode)
    {
        if (qrCode.TrackableType != OVRAnchor.TrackableType.QRCode)
        {
            Debug.Log("QR not correct");
            return;
        }

        string qrURL = qrCode.MarkerPayloadString;
        Debug.Log("QR code tracked with URL: " + qrURL);

        Vector3 targetPosition = qrCode.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(-qrCode.transform.forward, qrCode.transform.up);

        float width = qrCode.PlaneRect.Value.width;
        float height = qrCode.PlaneRect.Value.height;

        if (qrURL.Contains("chair"))
        {
            Debug.Log("Its a chair!");
            GameObject spawnedChair = Instantiate(Chair, targetPosition, targetRotation);      

            Vector3 targetScale = new Vector3(width, height, 0);

            spawnedChair.transform.localScale = targetScale;
            spawnedChair.transform.parent = qrCode.transform;
        }
        else
        {
            GameObject spawned = Instantiate(debugObject, targetPosition, targetRotation);

            Vector3 targetScale = new Vector3(width, height, 0);

            spawned.transform.localScale = targetScale;
            spawned.transform.parent = qrCode.transform;

            Debug.Log("Object spawned at QR code position with scale: " + targetScale);
        }
        
    }


}
