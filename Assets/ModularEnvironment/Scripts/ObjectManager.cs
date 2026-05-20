using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private SystemManager systemManager;

    private Theme currentTheme;

    public ObjectMaterial objectMaterial;

    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        systemManager = FindAnyObjectByType<SystemManager>();
    }

    void Start()
    {
        currentTheme = systemManager.globalTheme;
        // This updates material depending on theme on start
        UpdateObjectMaterial(systemManager.globalTheme);

    }

    private void Update()
    {
        //A check to just make sure the theme is updated or not.
        if (currentTheme != systemManager.globalTheme)
        {
            UpdateObjectMaterial(systemManager.globalTheme);
            currentTheme = systemManager.globalTheme;
        }
    }

    private void UpdateObjectMaterial(Theme theme)
    {
        meshRenderer.material = objectMaterial.material[(int)theme];
    }
}
