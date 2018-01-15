using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
public class MainController : MonoBehaviour {

	public enum GameMode
	{
		timeLimit, 
		policeDestroyer,
		defaultMode
	}

	[SerializeField]
	private AnimationCurve spawnMobsByTime; 
	[SerializeField]
	private GameObject goodEnemy; 

	[SerializeField]
	private GameObject badEnemy; 

	[SerializeField]
	private Text carsLeft; 

	[SerializeField]
	private Text timeLeft; 

	[SerializeField]
	private GameObject[] powerupPrefabs; 

	[SerializeField]
	private float timeToSpawnPowerup = 20f; 


	private float timerPowerup = 0; 

	[SerializeField]
	private float maxTime = 20;

	[SerializeField]
	private GameMode gameMode = GameMode.timeLimit;

	private float timer = 0; 
	private int cars; 
	private int carsDestroyed = 0; 

	[SerializeField]
	private Vector3 mapSize; 

	private Transform[] spawnPolices; 
	[SerializeField]
	private float timePolice = 10; 
	private float timerPolice = 0; 

	private float generalTimer = 0; 




	// Use this for initialization
	void Start () {
		if (StaticVariables.gameMode != GameMode.defaultMode) {
			gameMode = StaticVariables.gameMode; 
		} 
		if (gameMode== GameMode.policeDestroyer) {
			timeLeft.transform.parent.GetComponent<CanvasGroup> ().alpha = 0; 
		}
		if (StaticVariables.timeToPlay != -1) {
			maxTime = StaticVariables.timeToPlay;
		}
		cars = GameObject.FindGameObjectsWithTag ("Enemy").Length; 

		carsLeft.text = carsDestroyed.ToString ();

		spawnPolices = GameObject.Find ("Points").GetComponentsInChildren<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 
		timerPowerup += Time.deltaTime; 
		generalTimer += Time.deltaTime; 
		timerPolice += Time.deltaTime; 
		timeLeft.text = (Mathf.CeilToInt (maxTime - timer)).ToString ();


		if (timerPowerup > timeToSpawnPowerup) {
			timerPowerup = 0; 
			Vector3 pos = GetRandomPositionInMap ();
			pos.y = 1;
			Instantiate (powerupPrefabs [Random.Range (0, powerupPrefabs.Length)], pos, Quaternion.identity);
		}
		if (gameMode == GameMode.policeDestroyer) {
			maxTime = spawnMobsByTime.Evaluate (generalTimer); 

			if (timerPolice > timePolice) {
				timerPolice = 0; 
				Instantiate (badEnemy, spawnPolices [Random.Range (0, spawnPolices.Length)].position, badEnemy.transform.rotation);
			}
		}

		if (timer > maxTime) {
			timer = 0; 
			switch (gameMode) {
			case GameMode.policeDestroyer:
				
				InstantiateCar (goodEnemy); 

				cars++; 



				break; 

			case GameMode.timeLimit:
				EndGame ();
				break; 
			}
		}

	}

	public void InstantiateCar(GameObject car){
		Instantiate (car, this.GetRandomPositionInMap (), car.transform.rotation); 
	}

	public void OnCarDestroyed(){
		cars--; 
		carsDestroyed++;
		carsLeft.text =  carsDestroyed.ToString ();

		if (cars <= 0 && gameMode == GameMode.timeLimit)
			EndGame ();
		else if (cars <= 0) {
			InstantiateCar (goodEnemy);
			cars++; 


		}
	}


	public void EndGame(){
		PlayerPrefs.SetInt ("cars", carsDestroyed);  
		SceneManager.LoadScene (2);

	}



	public Vector3 GetRandomPositionInMap(){
		return new Vector3 (Random.Range(this.transform.position.x - mapSize.x * 0.5f, this.transform.position.x + mapSize.x * 0.5f), 0,
			Random.Range(this.transform.position.z - mapSize.z * 0.5f, this.transform.position.z + mapSize.z * 0.5f )); 
	}

	#if UNITY_EDITOR
	void OnDrawGizmos(){
		Gizmos.DrawWireCube (this.transform.position, mapSize); 
	}

	#endif
}
