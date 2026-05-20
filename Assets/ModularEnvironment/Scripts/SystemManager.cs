using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public Theme globalTheme;
    public void ThemeSwitcher(int themeIndex)
    {
        switch (themeIndex)
        {
            case 0:
                globalTheme = Theme.PostApocalype;
                break;

            case 1:
                globalTheme = Theme.CyberPunk;
                break;

        };
    }
}

//This is the enum for themes, make sure to add the right index for additional themes
public enum Theme
{
    PostApocalype,
    CyberPunk
}