using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    List<MenuController> menus;

    void Start()
    {
        // Initiate hidden menus.
        foreach(MenuController menu in menus)
        {
            menu.SetUp();
        }
    }

    public void ShowMenu(MenuController menu, CharacterController character)
    {
        menu.Show(character);
    }
}
