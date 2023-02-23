using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIClock : GameBehaviour
{
    [SerializeField]
    TextMeshProUGUI timeText;

    void Start()
    {
        gameManager.Sprint.onBeginSprint += Show;
        gameManager.Sprint.onBeginRetrospective += Hide;
        Hide();
    }

    void Show()
    {
        this.enabled = true;
    }

    void Hide()
    {
        this.enabled = false;
    }

    void Update()
    {
        timeText.text = gameManager.Sprint.Clock.BeautifulTime;
    }
}
