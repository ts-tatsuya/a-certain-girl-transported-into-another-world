using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour {

	public GameObject NextDialogue;


	public bool routePoint;
	public int route2Req;
	public GameObject route1;
	public GameObject route2;

	void Awake(){
	
		Time.timeScale = 0;
	}

	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.Mouse0)) {
		
			if (NextDialogue != null) {
				NextDialogue.SetActive (true);
			} else if(routePoint){

				switch (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().collectiblesObtained >= route2Req) {

				case true:
					route2.SetActive (true);
					break;
				case false:
					route1.SetActive (true);
					break;


				}

			} else {
			
				Time.timeScale = 1;
			}
			Destroy (gameObject);
		}



	}



}
