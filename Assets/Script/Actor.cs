using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Actor {

	[Tooltip("Actor's maximum HP")]public float maxHP;
	[SerializeField]private float _currHP;
	[Tooltip("Actor's maximum Stamina")]public float maxStamina;
	[SerializeField]private float _currStamina;
	[Tooltip("Actor's attack power value")]public float attackPower;







	/// <summary>
	/// Actor's HP
	/// </summary>
	/// <value>The curr H.</value>
	public float currHP{

		get{ return _currHP;}
		set{ _currHP = Mathf.Clamp (value, 0, maxHP);}

	}
	/// <summary>
	/// Actor's Stamina
	/// </summary>
	/// <value>The curr stamina.</value>
	public float currStamina{

		get{ return _currStamina;}
		set{ _currStamina = Mathf.Clamp (value, 0, maxStamina);}

	}
	/// <summary>
	/// Check if actor has enough stamina
	/// </summary>
	/// <returns><c>true</c>, if stamina usable was ised, <c>false</c> otherwise.</returns>
	/// <param name="staminaCost">Stamina cost.</param>
	public bool isStaminaUsable(float staminaCost){
	
		if (currStamina >= staminaCost) {
			
			return true;
		}else{
			
			return false;
		}

	}

	/// <summary>
	/// Uses the stamina.
	/// </summary>
	/// <param name="staminaCost">Stamina cost.</param>
	public void UseStamina(float staminaCost){
	
		currStamina = currStamina - staminaCost;
	}

	/// <summary>
	/// Recieves the damage.
	/// </summary>
	/// <param name="damage">Damage.</param>
	public void RecieveDamage(float damage){

		currHP = currHP - damage;

	}

	/// <summary>
	/// Check if actor HP is 0
	/// </summary>
	/// <returns><c>true</c>, if dead was ised, <c>false</c> otherwise.</returns>
	public bool isDead(){
	
		if (currHP <= 0) {
		
			return true;
		} else {
		
			return false;
		}
	}



}
