using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailPanel : InGamePanel
{
    private void OnEnable()
    {

        EventManager.OpenFailPanel.AddListener(ShowPanel);
        EventManager.OnLevelFinish.AddListener(HidePanel);
    }



    private void OnDisable()
    {

        EventManager.OpenFailPanel.RemoveListener(ShowPanel);
        EventManager.OnLevelFinish.RemoveListener(HidePanel);
    }

   
}
