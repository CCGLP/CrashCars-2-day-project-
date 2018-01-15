using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class CameraController : MonoBehaviour {

	private Vector3 previousPosition; 

	void Awake(){
		previousPosition = this.transform.localPosition; 
	}

	public void ShakeCamera(){





		this.transform.DOShakePosition (1, 3, 8, 75, false).OnComplete (() => {
			this.transform.DOLocalMove(previousPosition,0.3f); 
		});

	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
