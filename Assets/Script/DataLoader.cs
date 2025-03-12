using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataLoader : MonoBehaviour {

	private PlayerProgress playerData;

	private void Awake(){
	
		//MakePlayerData ();
	}

	void Start(){
	

		LoadPlayer();

		//playerData.UnlockedStage ();

		Debug.Log (playerData.GetCollectibleObtained ());

		SavePlayer ();
	}

	public void StoreData(int collectibledata){
	
		playerData.SetCollectibleObtained (collectibledata);
		SavePlayer ();


	}

	public void SendData(Player player){
	

		LoadPlayer ();
		player.collectiblesObtained = playerData.GetCollectibleObtained ();


	}


	private void MakePlayerData(){

		playerData = new PlayerProgress();
		playerData.SetCollectibleObtained (0);
	}



	private void SavePlayer(){
	
		string path = Application.persistentDataPath + "/PlayerData.tut";

		FileStream file = new FileStream (path, FileMode.Create);




		BinaryFormatter bf = new BinaryFormatter ();
		bf.Serialize (file, playerData);


		file.Close ();

	}


	private bool CheckFile(){
	
		return File.Exists (Application.persistentDataPath + "/PlayerData.tut");

	}





	private void LoadPlayer(){
	


		if (CheckFile ()) {
		
			string path = Application.persistentDataPath + "/PlayerData.tut";
			FileStream file = new FileStream (path, FileMode.Open);


			BinaryFormatter bf = new BinaryFormatter ();

			playerData = bf.Deserialize (file) as PlayerProgress;

			file.Close ();
		
		} else {
		
			Debug.Log ("file is GONE");

			MakePlayerData ();
			SavePlayer ();
		}







	
	}








}
