
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public Theme globalTheme;
    public void ThemeSwitcher(int themeIndex)
    {
        switch (themeIndex)
        {
            case 0:
                globalTheme = Theme.Theme1;
                break;
            case 1:
                globalTheme = Theme.Theme2;
                break;
            default:
                globalTheme = Theme.Ignore;
                break;
        }
    }

    [SerializeField] private GameObject menuPanel;
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
        

    }
}

//This is the enum for themes, make sure to add the right index for additional themes
public enum Theme
{
    Theme1,
    Theme2,
    Ignore
}

