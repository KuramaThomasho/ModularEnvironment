
using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.Runtime.CompilerServices;

public class SystemManager : MonoBehaviour
{
    public Theme globalTheme;
    public void ThemeSwitcher(int themeIndex)
    {
        if (themeIndex >= 0 && themeIndex < System.Enum.GetValues(typeof(Theme)).Length)
        {
            globalTheme = (Theme)themeIndex;
        }
        else
        {
            Debug.LogWarning("Invalid theme index: " + themeIndex);
        }
    }

    [SerializeField] private GameObject menuPanel;
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
        else
            return;
        

    }

}
//This is the enum for themes, make sure to add the right index for additional themes
public enum Theme
{
    Theme1,
    Theme2,
    Ignore
}

