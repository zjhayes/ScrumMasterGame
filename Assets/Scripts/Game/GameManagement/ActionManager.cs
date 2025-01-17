using System;
using System.Collections;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public Coroutine StartDelayedAction(float delay, Action action)
    {
        return StartCoroutine(DelayedActionCoroutine(delay, action));
    }

    private IEnumerator DelayedActionCoroutine(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}