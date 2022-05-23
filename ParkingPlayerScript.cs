using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePArkingMgr : MonoBehaviour {

	public List<Collider> ColliderList;
	public Color Parking1,Parking2,Parking3,Parking4;
	public bool OnParkingPoint;
	//ParkingCollider TriggerScript;
	//ParkingGameManager ParkingManager;
	float resultAngle;
	public bool AllClared=false;
	// Use this for initialization
	void Start () {
		//ParkingManager = FindObjectOfType<ParkingGameManager> ().GetComponent<ParkingGameManager> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnTriggerEnter(Collider col)
	{

		if(col.CompareTag ("collider"))
			{


				if(col.name=="WrongParkingArea")
				{

				if (!ColliderList.Contains (col)) {
				
					ColliderList.Add (col);
				}
				}
			}

		if (col.CompareTag ("ChangeCameraSettings")) {
			float angle = float.Parse (col.name);
            //StartCoroutine (ParkingManager.SetTargetPosition (angle));	

		}


	}

	public void OnTriggerStay(Collider col)
	{

		if(col.CompareTag ("collider"))
			{

			if (col.name == "ParkingArea") 
			{

				if (ColliderList.Count == 0 && !AllClared) {

					resultAngle =Mathf.DeltaAngle(col.transform.eulerAngles.y, transform.eulerAngles.y);
					resultAngle = Mathf.Abs (resultAngle);
					if (resultAngle < 50) {
					
                        //TriggerScript.BusMarkTexture.color = Color.Lerp (Parking2, Parking3, 5f);
                        //if (ParkingManager.ReverseMark.activeInHierarchy) {
						
                        //    ParkingManager.ReverseMark.SetActive (false);
						
                        //}

						if (GetComponent <Rigidbody> ().velocity.magnitude < 0.03f) {


							if (!OnParkingPoint) {

								OnParkingPoint = true;


							}
                            //if (ParkingManager.StopMark.activeInHierarchy) {

                            //    ParkingManager.StopMark.SetActive (false);
                            //}
						} else {

							OnParkingPoint = false;
                            //if (!ParkingManager.StopMark.activeInHierarchy) {

                            //    ParkingManager.StopMark.SetActive (true);
                            //}

						}
					
					} else {



                        //if (!ParkingManager.ReverseMark.activeInHierarchy) {

                        //    ParkingManager.ReverseMark.SetActive (true);

                        //}
					}

				
				} 

				else {
				
                    //if (ParkingManager.ReverseMark.activeInHierarchy) {

                    //    ParkingManager.ReverseMark.SetActive (false);

                    //}
                    //TriggerScript.BusMarkTexture.color = Color.Lerp (Parking3, Parking2, 5f);


						if (OnParkingPoint) {

							OnParkingPoint = false;

						}
                    //if (ParkingManager.StopMark.activeInHierarchy) {

                    //    ParkingManager.StopMark.SetActive (false);
                    //}
							
				
				
				}
			
			}
			}
	
	}

	public void OnTriggerExit(Collider col)
	{
		if(col.CompareTag ("collider"))
		{


			if(col.name=="WrongParkingArea")
			{

				if (ColliderList.Contains (col)) {

					ColliderList.Remove (col);
				}
			}

			if (col.name == "ParkingArea") 
			{

                //TriggerScript.BusMarkTexture.color = Color.Lerp (Parking2, Parking1, 5f);
                //TriggerScript.Arrow.SetActive (true);

			}
		}

	}

	public void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag ("collisiondetector")) {
			if (col.relativeVelocity.magnitude > 0.1f) {
                //ParkingManager.vehicleHit ();
			}
		}
	
	}



		
}
