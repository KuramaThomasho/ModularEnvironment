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
                break;
            case PlaneClassifications.WallFace:
                planeMatColor = Color.red;
                break;
            case PlaneClassifications.Ceiling:
                planeMatColor = Color.blue;
                break;
            case PlaneClassifications.Table:
                planeMatColor = Color.yellow;
                break;
            case PlaneClassifications.Seat:
                planeMatColor = Color.magenta;
                break;
            case PlaneClassifications.DoorFrame:
                planeMatColor = Color.cyan;
                break;
            case PlaneClassifications.WindowFrame:
                planeMatColor = Color.white;
                break;
        }

        planeMatColor.a = 0.3f;
        m_PlaneMeshRenderer.material.color = planeMatColor;

    }
}

