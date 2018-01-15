using UnityEngine;
using System.Collections;
using DG.Tweening; 
public class BasicEnemy : MonoBehaviour {


	private MainController mainController; 

	[SerializeField]
	private float timeToChange = 4f; 





	[SerializeField]
	private GameObject explosion; 
	[SerializeField]
	private GameObject permanence; 

	private Renderer rend; 
	private float timer = 0; 
	private UnityEngine.AI.NavMeshAgent navAgent; 
	// Use this for initialization
	void Start () {
		navAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent> ();
		mainController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<MainController> ();
		navAgent.SetDestination(mainController.GetRandomPositionInMap()); 
		rend = this.GetComponentInChildren<Renderer> (); 


	}

	public void Destroy(){
		Instantiate (explosion, this.transform.position, Quaternion.identity);
		Instantiate (permanence, this.transform.position, Quaternion.identity); 
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<MainController> ().OnCarDestroyed ();
		Destroy (this.gameObject); 
	
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 


		if (timer > timeToChange) {
			timer = 0; 
			navAgent.SetDestination (mainController.GetRandomPositionInMap ()); 
		}
		else if (Vector3.Distance (navAgent.destination, this.transform.position) < 1) {
			timer = 0; 
			navAgent.SetDestination(mainController.GetRandomPositionInMap()); 
		}
	}


	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Player") {
			Instantiate (explosion, this.transform.position, Quaternion.identity);
			Instantiate (permanence, this.transform.position, Quaternion.identity); 
			Camera.main.GetComponent<CameraController>().ShakeCamera();
			Time.timeScale = 0.1f; 
			DOVirtual.DelayedCall (0.1f, () => {
				Time.timeScale = 1; 
			}, true); 
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<MainController> ().OnCarDestroyed ();
			Destroy (this.gameObject); 


		}
	}
}
