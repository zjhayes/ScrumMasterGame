using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class Floor : Selectable
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        
        if(CanSelect && TryRaycastToFloor(eventData.position, out Vector3 floorClickPosition))
        {
            // Send the agent to the hit point.
            SendCurrentCharacterTo(floorClickPosition);
        }
    }

    private bool TryRaycastToFloor(Vector3 clickPosition, out Vector3 floorPosition)
    {
        floorPosition = Vector3.zero;

        // Raycast from the mouse click position.
        Ray ray = Camera.main.ScreenPointToRay(clickPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit point is on the NavMesh.
            if (hit.collider.CompareTag(Tags.FLOOR) && NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
            {
                floorPosition = navHit.position;
                return true;
            }
        }

        return false; // NavMesh was not clicked.
    }

    private void SendCurrentCharacterTo(Vector3 position)
    {
        if (gameManager.Context.CurrentCharacter != null) // A character must be selected.
        {
            ICharacterController character = gameManager.Context.CurrentCharacter;
            character.Idle();
            character.Movement.GoTo(position);
        }
    }
}
