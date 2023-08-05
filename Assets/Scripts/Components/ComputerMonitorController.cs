using UnityEngine;

public class ComputerMonitorController : MonoBehaviour
{
    [SerializeField]
    private TaskComputer computer;
    [SerializeField]
    private Renderer screenRenderer;
    [SerializeField]
    private Material wallpaperScreenMaterial;
    [SerializeField]
    private Material codingScreenMaterial;

    private void Awake()
    {
        computer.onRun += ShowProgrammingIDE;
        computer.onSleep += ShowWallpaper;
    }

    public void ShowWallpaper()
    {
        screenRenderer.material = wallpaperScreenMaterial;
    }

    public void ShowProgrammingIDE()
    {
        screenRenderer.material = codingScreenMaterial;
    }
}
