using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBar : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI statusText;

    public void UpdateStatusText(string status)
    {
        statusText.text = status;
    }
}
