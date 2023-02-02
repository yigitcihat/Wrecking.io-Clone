using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class InGamePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    protected CanvasGroup CanvasGroup { get { return (canvasGroup == null) ? canvasGroup = GetComponent<CanvasGroup>() : canvasGroup; } }

    public string PanelID;


    private List<string> panelList
    {
        get { return PanelList.PanelIDs; }
    }

    private void Awake()
    {
        PanelList.InGamePanels[PanelID] = this;
    }


    public virtual void ShowPanel()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    public virtual void HidePanel()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }

    public virtual void TogglePanel()
    {
        if (CanvasGroup.alpha == 0)
            ShowPanel();
        else HidePanel();
    }

}
public static class PanelList
{
    public static string WinPanel = "WinPanel";
    public static string FailPanel = "FailPanel";

    public static Dictionary<string, InGamePanel> InGamePanels = new Dictionary<string, InGamePanel>();

    private static string[] panelIDs = new string[]
    {
            "None",
            WinPanel,
            FailPanel,
           
    };
    public static List<string> PanelIDs { get { return panelIDs.ToList(); } }
}
