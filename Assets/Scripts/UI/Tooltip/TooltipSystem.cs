using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : Singleton<TooltipSystem>
{

    [SerializeField]
    private Tooltip tooltip;
    [SerializeField]
    //private float delay = 2.0f;

    //private DelayedAction showAfterDelay; // TODO: Refactor this before use. Replace delay system and change singleton to game behaviour.

    public static void Show(string content, string header = "")
    {
        if(!Instance.tooltip.gameObject.activeSelf)
        {
            CancelDelay();
            Instance.tooltip.SetText(content, header);
            //Instance.showAfterDelay = new DelayedAction(Instance.ShowTooltip, Instance.delay);
            //ActionManager.Instance.Add(Instance.showAfterDelay);
        }
    }

    private void ShowTooltip()
    {
        Instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        CancelDelay();
        Instance.tooltip.gameObject.SetActive(false);
    }

    private static void CancelDelay()
    {
        //Instance.showAfterDelay?.Cancel();
    }
}
