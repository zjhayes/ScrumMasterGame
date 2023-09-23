using UnityEngine;

public class OverheadCanvasController : CanvasController
{
    [SerializeField]
    private Transform stationProgressBarParent;
    [SerializeField]
    private GameObject stationProgressionBar;


    public ProgressBar CreateStationProgressionBar()
    {
        GameObject progressBar = Instantiate(stationProgressionBar, stationProgressBarParent);
        return progressBar.GetComponent<ProgressBar>();
    }
}
