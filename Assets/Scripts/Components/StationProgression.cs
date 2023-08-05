using UnityEngine;

public abstract class StationProgression : GameBehaviour
{
    [SerializeField]
    private Transform overheadLocation;

    protected ProgressBar progressBar;

    protected virtual void Awake()
    {
        // Create Progress Bar for this station.
        progressBar = gameManager.UI.OverheadCanvas.CreateStationProgressionBar();
        progressBar.Maximum = Numeric.ONE_HUNDRED_PERCENT; // Same as Task completeness maximum.
        HideProgressBar();
    }

    private void Start()
    {
        UpdatePosition();
    }

    protected void ShowProgressBar()
    {
        progressBar.Show();
        this.enabled = true;
    }

    protected void HideProgressBar()
    {
        progressBar.Hide();
        this.enabled = false;
    }

    private void UpdatePosition()
    {
        progressBar.transform.position = Camera.main.WorldToScreenPoint(overheadLocation.position);
    }
}
