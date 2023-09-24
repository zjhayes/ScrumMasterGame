using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
	private NavMeshAgent agent;

	public Events.GameEvent OnArrivedAtDestination;
	public Events.GameEvent OnDestinationChange;

	private void Awake()
    {
		agent = GetComponent<NavMeshAgent>();
    }

	private void FixedUpdate()
    {
		if(AtDestination())
        {
			// Character has arrived at destination.
			this.enabled = false; // Stop FixedUpdate.
			OnArrivedAtDestination?.Invoke();
        }
    }

	public void GoTo(Vector3 target)
	{
		this.enabled = true; // Start FixedUpdate.
		agent.enabled = true;
		agent.destination = target;
		OnDestinationChange?.Invoke();
	}

	public bool AtDestination()
    {
		if ((agent.enabled && !agent.pathPending) && // Agent is actively navigating...
			(agent.remainingDistance <= agent.stoppingDistance) && // Agent is close enough to destination...
			(!agent.hasPath || agent.velocity.sqrMagnitude == 0f)) // Agent has no calculated path or is stopped.
		{
			return true;
		}
		return false;
	}
}
