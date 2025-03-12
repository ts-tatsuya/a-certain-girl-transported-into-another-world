using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSensorHandler : MonoBehaviour {

	public bool playerOnSight;
	public GameObject target;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
	
		if (coll.gameObject.CompareTag ("Player")) {
		
			playerOnSight = true;
			target = coll.gameObject;		}

	}

	void OnTriggerExit2D(Collider2D coll){

		if (coll.gameObject.CompareTag ("Player")) {

			playerOnSight = false;
			target = null;
		}

	}




}
