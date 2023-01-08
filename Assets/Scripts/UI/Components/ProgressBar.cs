using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    int maximum;
    [SerializeField]
    Image mask;

    int current;

    void UpdateCurrentFill()
    {
        float fillAmount = (float) current / (float) maximum;
        mask.fillAmount = fillAmount;
    }

    public int CurrentFill
    {
        set 
        { 
            current = value; 
            UpdateCurrentFill(); 
        }
    }
}
