using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.AI; 
namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
		public bool autoControl = false; 
		private NavMeshAgent agent; 
		private Transform target; 
		private Rigidbody rb; 



        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
			agent = this.GetComponent<NavMeshAgent> ();
			rb = this.GetComponentInChildren<Rigidbody> ();
			agent.enabled = false; 
        }

		public void ActiveAutoControl(Transform target){
			agent.enabled = true; 
			agent.speed = 200;
			agent.acceleration = 100; 
			agent.angularSpeed = 2160;

			autoControl = true; 
			this.target = target; 
			rb.isKinematic = true; 
		}



        private void FixedUpdate()
        {
			if (!autoControl) {
				float h = 0;
				float v = CrossPlatformInputManager.GetAxis ("Vertical");

				v = 2; 

				#if UNITY_ANDROID
				if (Input.touchCount != 0) {
					for (int i = 0; i < Input.touchCount; i++) {
						if (Input.GetTouch (i).position.x < Screen.width / 2) {
							h = -1; 
						} else {
							h = 1; 
						}


					}
					if (Input.touchCount > 1) {
						v = -1; 
						h = 0; 
					}
				}
				#endif


				#if UNITY_EDITOR
				if (Input.GetMouseButton (0)) {
					h = Input.mousePosition.x < Screen.width / 2 ? -1 : 1; 
				} else {
					h = 0;
				}

				if (Input.GetKey (KeyCode.Space)) {
					v = -1; 
				}
				#endif



				m_Car.Move (h, v, v, 0f);

			} else {
				if (target == null || Vector3.Distance (target.position, this.transform.position) < 0.5f) {
					agent.enabled = false;
					autoControl = false; 
					rb.isKinematic = false; 
				} else {
					agent.SetDestination (target.position); 
				}
			}
        }
    }
}
