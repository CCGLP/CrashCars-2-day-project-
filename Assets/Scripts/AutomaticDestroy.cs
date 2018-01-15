using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDestroy : MonoBehaviour {

	[SerializeField]
	private float timeToDestroy = 5; 

	private float timer = 0; 

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 

		if (timer > timeToDestroy) {
			Destroy (this.gameObject); 
		}
	}
}
