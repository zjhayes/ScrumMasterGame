using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField]
    private CharacterController character;
    [SerializeField]
    private Animator animator;

    const string SPEED_BLEND_VARIABLE = "speedPercent";
    const float SMOOTH_TIME = .1f;

    private void Update()
    {
        float speedPercent = character.Movement.Agent.velocity.magnitude / character.Movement.BaseSpeed;
        animator.SetFloat(SPEED_BLEND_VARIABLE, speedPercent, SMOOTH_TIME, Time.deltaTime);
    }


}
