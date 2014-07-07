using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2(3, 5);
	public bool standing = false;
	public float jetSpeed = 15f;
	public float airSpeedMultiplier = .3f;

	private PlayerController controller;
	private Animator animator;

	void Start()
	{
		controller = GetComponent<PlayerController>();
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		float forceX = 0f;
		float forceY = 0f;

		float absVelX = Mathf.Abs(rigidbody2D.velocity.x);
		float absVelY = Mathf.Abs(rigidbody2D.velocity.y);

		if (absVelY < 0.2f)
		{
			standing = true;
		}
		else
		{
			standing = false;
		}

		if (controller.moving.x != 0)
		{
			if (absVelX < maxVelocity.x)
			{
				float finalSpeed = speed * controller.moving.x;
				forceX = standing ? finalSpeed : (finalSpeed * airSpeedMultiplier);

				transform.localScale = new Vector3(forceX > 0 ? 1 : -1, 1, 1);
			}
			animator.SetInteger("AnimState", 1);
		}
		else
		{
			animator.SetInteger("AnimState", 0);
		}

		if (controller.moving.y > 0)
		{
			if (absVelY < maxVelocity.y)
			{
				forceY = jetSpeed;
			}

			animator.SetInteger("AnimState", 2);
		}
		else if (absVelY > 0)
		{
			animator.SetInteger("AnimState", 3);

		}

		rigidbody2D.AddForce(new Vector2(forceX, forceY));
	}
}
