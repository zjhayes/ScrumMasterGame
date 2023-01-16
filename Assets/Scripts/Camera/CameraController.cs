using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Animator animator; // Controls state driven camera.

    const string OVERWORLD_STATE = "OverworldCamera";
    const string BOARD_STATE = "BoardCamera";

    public void SwitchToOverworldCamera()
    {
        animator.Play(OVERWORLD_STATE);
    }

    public void SwitchToBoardCamera()
    {
        animator.Play(BOARD_STATE);
    }
}
