﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
	
		if (coll.gameObject.tag == "Player") {
		
			coll.gameObject.GetComponent<Player> ().collectiblesObtained++;
			Destroy (gameObject);
		}

	}


}
