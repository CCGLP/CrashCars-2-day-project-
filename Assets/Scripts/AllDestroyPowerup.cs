using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDestroyPowerup : MonoBehaviour {

	void OnTriggerEnter(Collider coll){
		
		if (coll.gameObject.tag == "Player") {
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Enemy");

			for (int i = 0; i < gameObjects.Length; i++) {
				if (gameObjects [i].GetComponentInChildren<Renderer> ().isVisible) {
					gameObjects [i].GetComponent<BasicEnemy> ().Destroy ();
				}

			}

			gameObjects = GameObject.FindGameObjectsWithTag ("Police"); 
			for (int i = 0; i < gameObjects.Length; i++) {
				if (gameObjects [i].GetComponentInChildren<Renderer> ().isVisible) {
					gameObjects [i].GetComponent<BadAssEnemy> ().Destroy ();
				}

			}

			Destroy (this.gameObject); 
	
		}
	}


}
