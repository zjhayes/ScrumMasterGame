using UnityEngine;

/* Persist gameObject between scenes, and removes duplicates. */
public class DoNotDestroyOnLoad : Singleton<DoNotDestroyOnLoad>
{
    // This gameObject is kept alive by the Singleton pattern.
    private void OnDestroy()
    {
        //Debug.LogWarningFormat("{0} was destroyed.", gameObject.name);
    }
}

// TODO: Determine if this is needed.