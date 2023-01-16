using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    List<MenuController> menus;

    void Awake()
    {
        // Initiate hidden menus.
        foreach(MenuController menu in menus)
        {
            menu.SetUp();
            menu.onShow += ShowMenu;
            menu.onHide += HideMenu;
        }
    }

    void Start()
    {
        PlayerControls.Instance.onEscape += OnEscape;
    }

    public void ShowMenu(MenuController menu)
    {
        if(menu.Tinted)
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
