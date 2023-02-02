using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : InGamePanel
{
    private void OnEnable()
    {

        EventManager.OpenWinPanel.AddListener(ShowPanel);
        EventManager.OnLevelFinish.AddListener(HidePanel);
    }



    private void OnDisable()
    {

        EventManager.OpenWinPanel.RemoveListener(ShowPanel);
        EventManager.OnLevelFinish.RemoveListener(HidePanel);
    }

   
}
