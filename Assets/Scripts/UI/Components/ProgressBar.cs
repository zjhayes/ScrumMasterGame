using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private float maximum = 1;
    [SerializeField]
    private Image mask;

    private float current = 0;

    public float CurrentFill
    {
        set 
        { 
            current = value; 
            UpdateCurrentFill(); 
        }
    }

    public float Maximum
    {
        get { return maximum; }
        set { maximum = value; UpdateCurrentFill(); }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateCurrentFill()
    {
        float fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
    }
}
