using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float speed;
	private float speedDefault;
	public float jumpForce;
	private bool isPaused;
	private bool isFaceForward;

	//public Transform attackPoint;
	//public float attackRange;
	//public LayerMask attackTargetLayer;
	//private Collider2D[] attackTargetHit;
	private bool attackCooldown;

	//public bool showAttackRange;

	[SerializeField] private LayerMask lmLand;
	[SerializeField] private Animator player_Animator;
	[SerializeField] private Player playerData;

	[SerializeField] private GameObject pauseMenu;

	[SerializeField]private GameObject[] bullet;
	[SerializeField]private Transform playerShootingPoint;

	[SerializeField]private GameObject GameOverScreen;





	// Use this for initialization
	void Start () {

		Time.timeScale = 1;
		attackCooldown = false;
		isFaceForward = true;
		speedDefault = speed;
	}
	
	// Update is called once per frame
	void Update () {

		AnimUpdate ();


		//Debug.Log (isGrounded ());
		if (playerData.myStats.isDead () == false) {
	
			if (Input.GetKey (KeyCode.D) /*RIGHT*/) {
		
				Move (true);
			}

			if (Input.GetKey (KeyCode.A) /*LEFT*/) {

				Move (false);

			}

			if (Input.GetKeyDown (KeyCode.W) /*JUMP*/) {

				if (isGrounded ()) {
			
					Jump ();
				}
			}

			if (Input.GetKey (KeyCode.S) /*DROP*/) {


			}

			if (Input.GetKey (KeyCode.LeftShift)) {
		
				Run (true);
			} else {
		
				Run (false);
			}

			//ACTION

			if (Input.GetKeyDown (KeyCode.Space) && attackCooldown == false /*ATTACK*/) {

				//attackTargetHit = Physics2D.OverlapCircleAll (attackPoint.position, attackRange, attackTargetLayer);
				StartCoroutine (Attack ());

			}

			if (Input.GetKeyDown (KeyCode.G) /*SPECIALS*/) {

				if(playerData.myStats.isStaminaUsable(playerData.skillCost[playerData.skillCurr])){
					
					StartCoroutine (UseSpecials (playerData.skillCurr));


				}

			}

			if (Input.GetKeyDown (KeyCode.E) /*SWITCH SPECIALS*/) {

				playerData.SwitchSkill (false);
			} else if (Input.GetKeyDown (KeyCode.Q) /*SWITCH SPECIALS*/) {
			
				playerData.SwitchSkill (true);
			}

			//OTHERS

			if (Input.GetKeyDown (KeyCode.Escape) /*Pause*/) {
			
				PauseGame ();
			}

		} else {
		
			if (playerData.Dead == false) {
				StartCoroutine (Dead ());
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


		gameObject.transform.Translate (new Vector2 (speed, 0) * Time.deltaTime);


	}



	void Run(bool isActive){
	
		switch (isActive) {

		case true:
			if (speed == speedDefault) {
				speed = speed * 2f;
			}
			break;
		case false:
			speed = speedDefault;
			break;

		}

	}


	void Jump(){

		gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, jumpForce));
		gameObject.GetComponent<Animator> ().SetTrigger ("doJump");

	}

	void Drop(){
	

	}

	IEnumerator Attack(){
	


		/*foreach (Collider2D target in attackTargetHit) {

			target.gameObject.GetComponent<Enemy> ().RecieveDamage (playerData.myStats.attackPower);
		
		}*/
		if (gameObject.GetComponentInChildren<PlayerSensorHandler> ().target != null) {

			gameObject.GetComponentInChildren<PlayerSensorHandler> ().target.GetComponent<Animator> ().SetTrigger ("doHurt");

			if (gameObject.GetComponentInChildren<PlayerSensorHandler> ().target.tag == "Enemy") {
				gameObject.GetComponentInChildren<PlayerSensorHandler> ().target.GetComponent<Enemy> ().RecieveDamage (playerData.myStats.attackPower);
			}else if (gameObject.GetComponentInChildren<PlayerSensorHandler> ().target.tag == "Boss") {
				gameObject.GetComponentInChildren<PlayerSensorHandler> ().target.GetComponent<Boss> ().RecieveDamage (playerData.myStats.attackPower);
			}

		}

		gameObject.GetComponentInChildren<AudioSource> ().Play ();

		attackCooldown = true;
		yield return new WaitForSeconds (0.8f);
		attackCooldown = false;

	}


	IEnumerator UseSpecials(int currSkill){

	

		if (attackCooldown == false) {
			gameObject.GetComponent<Animator> ().SetTrigger ("doAttack");
			yield return new WaitForSeconds (0.01f);
			playerData.myStats.UseStamina (playerData.skillCost [currSkill]);
			Instantiate (bullet[currSkill], playerShootingPoint.position, playerShootingPoint.rotation);
			attackCooldown = true;
			yield return new WaitForSeconds (0.7f);
			attackCooldown = false;
		}

	}



	IEnumerator Dead(){

		if (playerData.Dead == false) {
			player_Animator.SetTrigger ("doDead");
			playerData.Dead = true;
			yield return new WaitForSeconds (1);
			GameOverScreen.SetActive (true);
			Time.timeScale = 0;
		}


	}



	public void PauseGame(){
		
		if (isPaused == false) {
		
			pauseMenu.SetActive (true);
			Time.timeScale = 0;
			isPaused = true;
		} else if (isPaused == true) {
		
			pauseMenu.SetActive (false);
			Time.timeScale = 1;
			isPaused = false;
		}


	}


	private void AnimUpdate(){
	
		// MOVE

		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A)) {
		
			player_Animator.SetBool ("isWalking", true);
		} else if (Input.GetKey (KeyCode.LeftShift) && player_Animator.GetBool("isWalking")) {

			player_Animator.SetBool ("isWalking", false);
			player_Animator.SetBool ("isRunning", true);
		} else {
		
			player_Animator.SetBool ("isWalking", false);
			player_Animator.SetBool ("isRunning", false);
		}

		// JUMP

		if (gameObject.GetComponent<Rigidbody2D> ().velocity.y < 0 && isGrounded() == false) {
		
			player_Animator.SetBool ("isDropping", true);
		} else {
			
			player_Animator.SetBool ("isDropping", false);
		}


		//ATTACK

		if (Input.GetKeyDown(KeyCode.Space) && attackCooldown == false) {
		
			player_Animator.SetTrigger ("doAttack");
		}





	}




	private bool isGrounded(){

		RaycastHit2D rchit = Physics2D.BoxCast (transform.GetComponent<BoxCollider2D>().bounds.center, transform.GetComponent<BoxCollider2D>().bounds.size, 0f, Vector2.down, 0.1f, lmLand);

		Debug.Log (rchit.collider);

		return rchit.collider != null;



		/*Debug.Log (gameObject.GetComponent<BoxCollider2D> ().IsTouching (GameObject.FindGameObjectWithTag ("land").GetComponent<BoxCollider2D>()));
		return gameObject.GetComponent<BoxCollider2D> ().IsTouching (GameObject.FindGameObjectWithTag ("land").GetComponent<BoxCollider2D>());
		*/
	}



	/*void OnDrawGizmosSelected(){

		if (showAttackRange) {
			if (attackPoint == null) {
		
				return;
		
			}

			Gizmos.DrawWireSphere (attackPoint.position, attackRange);
		}
	}*/








}
