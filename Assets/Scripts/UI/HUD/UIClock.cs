using UnityEngine;
using TMPro;

public class UIClock : GameBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;

    private void Update()
    {
        timeText.text = gameManager.Sprint.Clock.BeautifulTime;
    }
}
