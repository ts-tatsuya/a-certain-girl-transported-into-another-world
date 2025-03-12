using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandler : MonoBehaviour {

	[Tooltip("Object to be spawned")]public GameObject spawn;
	[Tooltip("Spawn interval in seconds")]public float interval;
	private bool inCooldown;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (inCooldown == false && gameObject.GetComponent<SpriteRenderer> ().isVisible == false) {
		
			StartCoroutine (SpawnObj (spawn));
			inCooldown = true;
		}


		
	}

	IEnumerator SpawnObj(GameObject obj){


		Instantiate (obj, transform.position, transform.rotation);
		yield return new WaitForSeconds (interval);
		inCooldown = false;
	}





}
