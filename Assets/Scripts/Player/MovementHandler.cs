using UnityEngine;

public class MovementHandler
{
	float stationaryTurnSpeed = 180;
	float movingTurnSpeed = 360;
	float groundCheckDistance = 0.1f;

	Transform transform;
	float forwardAmount;
	float turnAmount;
	Vector3 groundNormal;


	public MovementHandler(Transform transform)
    {
		this.transform = transform;
	}

	public void Move(Vector3 direction, float speed)
	{
		CheckGroundStatus();

		// Move.
		if (direction.magnitude > 1f) direction.Normalize();
		direction = Vector3.ProjectOnPlane(direction, groundNormal);

		transform.GetComponent<Rigidbody>().MovePosition(transform.position + direction * Time.deltaTime * speed);

		// Rotate.
		direction = transform.InverseTransformDirection(direction);
		turnAmount = Mathf.Atan2(direction.x, direction.z);
		forwardAmount = direction.z;

		ApplyTurnRotation();
	}

	void ApplyTurnRotation()
	{
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
	}

}