using UnityEngine;
using UnityEngine.AI;

public class Awareness : MonoBehaviour
{
    [SerializeField]
    private float distance = 3.0f;
    [SerializeField]
    private Vector3 sightOffset;

    private GameObject currentTarget;

    public delegate void OnAwarenessChanged(GameObject target);
    public OnAwarenessChanged onAwarenessChanged;

    public GameObject Target
    {
        get { return currentTarget; }
    }

    public bool HasTarget()
    {
        return currentTarget != null;
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 position = transform.position;
        Vector3 positionOffset = transform.TransformDirection(sightOffset);
        position.x += positionOffset.x;
        position.y += positionOffset.y;
        position.z += positionOffset.z;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(position, forward, out hit, distance))
        {
            GameObject currentHit = hit.transform.gameObject;
            UpdateTarget(currentHit);
            Debug.DrawRay(position, forward * distance, Color.red);
        }
        else
        {
            ClearTarget();
            Debug.DrawRay(position, forward * distance, Color.white);
        }
    }

    private void UpdateTarget(GameObject newTarget)
    {
        if (!GameObject.ReferenceEquals(newTarget, currentTarget))
        {
            currentTarget = newTarget;
            InvokeOnAwarenessUpdated();
        }
    }

    public void ClearTarget()
    {
        if (currentTarget != null)
        {
            currentTarget = null;
            InvokeOnAwarenessUpdated();
        }
    }

    private void InvokeOnAwarenessUpdated()
    {
        onAwarenessChanged?.Invoke(currentTarget);
    }

    void OnDisable()
    {
        ClearTarget();
    }
}
