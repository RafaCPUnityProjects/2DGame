﻿using UnityEngine;
using System.Collections;

public class Meter : MonoBehaviour {

	public float air = 10f;
	public float maxAir = 10f;
	public float airBurnRate = 1f;
	public Texture2D bgTexture;
	public Texture2D airBarTexture;
	public int iconWidth = 32;
	public Vector2 airOffset = new Vector2(10, 10);

	private Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<Player>();
	}

	void OnGUI()
	{
		float percent = Mathf.Clamp01(air / maxAir);

		if (!player)
			percent = 0;

		DrawMeter(airOffset.x, airOffset.y, airBarTexture, bgTexture, percent);
	}

	void DrawMeter(float x, float y, Texture2D texture, Texture2D background, float percent)
	{
		float bgW = background.width;
		float bgH = background.height;

		GUI.DrawTexture(new Rect(x, y, bgW, bgH), background);
		
		float nW = ((bgW-iconWidth)*percent)+iconWidth;
		GUI.BeginGroup(new Rect(x, y, nW, bgH));
		GUI.DrawTexture(new Rect(0, 0, bgW, bgH), texture);
		GUI.EndGroup();
	}

	// Update is called once per frame
	void Update () {
		if (!player)
		{
			return;
		}

		if (air > 0)
		{
			air -= Time.deltaTime * airBurnRate;
		}
		else
		{
			Explode script = player.GetComponent<Explode>();
			script.OnExplode();
		}
	}
}
