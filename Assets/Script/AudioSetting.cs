using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour {

	public AudioMixer MainMixer;


	void Start(){

		if (PlayerPrefs.HasKey ("BGMVol") && PlayerPrefs.HasKey ("SEVol") && PlayerPrefs.HasKey ("isMute")) {
			MainMixer.SetFloat ("BGMVol", Mathf.Log10 (PlayerPrefs.GetFloat ("BGMVol")) * 20);
			MainMixer.SetFloat ("SEVol", Mathf.Log10 (PlayerPrefs.GetFloat ("SEVol")) * 20);

			switch (PlayerPrefs.GetInt ("isMute")) {

			case 0:

				if (gameObject == GameObject.Find ("Mute")) {

					gameObject.GetComponent<Toggle> ().isOn = false;
				}
				MainMixer.SetFloat ("MasterVol", Mathf.Log10 (1f) * 20);
				break;
			case 1:

				if (gameObject == GameObject.Find ("Mute")) {

					gameObject.GetComponent<Toggle> ().isOn = true;
				}
				MainMixer.SetFloat ("MasterVol", Mathf.Log10 (0.0001f) * 20);
				break;


			}



			if (gameObject == GameObject.Find ("BGMSlider")) {

				gameObject.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("BGMVol");

			} else if (gameObject == GameObject.Find ("SESlider")) {
		
				gameObject.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("SEVol");

			}

			
		} else {
		
			PlayerPrefs.SetInt ("isMute", 0);
			PlayerPrefs.SetFloat ("BGMVol", 1f);
			PlayerPrefs.SetFloat ("SEVol", 1f);
			PlayerPrefs.Save ();

		}




	}


	public void VolumeLevelSet(float volLevel){

		if (gameObject == GameObject.Find ("MVSlider")) {

			//MainMixer.SetFloat ("MasterVolPar", Mathf.Log10 (volLevel) * 20);

		}else if (gameObject == GameObject.Find ("BGMSlider")) {

			MainMixer.SetFloat ("BGMVol", Mathf.Log10 (volLevel) * 20);
			PlayerPrefs.SetFloat ("BGMVol", volLevel);

		}else if (gameObject == GameObject.Find ("SESlider")) {

			MainMixer.SetFloat ("SEVol", Mathf.Log10 (volLevel) * 20);
			PlayerPrefs.SetFloat ("SEVol", volLevel);
		}

		PlayerPrefs.Save ();


	}
		

	public void Mute(Toggle isMute){



		if (isMute.isOn) {
			MainMixer.SetFloat ("MasterVol", Mathf.Log10 (0.0001f) * 20);
		} else {
		
			MainMixer.SetFloat ("MasterVol", Mathf.Log10 (1f) * 20);
		}

		switch (isMute.isOn) {

		case true:
			PlayerPrefs.SetInt ("isMute", 1);
			break;
		case false:
			PlayerPrefs.SetInt ("isMute", 0);
			break;


		}

		PlayerPrefs.Save ();


	}


}
