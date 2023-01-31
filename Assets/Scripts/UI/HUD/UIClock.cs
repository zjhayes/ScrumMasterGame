using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIClock : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timeText;

    void Start()
    {
        GameManager.Instance.Sprint.onBeginSprint += Show;
        GameManager.Instance.Sprint.onBeginRetrospective += Hide;
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
        timeText.text = GameManager.Instance.Sprint.Clock.BeautifulTime;
    }
}
