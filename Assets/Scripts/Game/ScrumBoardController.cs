using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrumBoardController : MonoBehaviour
{
    [SerializeField]
    private GameObject cartridgePrefab;
    [SerializeField]
    private GameObject playerHold;


    private void CreateCartridge()
    {
        GameObject cartridge = (GameObject) Instantiate(cartridgePrefab, playerHold.transform.position, playerHold.transform.rotation);
        cartridge.transform.parent = playerHold.transform.parent;
    }
}
