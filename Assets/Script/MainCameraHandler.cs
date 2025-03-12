using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraHandler : MonoBehaviour {

	public GameObject player;
	public Vector3 offset;
	public float speedMovement;

	public bool showCamStopRange;

	// Use this for initialization
	void Start () {

		gameObject.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, gameObject.transform.position.z);
		
	}
	
	// Update is called once per frame
	void Update () {



		if (player.transform.position.x > gameObject.transform.position.x) {
		
			StartCoroutine (MoveCam(player.transform.position));
		}

		if (player.transform.position.x < (gameObject.transform.position.x - offset.x)) {

			StartCoroutine (MoveCam(player.transform.position));


		}




		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, player.transform.position.y, gameObject.transform.position.z);

	}



	private IEnumerator MoveCam(Vector3 playerPos){
	
		if (playerPos.x > gameObject.transform.position.x) {

			while (gameObject.transform.position.x <= player.transform.position.x) {
				gameObject.transform.position = new Vector3 ((gameObject.transform.position.x + speedMovement), gameObject.transform.position.y, gameObject.transform.position.z);
				yield return new WaitForSeconds (0.0001f);
			}

		}


		if (playerPos.x < (gameObject.transform.position.x - offset.x)) {
			while ((gameObject.transform.position.x - offset.x) >= player.transform.position.x) {
				gameObject.transform.position = new Vector3 ((gameObject.transform.position.x - speedMovement), gameObject.transform.position.y, gameObject.transform.position.z);
				yield return new WaitForSeconds (0.0001f);
			}
		}
	}





	void OnDrawGizmosSelected(){

		if (showCamStopRange) {
			
			Gizmos.DrawWireSphere (gameObject.transform.position, offset.x);
		}

	}



}
