using UnityEngine;
using TMPro;

public class UIClock : GameBehaviour
{
    [SerializeField]
    TextMeshProUGUI timeText;

    void Update()
    {
        timeText.text = gameManager.Sprint.Clock.BeautifulTime;
    }
}
