using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCubes : MonoBehaviour {
	[SerializeField]
	private GameObject spawn; 
	[SerializeField]
	private int numberOfCubes = 40; 

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numberOfCubes; i++) {
			Instantiate (spawn, new Vector3 (Random.Range (this.transform.position.x - 1, this.transform.position.x +1), 3, 
				Random.Range (this.transform.position.z -1, this.transform.position.z + 1)),Quaternion.identity); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
