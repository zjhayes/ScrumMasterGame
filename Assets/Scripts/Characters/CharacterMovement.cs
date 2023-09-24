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
		if(AtDestination())
        {
			// Character has arrived at destination.
			this.enabled = false; // Stop FixedUpdate.
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
		agent.enabled = true;
		agent.speed = speed;
		agent.destination = target;
		OnDestinationChange?.Invoke();
	}

	public bool AtDestination()
    {
		if (IsNavigationReady() && WithinStoppingDistance() && IsStopped())
		{
			return true;
		}
		return false;
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

	public void WalkToRandomSpot(float distance, float speed)
	{
		// Get a random point within the radius.
		Vector3 randomPoint = transform.position + Random.insideUnitSphere * distance;
		
		NavMeshHit hit;
		if (NavMesh.SamplePosition(randomPoint, out hit, distance, 1))
		{
			// Go to the nearest valid point on the NavMesh to the random point.
			GoTo(hit.position, speed);
		}
	}

	public float BaseSpeed
    {
		get { return baseSpeed; }
    }
}
