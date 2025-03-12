using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBorder : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll){

		if (coll.gameObject.tag == "Player") {
		
			coll.gameObject.GetComponent<Player> ().RecieveDamage (coll.gameObject.GetComponent<Player> ().myStats.maxHP);
		}

		if (coll.gameObject.tag == "Enemy") {
		
			coll.gameObject.GetComponent<Enemy> ().RecieveDamage (coll.gameObject.GetComponent<Enemy> ().myStats.maxHP);
		}



	}





}
