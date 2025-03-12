using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour {

	public Slider sliderObj;
	public Color low;
	public Color high;
	public Vector3 offset;
	public bool isNPCHealthBar;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isNPCHealthBar) {
			
			sliderObj.transform.position = Camera.main.WorldToScreenPoint (transform.parent.position + offset);

		}
	}

	public void SetHealth(float currHealth, float maxHealth){

		if (isNPCHealthBar) {
			//sliderObj.gameObject.SetActive (currHealth < maxHealth);
		}

		sliderObj.maxValue = maxHealth;
		sliderObj.value = currHealth;


		sliderObj.fillRect.GetComponent<Image> ().color = Color.Lerp (low, high, sliderObj.normalizedValue);

	}


}
