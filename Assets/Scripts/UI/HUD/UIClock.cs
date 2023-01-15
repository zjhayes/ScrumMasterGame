using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIClock : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timeText;

    void Start()
    {
        SprintManager.Instance.onBeginSprint += Show;
        SprintManager.Instance.onBeginRetrospective += Hide;
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
        timeText.text = SprintManager.Instance.Clock.BeautifulTime;
    }
}
