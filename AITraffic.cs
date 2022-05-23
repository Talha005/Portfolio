using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITraffic : MonoBehaviour {

	public List<Transform> path;
	public Transform PathGroup;
	public bool ApplyBrakes;
	public int currentPathObject=0,RestartingPoint=0;
	public float carSpeed=0.3f;
	public float currentSpeed=0f;
	public float damping=1;
	public float SensorOrigin=0f,SensorHeight=1f,SensorLength=6f,LeftAndRightSensorAngle=1.7f;
	float WheelRotatingSpeed;
	public Transform[] Wheel;
	public string PlayerTag, AITrafficTag;
	Vector3 MiddleRayCastposition,LeftRayCastPosition,RightRaycastPostion;
	public bool Colliding,stop;
	public GameObject Headlight,BreakLight;
	public bool PlayerAhead=false;
	AudioSource horn;
	void Start () {
		horn = GetComponent<AudioSource> ();
		GetPath ();

	}


	void GetPath()
	{
		Transform[] path_objs = PathGroup.GetComponentsInChildren<Transform> ();
		path = new List<Transform> ();

		foreach(Transform path_obj in path_objs) 
		{
			if(path_obj != PathGroup)
			{
				path.Add(path_obj);
			}
		}
	}
	void Update () {
		
		CheckSensors ();
		Debug.DrawRay (transform.position, transform.forward *SensorLength,Color.green);
	}

	void FixedUpdate(){
	
		Move ();
	
	}
	public void Move(){
	
		if (!ApplyBrakes) {
			currentSpeed = Mathf.Lerp (currentSpeed, carSpeed, Time.deltaTime * SensorLength / 1f);
			Wheel [0].Rotate (Vector3.right * WheelRotatingSpeed*300f);
			Wheel [1].Rotate (Vector3.right *WheelRotatingSpeed*300f);
			Wheel [2].Rotate (Vector3.right * WheelRotatingSpeed*300f);
			Wheel [3].Rotate (Vector3.right * WheelRotatingSpeed*300f);
			if (BreakLight.activeInHierarchy) {
				BreakLight.SetActive (false);
			}

		}
		else if(ApplyBrakes){

			currentSpeed = Mathf.Lerp (currentSpeed, 0, Time.deltaTime*SensorLength);
			if (!BreakLight.activeInHierarchy) {
				BreakLight.SetActive (true);
			}

		}
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (path [currentPathObject].position.x, transform.position.y, path [currentPathObject].position.z), currentSpeed);
			var rotation = Quaternion.LookRotation (path [currentPathObject].position - transform.position);
			rotation.x = 0;
			rotation.z = 0;
			WheelRotatingSpeed = Mathf.Lerp (WheelRotatingSpeed,carSpeed,currentSpeed);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);

			//Vector3.Distance (new Vector3(0,0,transform.position.z), new Vector3(0,0,path [currentPathObject].position.z)
			if (Vector3.Distance (new Vector3 (transform.position.x, 0, transform.position.z), new Vector3 (path [currentPathObject].position.x, 0, path [currentPathObject].position.z)) <= 0.3f) {
				currentPathObject++;
				if (currentPathObject >= path.Count) {
					currentPathObject = RestartingPoint;
				}
			}
		 else if(ApplyBrakes){
			if (currentSpeed >= Mathf.Abs (0)) {
				currentSpeed = Mathf.Lerp (currentSpeed, 0, Time.deltaTime * SensorLength / 5f);
			}
		
		}
	}

	public void CheckSensors(){

		MiddleRayCastposition = transform.position;
		RightRaycastPostion = transform.position;
		LeftRayCastPosition = transform.position;
		MiddleRayCastposition += transform.forward * SensorOrigin;
		LeftRayCastPosition += transform.right * SensorOrigin;
		RightRaycastPostion += transform.right * SensorOrigin;
		MiddleRayCastposition.y += SensorHeight;
		LeftRayCastPosition.z += -LeftAndRightSensorAngle;
		RightRaycastPostion.z += LeftAndRightSensorAngle;
		RightRaycastPostion.y += SensorHeight;

		RaycastHit hit;
		if (Physics.Raycast (MiddleRayCastposition, transform.forward, out hit, SensorLength) || Physics.Raycast (LeftRayCastPosition, transform.forward, out hit, SensorLength) || Physics.Raycast (RightRaycastPostion, transform.forward, out hit, SensorLength)) {
			
			if (hit.transform.CompareTag (PlayerTag)) {
				ApplyBrakes = true;
				PlayerAhead = true;
				if (!IsInvoking("HeadlightOn")) {
					Invoke ("HeadlightOn", 5f);
				}
				if (!IsInvoking ("HornPlay")) {
					Invoke ("HornPlay", 6.5f);
				}

				Debug.DrawLine (transform.position, hit.point, Color.red);
		
			} else if (hit.transform.CompareTag (AITrafficTag) ||(hit.transform.CompareTag ("AiTraffic"))) {

				if (IsInvoking("HeadlightOn")) {
					CancelInvoke ("HeadlightOn");
				}
				if (IsInvoking ("HornPlay")) {
					CancelInvoke ("HornPlay");
				}
				ApplyBrakes = true;
				PlayerAhead = false;
				Debug.DrawLine (transform.position, hit.point, Color.red);

			} 
				
		
		} 


		else if (!Physics.Raycast (RightRaycastPostion, transform.forward, out hit, SensorLength) && !stop) {
			if (IsInvoking("HeadlightOn")) {
				CancelInvoke ("HeadlightOn");
			}
			if (IsInvoking ("HornPlay")) {
				CancelInvoke ("HornPlay");
			}
			ApplyBrakes = false;
			PlayerAhead = false;
		}
	}


	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("collisioning");
		if (col.gameObject.CompareTag ("Player")) {
			Debug.Log ("collisioning with player");
			stop = true;
			Colliding = true;
			ApplyBrakes = true;
		}
	}

	void OnCollisionExit(Collision col)
	{
		Debug.Log ("collisioning");
		if (col.gameObject.CompareTag ("Player")) {
			Debug.Log ("collisioning with player");
			Colliding = false;
			if (!IsInvoking ("StartMoving")) {
				Invoke ("StartMoving",6f);
			}

		}

	}

	public void HeadlightOn()
	{
	
//		StartCoroutine ("TurnOnHeadLights");
	}

	public void HornPlay()
	{

//		horn.Play ();
	}
	public IEnumerator TurnOnHeadLights(){

		if(PlayerAhead){
		Headlight.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		Headlight.SetActive (false);
		yield return new WaitForSeconds (0.5f);
		Headlight.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		Headlight.SetActive (false);
		}

	
	}




	public void StartMoving(){
		if (!Colliding) {
			ApplyBrakes = false;
			stop = false;
		} else {
		
			Invoke ("StartMoving",6f);
		}
	}
	void OnDisable()
	{
	
		StopAllCoroutines ();
	
	}
}
