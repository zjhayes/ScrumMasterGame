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
        float time = SprintManager.Instance.Clock.CurrentTime;
        string clockText = BeautifyTime(time);
        Debug.Log(clockText);
        timeText.text = clockText;
    }

    string BeautifyTime(float time)
    {
        int parsedTime = (int) time;
        int minutes = parsedTime / 60;
        int seconds = parsedTime % 60;
        Debug.Log(minutes);
        Debug.Log(seconds);
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
