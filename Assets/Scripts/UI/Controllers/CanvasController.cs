using System.Collections.Generic;
using UnityEngine;

public class CanvasController : GameBehaviour
{
    [SerializeField]
    List<MenuController> menus;

    public Events.UIEvent OnShowFirstMenu;
    public Events.UIEvent OnHideLastMenu;

    private void Awake()
    {
        // Initiate hidden menus.
        foreach (MenuController menu in menus)
        {
            menu.OnShow += ShowMenu;
            menu.OnHide += HideMenu;
            menu.SetUp();
        }
    }

    private void Start()
    {
        // Listen to player controls.
        gameManager.Controls.OnEscape += OnEscape;
    }

    public void ShowMenu(MenuController menu)
    {
        // Check if menus not already open.
        if(ActiveCount <= 0)
        {
            OnShowFirstMenu?.Invoke();
        }
        menu.SetActive(true);
    }

    public void HideMenu(MenuController menu)
    {
        menu.SetActive(false);

        // Check if all menus hidden.
        if(ActiveCount <= 0)
        {
            OnHideLastMenu?.Invoke();
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
