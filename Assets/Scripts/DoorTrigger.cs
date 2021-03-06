﻿using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

	public Door door;
	public bool ignoreTrigger;

	void OnTriggerEnter2D(Collider2D target)
	{
		if (ignoreTrigger)
			return;

		if (target.gameObject.tag == "Player")
		{
			door.Open();
		}
	}
	
	void OnTriggerExit2D(Collider2D target)
	{
		if (ignoreTrigger)
			return;

		if (target.gameObject.tag == "Player")
		{
			door.Close();
		}
	}

	internal void Toggle(bool value)
	{
		if (value)
		{
			door.Open();
		}
		else
		{
			door.Close();
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = ignoreTrigger ? Color.gray : Color.green;

		BoxCollider2D bc2d = GetComponent<BoxCollider2D>();
		Vector3 bc2dPos = bc2d.transform.position;
		Vector2 newPos = new Vector2(bc2dPos.x + bc2d.center.x, bc2dPos.y + bc2d.center.y);

		Gizmos.DrawWireCube(newPos, new Vector2(bc2d.size.x, bc2d.size.y));
	}
}
