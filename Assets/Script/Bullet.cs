using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;
	public Rigidbody2D rb;
	public int bulletID;
	public float bulletDamage;



	// Use this for initialization
	void Start () {


		rb.velocity = transform.right * speed;

	}

	void Update(){

	
		if (GetComponent<SpriteRenderer> ().isVisible == false) {
		
			Destroy (gameObject);
		}




	}

	void OnCollisionEnter2D(Collision2D coll){



		switch (bulletID) {


		case 0:

			if (coll.gameObject.tag == "Enemy") {


				coll.gameObject.GetComponent<Enemy> ().RecieveDamage (bulletDamage);
				coll.gameObject.GetComponent<Animator> ().SetTrigger ("doHurt");
				coll.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (10, 2000));
				coll.gameObject.GetComponent<AudioSource> ().Play ();
			}else if (coll.gameObject.tag == "Boss") {


				coll.gameObject.GetComponent<Boss> ().RecieveDamage (bulletDamage);
				coll.gameObject.GetComponent<Animator> ().SetTrigger ("doHurt");
				coll.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (10, 1000));
				coll.gameObject.GetComponent<AudioSource> ().Play ();
			}


			break;
		case 1:

			if (coll.gameObject.tag == "Enemy") {
				coll.gameObject.GetComponent<Enemy> ().GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				coll.gameObject.GetComponent<Enemy> ().myStatusEffect.InflictStatusEffect (1, 5f);

			}else if (coll.gameObject.tag == "Boss") {
				coll.gameObject.GetComponent<Boss> ().GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				coll.gameObject.GetComponent<Boss> ().myStatusEffect.InflictStatusEffect (1, 3f);

			}

			break;


		}

		if (coll.gameObject.CompareTag ("Player") == false) {
			Destroy (gameObject);
		}
	}




}
