using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
	NavMeshAgent agent;
	Vector3 groundNormal;
	bool moving = false; // TODO: Use State system.

	public delegate void OnArrivedAtDestination();
	public OnArrivedAtDestination onArrivedAtDestination;

	void Awake()
    {
		agent = GetComponent<NavMeshAgent>();
    }

	void FixedUpdate()
    {
		if(moving && AtDestination())
        {
			moving = false;
			onArrivedAtDestination?.Invoke();
        }
    }

	public void GoTo(Vector3 target)
	{
		Debug.Log(this.gameObject.name + " goes to " + target);
		agent.destination = target;
		moving = true;
	}

	public bool AtDestination()
    {
		if (!agent.pathPending)
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


	/*
	 * 
	 *     [SerializeField]
	private float stationaryTurnSpeed = 180;
    [SerializeField]
	private float movingTurnSpeed = 360;
    [SerializeField]
	private float groundCheckDistance = 0.1f;
	void FixedUpdate()
	{
		Move(controller.Direction, controller.Speed);
		
	}

	public void Move(Vector3 direction, float speed)
	{
		CheckGroundStatus();

		// Move.
		if (direction.magnitude > 1f) direction.Normalize();
		direction = Vector3.ProjectOnPlane(direction, groundNormal);

		transform.GetComponent<Rigidbody>().MovePosition(transform.position + direction * Time.deltaTime * speed);

		// Rotate.
		ApplyTurnRotation(direction);
	}

	void ApplyTurnRotation(Vector3 direction)
	{
		Vector3 turnDirection = transform.InverseTransformDirection(direction);
		float turnAmount = Mathf.Atan2(turnDirection.x, turnDirection.z);
		float forwardAmount = turnDirection.z;

		// This is in addition to root rotation in the animation.
		float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
		transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, Numeric.ZERO);
	}

	public void CheckGroundStatus()
	{
		RaycastHit hit;

		// 0.1f is a small offset to start the ray from inside the character
		// the transform position is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, groundCheckDistance))
		{
			// Grounded.
			groundNormal = hit.normal;
			//animation.ApplyRootMotion = true;
			//animation.OnGround(true);
		}
		else
		{
			// Falling.
			groundNormal = Vector3.up;
			//animation.ApplyRootMotion = false;
			//animation.OnGround(false);
		}
	}*/
}
