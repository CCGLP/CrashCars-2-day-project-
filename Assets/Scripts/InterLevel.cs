using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 
public class InterLevel : MonoBehaviour {
	[SerializeField]
	private Text carsText; 


	void Start(){
		carsText.text = "You destroyed: " + PlayerPrefs.GetInt ("cars", -1) + " cars" ; 
	}


	public void OnRestartClick(){
		SceneManager.LoadScene (1); 
	}
}
