using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class PlaneThemeManager : MonoBehaviour
{
    MeshRenderer planeMeshRendererMat;

    public PlaneSurfaces planeSurface;

    // This is the theme 
    private Theme currentTheme;

    SystemManager systemManager;
    
    // This is the material from the scriptable materials
    public ThemeMaterial themeMaterial;

    void Awake()
    {
        planeMeshRendererMat = GetComponent<MeshRenderer>();

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
        switch (planeSurface)
        {
            case PlaneSurfaces.Floor:
                planeMeshRendererMat.material = themeMaterial.floorMaterials[(int)currentTheme];
                //Debug.Log("Its a Floor");
                break;
            case PlaneSurfaces.WallFace:
                planeMeshRendererMat.material = themeMaterial.wallMaterials[(int)currentTheme];
                //Debug.Log("Its a wall");
                break;
            case PlaneSurfaces.Ceiling:
                planeMeshRendererMat.material = themeMaterial.ceilingMaterials[(int)currentTheme];
                //Debug.Log("Its a ceiling");
                break;
            case PlaneSurfaces.WallArt:
                planeMeshRendererMat.material = themeMaterial.wallArtMaterials[(int)currentTheme];
                //Debug.Log("Its a WallArt");
                break;
            case PlaneSurfaces.DoorFrame:
                planeMeshRendererMat.material = themeMaterial.doorFrameMaterials[(int)currentTheme];
                //Debug.Log("Its a Door");
                break;
            case PlaneSurfaces.WindowFrame:
                planeMeshRendererMat.material = themeMaterial.windowFrameMaterials[(int)currentTheme];
                //Debug.Log("Its a Window");
                break;
            default:
                break;
        }
    }
}


public enum PlaneSurfaces
{
    Floor,
    WallFace,
    Ceiling,
    WallArt,
    DoorFrame,
    WindowFrame
}

