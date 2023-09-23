using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : GameBehaviour
{
    [SerializeField]
    List<MenuController> menus;

    public delegate void OnShowFirstMenu();
    public OnShowFirstMenu onShowFirstMenu;

    public delegate void OnHideLastMenu();
    public OnHideLastMenu onHideLastMenu;

    void Awake()
    {
        // Initiate hidden menus.
        foreach (MenuController menu in menus)
        {
            menu.onShow += ShowMenu;
            menu.onHide += HideMenu;
            menu.SetUp();
        }
    }

    void Start()
    {
        // Listen to player controls.
        gameManager.Controls.onEscape += OnEscape;
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

        // Check if all menus hidden.
        if(ActiveCount <= 0)
        {
            onHideLastMenu?.Invoke();
        }
    }

    public void ShowAll()
    {
        foreach(MenuController menu in menus)
        {
            ShowMenu(menu);
        }
    }

    public void HideAll()
    {
        foreach(MenuController menu in menus)
        {
            HideMenu(menu);
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
