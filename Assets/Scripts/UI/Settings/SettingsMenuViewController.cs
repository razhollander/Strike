using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuViewController : SettingsBase
{
    public override void OnQuitClicked()
    {
        Application.Quit();
    }

    public override void OnResumeClicked()
    {
        
    }
}
