using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class PlaneThemeManager : MonoBehaviour
{
    ARPlane m_ARPlane;
    MeshRenderer m_PlaneMeshRenderer;

    // This is the theme 
    private Theme currentTheme;

    SystemManager systemManager;
    
    // This is the material from the scriptable materials
    public ThemeMaterial themeMaterial;

    void Awake()
    {
        m_ARPlane = GetComponent<ARPlane>();
        m_PlaneMeshRenderer = GetComponent<MeshRenderer>();

        //System Manager holds the global theme.
        systemManager = FindAnyObjectByType<SystemManager>();
    }

    void Start()
    {
        currentTheme = systemManager.globalTheme;
        // This updates material depending on theme on start
        UpdatePlaneMaterial(systemManager.globalTheme);
        
    }

    private void Update()
    {
        if (currentTheme != systemManager.globalTheme)
        {
            UpdatePlaneMaterial(systemManager.globalTheme);
            currentTheme = systemManager.globalTheme;
            //Debug.Log("Theme updated");
        }
    }

    void UpdatePlaneMaterial(Theme currentTheme)
    {

        switch (m_ARPlane.classifications)
        {
            case PlaneClassifications.Floor:
                m_PlaneMeshRenderer.material = themeMaterial.floorMaterials[(int)currentTheme];
                //Debug.Log("Its a Floor");
                break;
            case PlaneClassifications.WallFace:
                m_PlaneMeshRenderer.material = themeMaterial.wallMaterials[(int)currentTheme];
                //Debug.Log("Its a wall");
                break;
            case PlaneClassifications.Ceiling:
                m_PlaneMeshRenderer.material = themeMaterial.ceilingMaterials[(int)currentTheme];
                //Debug.Log("Its a ceiling");
                break;
            case PlaneClassifications.Table:
                m_PlaneMeshRenderer.material = themeMaterial.tableMaterials[(int)currentTheme];
                //Debug.Log("Its a Table");
                break;
            case PlaneClassifications.DoorFrame:
                m_PlaneMeshRenderer.material = themeMaterial.doorFrameMaterials[(int)currentTheme];
                //Debug.Log("Its a Door");
                break;
            case PlaneClassifications.WindowFrame:
                m_PlaneMeshRenderer.material = themeMaterial.windowFrameMaterials[(int)currentTheme];
                //Debug.Log("Its a Window");
                break;
        }
    }
}




