using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public Actor myStats;
	public StatusEffect myStatusEffect;



	[SerializeField] private HealthBarHandler healthBar;
	[SerializeField] private Animator enemyAnimator;
	[SerializeField]private bool isFaceForward;



	public float speed;

	public bool enemyAI_Patrol;
	public bool enemyAI_Chase;
	public bool enemyAI_Aggresive;
	public GameObject enemySight;
	public GameObject enemyMeleeRange;
	private bool attackCooldown;
	private int prevAttack;



	public float enemyPatrolRange;
	private Vector3 enemyPatrolStart;
	public bool enemyReversePatrol;

	// Use this for initialization
	void Start () {

		attackCooldown = false;
		enemyPatrolStart = gameObject.transform.position;
		healthBar.SetHealth (myStats.currHP, myStats.maxHP);
		isFaceForward = true;


	}
	
	// Update is called once per frame
	void Update () {


		if (myStatusEffect.status_1_Charmed == false && attackCooldown == false) {

			if (enemyAI_Chase == true && enemySight.GetComponent<NPCSensorHandler> ().playerOnSight == true) {


				Chase ();

			}

			if (enemyAI_Aggresive == true && enemyMeleeRange.GetComponent<NPCSensorHandler> ().playerOnSight == true) {

				if (attackCooldown == false) {
					StartCoroutine (Attack ());
				}
			}


			if (enemyAI_Chase) {

				if (enemyAI_Patrol && enemySight.GetComponent<NPCSensorHandler> ().playerOnSight == false) {

					Patrol ();
				}
			} else {

				if (enemyAI_Patrol && attackCooldown == false) {

					Patrol ();
				}
			}




		}

		AnimUpdate ();
		myStatusEffect.DurationCountUpdate ();



	}

	public void RecieveDamage(float damageValue){

		//gameObject.GetComponent<AudioSource> ().Play ();
		myStats.currHP -= damageValue;

		healthBar.SetHealth (myStats.currHP, myStats.maxHP);

		if (myStats.isDead ()) {


			Destroy (gameObject);

		}

	}



	private void AnimUpdate(){

		if (myStatusEffect.status_1_Charmed == false && attackCooldown == false) {

			//enemyAnimator.SetBool ("isEase", false);

			if ((enemyAI_Chase == true &&
				enemySight.GetComponent<NPCSensorHandler> ().playerOnSight == true) ||
				enemyAI_Patrol == true) {

				enemyAnimator.SetBool ("isMoving", true);

			} else {

				enemyAnimator.SetBool ("isMoving", false);
			}

		} else {

			//enemyAnimator.SetBool ("isEase", true);
			enemyAnimator.SetBool ("isMoving", false);
		}
			




	}





	public void Patrol(){

		if (isFaceForward) {

			Move (true);

		} else if (!isFaceForward) {

			Move (false);
		}





		Debug.Log (enemyPatrolStart.x - enemyPatrolRange);

		if (enemyReversePatrol == false) {

			if (gameObject.transform.position.x <= (enemyPatrolStart.x - enemyPatrolRange)) {

				Move (false);


			} else if (gameObject.transform.position.x >= enemyPatrolStart.x) {

				Move (true);

			}

		} else if (enemyReversePatrol == true) {

			if (gameObject.transform.position.x >= (enemyPatrolStart.x + enemyPatrolRange)) {

				Move (true);


			} else if (gameObject.transform.position.x <= enemyPatrolStart.x) {

				Move (false);

			}
		}





	}
		


	public void Chase(){

		if (enemySight.GetComponent<NPCSensorHandler> ().target != null) {

			switch(enemySight.GetComponent<NPCSensorHandler> ().target.transform.position.x < gameObject.transform.position.x){

			case true:
				Move (true);
				break;
			case false:
				Move (false);
				break;

			}


		}


	}
















	void Move(bool Forward){

		switch(Forward){
		case true:
			if (isFaceForward == false) {

				gameObject.transform.Rotate (new Vector2 (0, 180));
				isFaceForward = true;
			}
			break;
		case false:
			if (isFaceForward == true) {

				gameObject.transform.Rotate (new Vector2 (0, 180));
				isFaceForward = false;
			}
			break;
		}


		gameObject.transform.Translate (new Vector2 (-speed, 0) * Time.deltaTime);


	}







	IEnumerator Attack(){




			if (enemyMeleeRange.GetComponent<NPCSensorHandler> ().target != null && enemyMeleeRange.GetComponent<NPCSensorHandler> ().target.CompareTag ("Player")) {
				
				attackCooldown = true;
				enemyAnimator.SetTrigger ("doAttack1");
				enemyMeleeRange.GetComponent<AudioSource> ().PlayDelayed (0.01f);

				

				yield return new WaitForSeconds (0.3f);

				if (enemyMeleeRange.GetComponent<NPCSensorHandler> ().target != null && enemyMeleeRange.GetComponent<NPCSensorHandler> ().target.CompareTag ("Player")) {

					if (enemyAnimator.GetCurrentAnimatorStateInfo (0).IsName ("isHurt") == false) {
						enemyMeleeRange.GetComponent<NPCSensorHandler> ().target.GetComponent<Player> ().RecieveDamage (myStats.attackPower/2);
					}
				}

				yield return new WaitForSeconds (0.1f);


				enemyMeleeRange.GetComponent<AudioSource> ().Stop ();
				//attackCooldown = false;

			}





			if (enemyMeleeRange.GetComponent<NPCSensorHandler> ().target != null && enemyMeleeRange.GetComponent<NPCSensorHandler> ().target.CompareTag ("Player")) {

				enemyAnimator.SetTrigger ("doAttack2");
				enemyMeleeRange.GetComponent<AudioSource> ().PlayDelayed (0.01f);

				//attackCooldown = true;

				yield return new WaitForSeconds (0.4f);

				if (enemyMeleeRange.GetComponent<NPCSensorHandler> ().target != null && enemyMeleeRange.GetComponent<NPCSensorHandler> ().target.CompareTag ("Player")) {

					if (enemyAnimator.GetCurrentAnimatorStateInfo (0).IsName ("isHurt") == false) {
						enemyMeleeRange.GetComponent<NPCSensorHandler> ().target.GetComponent<Player> ().RecieveDamage (myStats.attackPower);
					}
				}

				yield return new WaitForSeconds (0.2f);


				enemyMeleeRange.GetComponent<AudioSource> ().Stop ();
				//attackCooldown = false;

			}


	


			if (enemyMeleeRange.GetComponent<NPCSensorHandler> ().target != null && enemyMeleeRange.GetComponent<NPCSensorHandler> ().target.CompareTag ("Player")) {

				enemyAnimator.SetTrigger ("doAttack3");
				enemyMeleeRange.GetComponent<AudioSource> ().PlayDelayed (0.01f);

				//attackCooldown = true;

				yield return new WaitForSeconds (0.5f);

				if (enemyMeleeRange.GetComponent<NPCSensorHandler> ().target != null && enemyMeleeRange.GetComponent<NPCSensorHandler> ().target.CompareTag ("Player")) {

					if (enemyAnimator.GetCurrentAnimatorStateInfo (0).IsName ("isHurt") == false) {
						enemyMeleeRange.GetComponent<NPCSensorHandler> ().target.GetComponent<Player> ().RecieveDamage (myStats.attackPower*2);
					}
				}

				yield return new WaitForSeconds (0.3f);


				enemyMeleeRange.GetComponent<AudioSource> ().Stop ();
				

			}


		yield return new WaitForSeconds (0.5f);
		attackCooldown = false;


	}



}
