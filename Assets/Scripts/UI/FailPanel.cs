using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void RestartButton()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

}
