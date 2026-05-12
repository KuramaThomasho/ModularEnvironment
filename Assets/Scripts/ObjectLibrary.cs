using UnityEngine;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectLibrary : MonoBehaviour
{
    //Set up class for soring the type of things that are in the scene
    ARPlane m_ARPlane;
    MeshRenderer m_PlaneMeshRenderer;


    //Prevention of duplication
    void Awake()
    {
        m_ARPlane = GetComponent<ARPlane>();
        m_PlaneMeshRenderer = GetComponent<MeshRenderer>();
        Debug.Log("Got the plane and mesh renderer");
    }

    void Start()
    {
        UpdatePlaneColor();

    }


    void UpdatePlaneColor()
    {
        Color planeMatColor = Color.gray;

        switch (m_ARPlane.classifications)
        {
            case PlaneClassifications.Floor:
                planeMatColor = Color.green;
                Debug.Log("Its a Floor");
                break;
            case PlaneClassifications.WallFace:
                planeMatColor = Color.red;
                Debug.Log("Its a wall");
                break;
            case PlaneClassifications.Ceiling:
                planeMatColor = Color.blue;
                Debug.Log("Its a ceiling");
                break;
            case PlaneClassifications.Table:
                planeMatColor = Color.yellow;
                Debug.Log("Its a Table");
                break;
            case PlaneClassifications.Seat:
                planeMatColor = Color.magenta;
                Debug.Log("Its a Seat");
                break;
            case PlaneClassifications.DoorFrame:
                planeMatColor = Color.cyan;
                Debug.Log("Its a Door");
                break;
            case PlaneClassifications.WindowFrame:
                planeMatColor = Color.white;
                Debug.Log("Its a Window");
                break;
        }

        planeMatColor.a = 0.3f;
        m_PlaneMeshRenderer.material.color = planeMatColor;

    }
}

