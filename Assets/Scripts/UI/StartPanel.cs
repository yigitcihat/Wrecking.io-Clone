using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : InGamePanel
{

    private void Awake()
    {
        ShowPanel();
    }
    
    public void StartGame()
    {
        Debug.Log("sdasdasd");
        EventManager.OnLevelStart.Invoke();
        HidePanel();
    }
}
