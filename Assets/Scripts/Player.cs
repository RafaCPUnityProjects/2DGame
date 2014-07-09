﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2(3, 5);
	public bool standing = false;
	public float jetSpeed = 15f;
	public float airSpeedMultiplier = .3f;
	public AudioClip leftFootSound;
	public AudioClip rightFootSound;
	public AudioClip thudSound;
	public AudioClip rocketSound;

	private PlayerController controller;
	private Animator animator;

	void Start()
	{
		controller = GetComponent<PlayerController>();
		animator = GetComponent<Animator>();
	}

	void PlayLeftFootSound()
	{
		if (leftFootSound)
		{
			AudioSource.PlayClipAtPoint(leftFootSound, transform.position);
		}
	}

	void PlayRightFootSound()
	{
		if (rightFootSound)
		{
			AudioSource.PlayClipAtPoint(rightFootSound, transform.position);
		}
	}

	void PlayRocketSound()
	{
		if (!rocketSound || GameObject.Find("RocketSound"))
		{
			return;
		}

		GameObject go = new GameObject("RocketSound");
		AudioSource aSrc = go.AddComponent<AudioSource>();
		aSrc.clip = rocketSound;
		aSrc.volume = 0.7f;
		aSrc.Play();

		Destroy(go, rocketSound.length);
	}

	void OnCollisionEnter2D(Collision2D target)
	{
		if (!standing)
		{
			float absVelX = Mathf.Abs(rigidbody2D.velocity.x);
			float absVelY = Mathf.Abs(rigidbody2D.velocity.y);

			if (absVelX <= .1f || absVelY <= .1f)
			{
				if (thudSound)
				{
					AudioSource.PlayClipAtPoint(thudSound, transform.position);
				}
			}
		}
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
			PlayRocketSound();
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
