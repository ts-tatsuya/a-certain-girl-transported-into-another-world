using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour {

	public AudioSource audio_Source;
	public AudioClip audio_Clip;
	public float audio_delay = 0;
	public GameObject dialogue;
	public GameObject Boss;


	public bool event_Dialogue;
	public bool event_AudioPlay;
	public bool event_AudioChange;
	public bool event_AudioStop;
	public bool eventcond_Boss;



	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {


		if (eventcond_Boss && Boss == null) {
		
			RunEvent ();

		}




	}


	void OnTriggerEnter2D(Collider2D coll){
	
		if(coll.gameObject.tag == "Player"){

			if (!eventcond_Boss) {
				RunEvent ();
			}
		}
			



	}


	public void RunEvent (){

		if (event_AudioChange) {
		
			audio_Source.clip = audio_Clip;
		}


		if (event_AudioPlay) {

			audio_Source.PlayDelayed (audio_delay);

		} else if (event_AudioStop) {

			audio_Source.Stop ();
		}

		if (event_Dialogue) {

			dialogue.SetActive (true);
		}

		Destroy (gameObject);

	}









}
