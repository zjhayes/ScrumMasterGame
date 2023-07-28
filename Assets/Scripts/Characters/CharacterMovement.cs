using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
	NavMeshAgent agent;

	public delegate void OnArrivedAtDestination();
	public OnArrivedAtDestination onArrivedAtDestination;

	void Awake()
    {
		agent = GetComponent<NavMeshAgent>();
    }

	void FixedUpdate()
    {
		if(AtDestination())
        {
			this.enabled = false;
			onArrivedAtDestination?.Invoke();
        }
    }

	public void GoTo(Vector3 target)
	{
		this.enabled = true;
		agent.enabled = true;
		agent.destination = target;
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

	public bool CanMoveTo(Vector3 targetPosition)
	{
		return NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, new NavMeshPath());
	}
}
