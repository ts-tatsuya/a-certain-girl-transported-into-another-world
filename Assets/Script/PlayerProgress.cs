[System.Serializable]
public class PlayerProgress {

	private int collectiblesObtained;


	/*public PlayerProgress (string playerName){

		this.playerName = playerName;
		stageNumber = 0;
	
	}*/

	public void SetCollectibleObtained(int collectible){
	
		collectiblesObtained = collectible;
	
	}


	public int GetCollectibleObtained(){

		return collectiblesObtained;

	}






}
