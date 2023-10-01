using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
	private NavMeshAgent agent;
	private float baseSpeed;

	public Events.GameEvent OnArrivedAtDestination;
	public Events.GameEvent OnDestinationChange;

	private void Awake()
    {
		agent = GetComponent<NavMeshAgent>();
		baseSpeed = agent.speed;
    }

	private void FixedUpdate()
    {
		if(IsNavigationReady() && AtDestination())
        {
			// Character has arrived at destination.
			this.enabled = false; // Stop FixedUpdate.
			agent.enabled = false;
			OnArrivedAtDestination?.Invoke();
        }
    }

	public void GoTo(Vector3 target)
	{
		GoTo(target, baseSpeed);
	}

	public void GoTo(Vector3 target, float speed)
    {
		this.enabled = true; // Start FixedUpdate.
		transform.parent = null; // Unsocket character.
		agent.enabled = true;
		agent.speed = speed;
		agent.SetDestination(target);
		OnDestinationChange?.Invoke();
	}

	public bool AtDestination()
    {
		return (WithinStoppingDistance() && IsStopped());
	}

	public bool IsNavigationReady()
    {
		// Agent is actively navigating...
		return agent.enabled && !agent.pathPending;
	}

	public bool WithinStoppingDistance()
    {
		// Agent is close enough to destination...
		return agent.remainingDistance <= agent.stoppingDistance;
	}

	public bool IsStopped()
    {
		// Agent has no calculated path or is stopped.
		return !agent.hasPath || agent.velocity.sqrMagnitude == 0f;
	}

	public void GoToBoundary(Boundary boundary, float speed)
    {
		Vector3 randomPoint = boundary.GetRandomPointInsideBoundary();

		GoTo(randomPoint, speed);
    }

	public float BaseSpeed
    {
		get { return baseSpeed; }
    }

}
