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
        
        menu.SetActive(true);
    }

    public void HideMenu(MenuController menu)
    {
        menu.SetActive(false);

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
}
