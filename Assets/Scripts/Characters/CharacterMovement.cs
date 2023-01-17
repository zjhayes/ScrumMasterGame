using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
	NavMeshAgent agent;
	Vector3 groundNormal;

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
		agent.destination = target;
		this.enabled = true;
	}

	public bool AtDestination()
    {
		if (agent.enabled && !agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
				{
					return true;
				}
			}
		}
		return false;
	}
}
