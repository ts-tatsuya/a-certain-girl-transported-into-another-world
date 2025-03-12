using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneHandler : MonoBehaviour {

	public string sceneName;
	public GameObject loadingScreen;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



		
	}


	public void GoToNewScene(){

		StartCoroutine (SceneLoading ());
		//Time.timeScale = 1;
	}

	public void ExitGameProgram(){

		Application.Quit ();

	}

	IEnumerator SceneLoading(){
		AsyncOperation sceneLoader = SceneManager.LoadSceneAsync (sceneName);
		if (sceneName != SceneManager.GetActiveScene ().name && sceneName != "MainMenu") {

			sceneLoader.allowSceneActivation = false;

			while (!sceneLoader.isDone) {
		
				//loadingScreen.loadingProgress = Mathf.Clamp01 (sceneLoader.progress / .9f);


				for (int i = 0; i < (Mathf.Clamp01 (sceneLoader.progress + 0.1f) * 100); i++) {
			
					Debug.Log (sceneLoader.progress);
					loadingScreen.GetComponent<Text> ().text = i + " %";
					yield return new WaitForSecondsRealtime (0.01f);

					if (i >= 98) {

						if (SceneManager.GetActiveScene ().name == "MainMenu") {

							Debug.Log ("reset progress");
							GameObject.Find ("Canvas").GetComponent<DataLoader> ().StoreData (0);
						} else {
						
							GameObject.Find ("Canvas").GetComponent<DataLoader> ().StoreData (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().collectiblesObtained);
						}

						sceneLoader.allowSceneActivation = true;
					}
				}



				//yield return null;
		
			}

		}


		
			




	}

}
