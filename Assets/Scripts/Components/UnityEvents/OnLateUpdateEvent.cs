using UnityEngine;
using UnityEngine.Events;

public class OnLateUpdateEvent : MonoBehaviour
{
    public UnityEvent OnLateUpdate;

    private void LateUpdate()
    {
        OnLateUpdate?.Invoke();
    }
}
