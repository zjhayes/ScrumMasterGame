using UnityEngine;

public abstract class StationProgression : MonoBehaviour
{
    [SerializeField]
    protected ProgressBar progressBar;

    protected virtual void Awake()
    {
        progressBar.Maximum = Numeric.ONE_HUNDRED_PERCENT; // Same as Task completeness maximum.
        HideProgressBar();
    }

    protected void ShowProgressBar()
    {
        progressBar.Show();
    }

    protected void HideProgressBar()
    {
        progressBar.Hide();
    }
}
