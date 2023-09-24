using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField]
    private Vector3 centerPosition;
    [SerializeField]
    private float xLength;
    [SerializeField]
    private float zLength;
    [SerializeField]
    private Color gizmoColor = Color.green;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Vector3 halfSize = new Vector3(xLength / 2, 0, zLength / 2);
        Gizmos.DrawWireCube(centerPosition, halfSize * 2);
    }

    public Vector3 GetRandomPointInsideBoundary()
    {
        float randomX = Random.Range(-xLength / 2, xLength / 2);
        float randomZ = Random.Range(-zLength / 2, zLength / 2);
        return centerPosition + new Vector3(randomX, 0, randomZ);
    }
}
