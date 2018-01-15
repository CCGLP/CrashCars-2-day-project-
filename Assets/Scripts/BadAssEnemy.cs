using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 
using DG.Tweening; 
using UnityStandardAssets.Vehicles.Car; 

public class BadAssEnemy : MonoBehaviour {
	[SerializeField]
	private GameObject explosion; 
	[SerializeField]
	private GameObject permanence; 

	private MainController mainController; 

	private NavMeshAgent agent; 
	private Transform target; 
	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			agent = this.GetComponent<NavMeshAgent> (); 
			agent.SetDestination (target.position); 
			mainController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<MainController> ();
		}
	}

	void Update(){
		if (target != null) 
			agent.SetDestination (target.position); 
	}

	public void Destroy(){
		Instantiate (explosion, this.transform.position, Quaternion.identity);
		Instantiate (permanence, this.transform.position, Quaternion.identity); 
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<MainController> ().OnCarDestroyed ();
		Destroy (this.gameObject); 

	}
	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Player" && !coll.gameObject.GetComponentInChildren<Rigidbody>().isKinematic) {
			Instantiate (explosion, coll.contacts [0].point, Quaternion.identity); 


			Camera cameraL = coll.gameObject.GetComponentInChildren<Camera> ();
			if (cameraL!= null)
				cameraL.transform.SetParent(null); 

			Time.timeScale = 0.1f; 
			DOVirtual.DelayedCall (0.1f, () => {
				Time.timeScale = 1; 

				//Camera.main.transform.position -= Camera.main.transform.forward * 3; 
			}, true); 
			cameraL.GetComponent<CameraController> ().ShakeCamera ();

			Destroy (coll.gameObject); 

			DOVirtual.DelayedCall (2.5f, () => {
				mainController.EndGame();
			}, true); 

		
			

		}
	}
}
