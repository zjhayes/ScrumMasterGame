using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    List<MenuController> menus;

    public delegate void OnShowFirstMenu();
    public OnShowFirstMenu onShowFirstMenu;

    public delegate void OnHideLastMenu();
    public OnHideLastMenu onHideLastMenu;

    void Start()
    {
        // Initiate hidden menus.
        foreach (MenuController menu in menus)
        {
            menu.SetUp();
            menu.onShow += ShowMenu;
            menu.onHide += HideMenu;
        }

        PlayerControls.Instance.onEscape += OnEscape;
    }

    public void ShowMenu(MenuController menu)
    {
        // Check if menus not already open.
        if(ActiveCount <= 0)
        {
            onShowFirstMenu?.Invoke();
        }

        // Show back tint if necessary. TODO: Tint may no longer be needed.
        if (menu.Tinted)
        {
            ShowTint(true);
        }

        menu.SetActive(true);
    }

    public void HideMenu(MenuController menu)
    {
        menu.SetActive(false);
        if(!ActiveTinted)
        {
            // Hide tint when no menus require it.
            ShowTint(false);
        }
        // Check if all menus hiden.
        if(ActiveCount <= 0)
        {
            onHideLastMenu?.Invoke();
        }
    }

    public void OnEscape()
    {
        foreach(MenuController menu in menus)
        {
            menu.Escape();
        }
    }

    public void ShowTint(bool show)
    {
        gameObject.GetComponent<Image>().enabled = show;
    }

    public int ActiveCount
    {
        get
        {
            int count = 0;
            foreach(MenuController menu in menus)
            {
                if(menu.IsShowing) { count++; }
            }
            return count;
        }
    }

    // True if one or more active menus requires tint.
    public bool ActiveTinted
    {
        get
        {
            foreach(MenuController menu in menus)
            {
                if(menu.IsShowing && menu.Tinted) { return true; }
            }
            return false;
        }
    }
}
