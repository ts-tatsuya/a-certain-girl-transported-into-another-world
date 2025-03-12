using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryItem : MonoBehaviour {

	public float recoveryPoint;
	public bool isRecoverBoth;
	public bool isRecoverHP;
	public bool isRecoverMP;


	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.CompareTag ("Player")) {
		
			if (isRecoverBoth) {
			
				HealPlayerHP (coll.gameObject.GetComponent<Player> ());
				if (recoveryPoint != 0) {
				
					HealPlayerMP (coll.gameObject.GetComponent<Player> ());
				}


			} else {
			
				if (isRecoverHP) {
				
					HealPlayerHP(coll.gameObject.GetComponent<Player> ());
				}

				if (isRecoverMP) {
				
					HealPlayerMP(coll.gameObject.GetComponent<Player> ());
				}

			}

			Destroy (gameObject);



		}

	}

	void HealPlayerHP(Player player){

		while (recoveryPoint != 0 && player.myStats.currHP < player.myStats.maxHP) {
		
			player.myStats.currHP++;
			recoveryPoint--;

		} 

	}

	void HealPlayerMP(Player player){
	
		while (recoveryPoint != 0 && player.myStats.currStamina < player.myStats.maxStamina) {

			player.myStats.currStamina++;
			recoveryPoint--;

		} 

	}



}
