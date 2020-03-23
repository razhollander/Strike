using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsGameViewController : SettingsBase
{
    public override void OnQuitClicked()
    {
        GameManager.Instance.EndGame();
    }

    public override void OnResumeClicked()
    {
        GameManager.Instance.ResumeGame();
    }
}
