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
        ShowTint(true);
        menu.SetActive(true);
    }

    public void HideMenu(MenuController menu)
    {
        menu.SetActive(false);

        if(ActiveCount == 0)
        {
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
            foreach(MenuController menuController in menus)
            {
                if(menuController.IsShowing) { count++; }
            }
            return count;
        }
    }
}
