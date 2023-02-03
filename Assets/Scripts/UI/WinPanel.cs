using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void NextLevelButton()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
   
}
