using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorHandler : MonoBehaviour {

	public bool enemyOnSight;
	public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.CompareTag ("Enemy") || coll.gameObject.CompareTag ("Boss") ) {

			enemyOnSight = true;
			target = coll.gameObject;		
		} else {
		
			enemyOnSight = false;
		}

	}

	void OnTriggerExit2D(Collider2D coll){

		if (coll.gameObject.CompareTag ("Enemy") || coll.gameObject.CompareTag ("Boss") ) {

			enemyOnSight = false;
			target = null;
		}

	}
}
