using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class TogglePlane : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private InputActionReference togglePlaneAction;

    private ARPlaneManager planeManager;
    private bool isVisiible = true;
    private int numPlanesAddedOccurred = 0;


    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();

        if (planeManager is null)
        {
            Debug.LogError("ARPlaneManager component not found on the GameObject.");
        }

        togglePlaneAction.action.performed += OnTogglePlanesAction;
        planeManager.trackablesChanged.AddListener(OnPlanesChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTogglePlanesAction(InputAction.CallbackContext context)
    {
        isVisiible = !isVisiible;
        float fillAlpha = isVisiible ? 0.3f : 0f;
        float lineAlpha = isVisiible ? 1f : 0f;

        foreach (var plane in planeManager.trackables)
        {
            SetPlaneAlpha(plane, fillAlpha, lineAlpha);
        }
    }

    private void SetPlaneAlpha(ARPlane plane, float fillAlpha, float lineAlpha)
    {
        var meshRenderer = plane.GetComponent<MeshRenderer>();
        var lineRenderer = plane.GetComponent<LineRenderer>();

        if (meshRenderer != null)
        {
            Color color = meshRenderer.material.color;
            color.a = fillAlpha;
            meshRenderer.material.color = color;
        }

        if (lineRenderer != null)
        {
            Color startColor = lineRenderer.startColor;
            Color endColor = lineRenderer.endColor;

            startColor.a = lineAlpha;
            endColor.a = lineAlpha; 

            lineRenderer.startColor = startColor;
            lineRenderer.endColor = endColor;
        }
    }

    private void OnPlanesChanged(ARTrackablesChangedEventArgs<ARPlane> args)
    {
        if (args.added.Count > 0)
        {
            numPlanesAddedOccurred++;
            
            foreach (var plane in planeManager.trackables)
            {
                PrintPlaneLabel(plane);
            }

            Debug.Log("-> Numbers of planes: " + planeManager.trackables.count);
            Debug.Log("-> Num Planes Added Occurred: " + numPlanesAddedOccurred);
        }
    }

    private void PrintPlaneLabel(ARPlane plane)
    {
        string label = plane.classifications.ToString();
        string log = $"Plane ID: {plane.trackableId}: Label: {label}";
        Debug.Log(log);
    }

    void OnDestroy()
    {
        togglePlaneAction.action.performed -= OnTogglePlanesAction;
        planeManager.trackablesChanged.RemoveListener(OnPlanesChanged);
    }
}
