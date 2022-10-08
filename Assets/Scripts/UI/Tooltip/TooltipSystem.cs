using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : Singleton<TooltipSystem>
{

    [SerializeField]
    private Tooltip tooltip;
    [SerializeField]
    private float delay = 2.0f;

    private DelayedAction showAfterDelay;

    public static void Show(string content, string header = "")
    {
        if(!Instance.tooltip.gameObject.activeSelf)
        {
            CancelDelay();
            Instance.tooltip.SetText(content, header);
            Instance.showAfterDelay = new DelayedAction(Instance.ShowTooltip, Instance.delay);
            ActionManager.Instance.Add(Instance.showAfterDelay);
        }
    }

    private void ShowTooltip()
    {
        Debug.Log("Called SHOW");
        Instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        CancelDelay();
        Instance.tooltip.gameObject.SetActive(false);
    }

    private static void CancelDelay()
    {
        Instance.showAfterDelay?.Cancel();
    }
}
