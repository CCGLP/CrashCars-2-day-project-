using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class AutoControlPowerup : MonoBehaviour {

	void OnTriggerEnter(Collider coll){

		if (coll.gameObject.tag == "Player") {
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Enemy");


			coll.gameObject.GetComponentInParent<CarUserControl>().ActiveAutoControl(gameObjects[Random.Range(0,gameObjects.Length)].transform);

			

			Destroy (this.gameObject); 

		}
	}
}
