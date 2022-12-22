using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

//[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI headerField;
    [SerializeField]
    private TextMeshProUGUI contentField;
    [SerializeField]
    private LayoutElement layoutElement;
    [SerializeField]
    private int characterWrapLimit;
    [SerializeField]
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        /*
        if(Application.isEditor)
        {
            UpdateTextWrap();
        }*/

        SetPositionToMouse();
    }

    public void SetText(string content, string header = "")
    {
        if(string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        UpdateTextWrap();
    }

    private void UpdateTextWrap()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        // Update tooltip text wrapping.
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }

    private void SetPositionToMouse()
    {
        Vector2 position = Mouse.current.position.ReadValue();

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = position;
    }
}
