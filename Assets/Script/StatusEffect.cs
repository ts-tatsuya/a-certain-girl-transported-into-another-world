using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StatusEffect {

	public float[] statusDuration = new float[2];
	public GameObject[] statusAnim = new GameObject[2];
	public bool status_1_Charmed;
	public bool status_2_Poisoned;



	/// <summary>
	/// Inflicts the status effect.
	/// </summary>
	/// <param name="statusEffectID">Status effect I.</param>
	/// <param name="duration">Duration.</param>

	public void InflictStatusEffect(int statusEffectID, float duration){

		switch (statusEffectID) {

		case 1:
			status_1_Charmed = true;
			statusDuration [0] = duration;
			statusAnim [0].SetActive (true);
			break;
		case 2:
			status_2_Poisoned = true;
			statusDuration [1] = duration;
			statusAnim [1].SetActive (true);
			break;
		}
		
	}

	/// <summary>
	/// Update duration of all status effect.
	/// (Always use this on the top of Update() function)
	/// </summary>
	public void DurationCountUpdate(){
		
		for (int i = 0; i < statusDuration.Length; i++) {
		
			if (statusDuration [i] > 0) {
				statusDuration [i] = statusDuration [i] - 0.016f;
			} else {
			
				switch (i) {

				case 0:
					status_1_Charmed = false;
					if (statusAnim [0] != null) {
						statusAnim [0].SetActive (false);
					}
					break;
				case 1:
					status_2_Poisoned = false;
					if (statusAnim [1] != null) {
						statusAnim [1].SetActive (false);
					}
					break;

				}
			}
		}

	}



}
