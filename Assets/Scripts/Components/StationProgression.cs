using UnityEngine;

public abstract class StationProgression : GameBehaviour
{
    [SerializeField]
    private Transform overheadLocation;

    private ProgressBar progressBar;

    protected virtual void Awake()
    {
        // Create Progress Bar for this station.
        progressBar = gameManager.UI.OverheadCanvas.CreateProgressBar();
        progressBar.transform.position = overheadLocation.position;

        HideProgressBar();
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
}
