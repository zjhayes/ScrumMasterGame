using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform overworldCameraTransform;
    [SerializeField]
    private Animator cameraStateAnimator; // Controls state driven camera.

    public const string OVERWORLD_STATE = "OverworldCamera";
    public const string BOARD_STATE = "BoardCamera";

    public void SwitchToOverworldCamera()
    {
        cameraStateAnimator.Play(OVERWORLD_STATE);
    }

    public void SwitchToBoardCamera()
    {
        cameraStateAnimator.Play(BOARD_STATE);
    }

    public Camera MainCamera
    {
        get { return mainCamera; }
    }

    public Transform OverworldCameraTransform
    {
        get { return overworldCameraTransform; }
    }
}
