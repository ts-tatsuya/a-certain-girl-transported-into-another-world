using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public Actor myStats;
	public StatusEffect myStatusEffect;
	public int collectiblesObtained;
	public Text collectiblesShow;

	public Image skillUI;
	public Sprite[] skillSprite;
	public int skillCurr;
	public float[] skillCost;
	[SerializeField] private HealthBarHandler healthBar;
	[SerializeField] private HealthBarHandler staminaBar;
	public bool Dead;
	bool regen;

	// Use this for initialization
	void Start () {

		GameObject.Find ("Canvas").GetComponent<DataLoader> ().SendData (gameObject.GetComponent<Player> ());

		healthBar.SetHealth (myStats.currHP, myStats.maxHP);
		staminaBar.SetHealth (myStats.currStamina, myStats.maxStamina);
	}
	
	// Update is called once per frame
	void Update () {
		if (collectiblesObtained != 0) {
			collectiblesShow.text = "x " + collectiblesObtained;
		}
		if (regen == false) {
			StartCoroutine (Regen (false, 0.5f));
		}

		healthBar.SetHealth (myStats.currHP, myStats.maxHP);
		staminaBar.SetHealth (myStats.currStamina, myStats.maxStamina);
		myStatusEffect.DurationCountUpdate ();

	}

	public void SwitchSkill(bool prev){
	
		if (prev == false) {
		
			if (skillCurr < (skillSprite.Length - 1)) {
				skillCurr++;
			} else {
			
				skillCurr = 0;
			}

		} else if (prev == true) {
		
			if (skillCurr > 0) {
				skillCurr--;
			} else {

				skillCurr = (skillSprite.Length - 1);
			}
		}


		skillUI.sprite = skillSprite [skillCurr];




	}


	public void RecieveDamage(float damageValue){


		myStats.currHP -= damageValue;

		healthBar.SetHealth (myStats.currHP, myStats.maxHP);

	}


	IEnumerator Regen(bool HP, float RegenSpeed){

		regen = true;
		yield return new WaitForSeconds (1);
		regen = false;
		if (HP && myStats.currHP < myStats.maxHP) {
		
			myStats.currHP = myStats.currHP + RegenSpeed;
		} else if (myStats.currStamina < myStats.maxStamina) {
		
			myStats.currStamina = myStats.currStamina + RegenSpeed;
		}

	}




}
