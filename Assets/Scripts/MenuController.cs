using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuController : MonoBehaviour {

	public void OnTimeClick(){
		StaticVariables.gameMode = MainController.GameMode.timeLimit;
		StaticVariables.timeToPlay = 60; 
		SceneManager.LoadScene (1); 

	}

	public void OnPoliceClick(){
		StaticVariables.gameMode = MainController.GameMode.policeDestroyer;
		StaticVariables.timeToPlay = 15; 
		SceneManager.LoadScene (1); 
	}
}
