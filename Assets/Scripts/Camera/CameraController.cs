using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera followCamera;
    [SerializeField]
    Animator animator; // Controls state driven camera.

    Quaternion defaultCameraRotation;
    Transform defaultFocus;

    const string OVERWORLD_STATE = "OverworldCamera";
    const string BOARD_STATE = "BoardCamera";
    const string FOLLOW_STATE = "FollowCamera";

    void Awake()
    {
        // Set defaults to initial camera state.
        defaultCameraRotation = followCamera.transform.rotation;
        defaultFocus = followCamera.LookAt;
    }

    public void SwitchToOverworldCamera()
    {
        animator.Play(OVERWORLD_STATE);
    }

    public void SwitchToBoardCamera()
    {
        animator.Play(BOARD_STATE);
    }

    public void SwitchToFollowCamera(Transform target)
    {
        followCamera.LookAt = target;
        animator.Play(FOLLOW_STATE);
    }
}
