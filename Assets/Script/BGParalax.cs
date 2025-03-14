﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGParalax : MonoBehaviour {

	private float length;
	private float startpos;
	public GameObject cam;
	public float parallaxEffect;


	// Use this for initialization
	void Start () {

		startpos = transform.position.x;
		length = GetComponent<SpriteRenderer> ().bounds.size.x;

		
	}
	
	// Update is called once per frame
	void Update () {
		float temp = (cam.transform.position.x * (1 - parallaxEffect));
		float dist = (cam.transform.position.x * parallaxEffect);

		transform.position = new Vector3 (startpos + dist, transform.position.y, transform.position.z);

		if (temp > startpos + length) {
		
			startpos = startpos + length;
		} else if (temp < startpos - length) {
		
			startpos = startpos - length;
		}


	}
}
