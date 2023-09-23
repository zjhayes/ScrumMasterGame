using UnityEngine;
using UnityEngine.Events;

public class OnEnableEvent : MonoBehaviour
{
    public UnityEvent OnEnablement;

    private void OnEnable()
    {
        OnEnablement?.Invoke();
    }
}
