using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCarScript : MonoBehaviour 
{
    public Transform FLAxil, FLAxilMidddlepart, FLAxilLowerPart, FRAxil,FRAxilMiddlePart,FRAxilLowerpart,RLAxil,RLAxilMiddlepart,RLAxilLowerpart,RRAxil,RRAxilMiddlepart,RRAxilLowerpart;
    public Transform FLAxilJunctionPart, FLAxilWheelPart,FRAxilJunctionpart,FRAxilWheelPart,RLAxilJunctionpart,RLAxilWheelpart,RRAxilJunctionpart,RRAxilWheelpart;
    public float TransformationSpeed = 0.1f;
    public lookAtDest ArrowScript;
    InGameManager GameManager;
    public List<Collider> ColliderList;
    ParkingCollider TriggerScript;
    public Color Parking1, Parking2, Parking3, Parking4;
    float resultAngle;
    public bool OnParkingPoint;
    
    public Transform CameraTarget;
    public Transform MoveToTarget;
    public Transform InnerCameraPosition, InnerCameraPositionRev;

    public bool parkingLevelOnly; // for arrow path;
    bool aiLevel,resetTimer,showArrow;
    float distance,timer;
    Transform aicar;
    public Text distanceLabel;
	public Material colmaterial;
    public GameObject SensorCam;
//	public GameObject Headlighttt;
	bool rotateOnce=false;
    public GameObject FRAxilpart, FLAxilpart, RRAxilpart, RLAxilpart;
    LevelDataScript LevelData;
    public Material Transparent;
    public Color ClrTranparent,NonTransparent;
    void Start () 
    {
        GameManager = FindObjectOfType<InGameManager>();
        LevelData = FindObjectOfType<LevelDataScript>();
        ArrowScript = GameManager.ArrowScript;        
        parkingLevelOnly = GameManager.LevlesScript[GameManager.currentLevel].parkingModeOnly;
        aiLevel = GameManager.LevlesScript[GameManager.currentLevel].aiLevel;
        if (aiLevel)
        {
            aicar = GameManager.LevlesScript[GameManager.currentLevel].AIVehicle;
        }
        timer = 10f;
        showArrow = GameManager.LevlesScript[GameManager.currentLevel].showArrow;
        distanceLabel = GameManager.DistanceLabel;
//		if (PlayerPrefs.GetInt ("CurrentLevel") >= 46) {
//			Headlighttt.SetActive (false);
//		} else {
//			if (Headlighttt != null)
//				Headlighttt.SetActive (false);
//		}
	}
	// Update is called once per frame
	void Update () 
    {
        if (showArrow)
        {
            if (ArrowScript.Target)
            {

                NextPoint();
            }
        
        }
        if (aiLevel)
        {
            //checkDistance();
        
        }
		//if (rotateOnce)
			//CameraTarget.transform.Rotate (Vector3.up * (45f * Time.deltaTime));
	}
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "transparent")
        {
          
            Debug.Log("ChangeMAt");
            Transparent.color = ClrTranparent;
        }
        if (col.gameObject.tag == "transparentOff")
        {

            Debug.Log("ChangeMAt");
            Transparent.color = NonTransparent;
        }
        //if (col.CompareTag("collider"))
        //{
        //    if (col.name == "ParkingArea")
        //    {
        //        GameManager.MissionComplete();
        //    }
        //}

        if (col.CompareTag("collider"))
        {
//            if (col.name == "WrongParkingArea")
//            {
//                if (!ColliderList.Contains(col))
//                {
//                    ColliderList.Add(col);
//                }
//            }
            if (col.name == "ParkingArea")
            {
                if (TriggerScript != col.GetComponent<ParkingCollider>())
                {
                    TriggerScript = col.GetComponent<ParkingCollider>();
                }
                TriggerScript.BusMarkTexture.color = Color.Lerp(Parking1, Parking2, 5f);
                TriggerScript.Arrow.SetActive(false);
            }
        }
        if (col.CompareTag("tutorial"))
        {
            if (GameManager.ShowTutorial)
            {
                if (col.name == "0")
                {
                    col.gameObject.SetActive(false);
                    GameManager.StartTutorial(0);
                }
                else if (col.name == "1")
                {
                    GameManager.StartTutorial(1);
                    col.gameObject.SetActive(false);
                }
            }
        }
        if (col.CompareTag("Info"))
        {

            if (col.name == "Switch Info")
            {
                GameManager.showInfo(1);
            }
        }


		if (col.gameObject.tag == "ShowRoBTN") 
		{
			if (PlayerPrefs.GetInt ("RTTTT") == 0) 
            {
				GameManager.RotateHighlight.SetActive (true);
				PlayerPrefs.SetInt ("RTTTT", 1);
			}
			col.gameObject.SetActive (false);
			GameManager.ShowRotateBTN = true;
			GameManager.RotateHighlightingBTN.SetActive (true);
			GameManager.LeftRightSteerBtns.SetActive (false);
			GameManager.LeftHighlightingBar.SetActive (false);
			GameManager.cancelsteerLeftBTN ();
			GameManager.cancelSteerRightBTN ();
		}
        
		if(col.gameObject.tag=="Finishparking")
        {
			if (!OnParkingPoint)
			{
//				rotateOnce = true;
//				if (col.GetComponent<ParkingCollider> ().Completeparticle != null)
//					col.GetComponent<ParkingCollider> ().Completeparticle.SetActive (true);
				StartCoroutine(CompletFireWorks(col));
				OnParkingPoint = true;
				GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			}
		}
        if (col.gameObject.tag == "FinishparkingElevated")
        {
           
            TriggerScript.Completeparticle.SetActive(true);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
	

	IEnumerator CompletFireWorks(Collider fireworkCol)
    {
		rotateOnce = false;
		yield return new WaitForSeconds (0.3f);

		rotateOnce = true;
//		yield return new WaitForSeconds (0.2f);
//		if (fireworkCol.GetComponent<ParkingCollider> ().Completeparticle != null)
//			fireworkCol.GetComponent<ParkingCollider> ().Completeparticle.SetActive (false);
		yield return new WaitForSeconds (0.3f);
		if (fireworkCol.GetComponent<ParkingCollider> ().Completeparticle != null)
			fireworkCol.GetComponent<ParkingCollider> ().Completeparticle.SetActive (true);
       
		if (fireworkCol.GetComponent<ParkingCollider> ().completeAudio != null)
			fireworkCol.GetComponent<ParkingCollider> ().completeAudio.SetActive (true);
//		if (fireworkCol.GetComponent<ParkingCollider> ().Completeparticle != null)
//			fireworkCol.GetComponent<ParkingCollider> ().Completeparticle.SetActive (true);
//		yield return new WaitForSeconds (0.2f);
//		if (fireworkCol.GetComponent<ParkingCollider> ().Completeparticle != null)
//			fireworkCol.GetComponent<ParkingCollider> ().Completeparticle.SetActive (false);
		yield return new WaitForSeconds (1f);
//		if (fireworkCol.GetComponent<ParkingCollider> ().Completeparticle != null)
//			fireworkCol.GetComponent<ParkingCollider> ().Completeparticle.SetActive (true);
		rotateOnce = false;
	}
    public void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("collider"))
        {
            if (GameManager)
            {
                if (col.name == "TotalParkingArea" && !GameManager.changeCamHeight)
                {
                    GameManager.changeCamHeight = true;
                }
            }
           
          
            if (col.name == "ParkingArea")
            {
                // && !AllClared
                if (ColliderList.Count == 0)
                {
                    resultAngle = Mathf.DeltaAngle(col.transform.eulerAngles.y, transform.eulerAngles.y);
                    resultAngle = Mathf.Abs(resultAngle);
//                    if (resultAngle < 50)
//                    {
                        if (GameManager.NormalMode)//!GameManager.TrafficMode
                    {
                            TriggerScript.BusMarkTexture.color = Color.Lerp(Parking2, Parking3, 5f);
                            if (GameManager.ReverseMark.activeInHierarchy)
                            {
                                GameManager.ReverseMark.SetActive(false);
                            }
                            if (GetComponent<Rigidbody>().velocity.magnitude < 5f)
                            {
                                if (!OnParkingPoint)
                                {
                                    OnParkingPoint = true;
                                }
                                if (GameManager.StopMark.activeInHierarchy)
                                {
                                    GameManager.StopMark.SetActive(false);
                                }
                            }
                            else
                            {
                                OnParkingPoint = false;
                                if (!GameManager.StopMark.activeInHierarchy)
                                {
                                    GameManager.StopMark.SetActive(true);
                                }
                            }
                        }
//                    }
//                    else
//                    {
//                        if (!GameManager.ReverseMark.activeInHierarchy)
//                        {
//                            GameManager.ReverseMark.SetActive(true);
//                        }
//                    }
                }
                else
                {
                    if (GameManager.ReverseMark.activeInHierarchy)
                    {
                        GameManager.ReverseMark.SetActive(false);
                    }
                    TriggerScript.BusMarkTexture.color = Color.Lerp(Parking3, Parking2, 3f);
                    if (OnParkingPoint)
                    {
                        OnParkingPoint = false;
                    }
                    if (GameManager.StopMark.activeInHierarchy)
                    {
                        GameManager.StopMark.SetActive(false);
                    }
                }
            }
        }
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("collider"))
        {
//            if (col.name == "WrongParkingArea")
//            {
//
//                if (ColliderList.Contains(col))
//                {
//
//                    ColliderList.Remove(col);
//                }
//            }

            if (col.name == "ParkingArea")
            {

                TriggerScript.BusMarkTexture.color = Color.Lerp(Parking2, Parking1, 5f);
                TriggerScript.Arrow.SetActive(true);
				if (GameManager.StopMark.activeInHierarchy)
				{
					GameManager.StopMark.SetActive(false);
				}

            }
            if (col.name == "TotalParkingArea")
            {
                if (!GameManager.TrafficMode)
                {
                    GameManager.changeCamHeight = false;
                }
               

            }
        }
        if (col.CompareTag("Info"))
        {

            if (col.name == "Switch Info")
            {
                GameManager.ClearInfo();
            }
        }

    }

    
    public void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag("Bracker"))
        {
            if (col.relativeVelocity.magnitude > 0.5f)
            {
				if (col.gameObject.tag != "ignore") 
                {              
					GameManager.GetDamage ();
                    
                    if (col.gameObject.GetComponent<MeshRenderer>())
                    {
                        col.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    }
					
				}
                if (col.gameObject.name == "GATE_Vehicle_Moving")
                {
                    col.gameObject.GetComponent<BoxCollider>().enabled = false;
                    if (SensorCam != null)
                        SensorCam.SetActive(false);
                }
//				col.gameObject.GetComponent<MeshRenderer>().material.SetColor("_SomeColor", Color.red);
            }
        }
      
        // GameManager.isCollisoning = true;

    }

  
        
   
public void NextPoint()
    {
        if (!parkingLevelOnly)
        {


            if (Vector3.Distance(transform.position, ArrowScript.Target.position) <= ArrowScript.DstanceFromPoint)
            {
                if (ArrowScript.counter < ArrowScript.path.Count - 1)
                {
                    ArrowScript.counter++;
                    ArrowScript.Target = ArrowScript.path[ArrowScript.counter];
                }
                else
                {
                    ArrowScript.gameObject.SetActive(false);

                }

            }
        }
        else
        {

            if (Vector3.Distance(transform.position, ArrowScript.Target.position) <= 5f)
            {
                if (ArrowScript.counter < ArrowScript.path.Count - 1)
                {
                    ArrowScript.counter++;
                    ArrowScript.Target = ArrowScript.path[ArrowScript.counter];
                    ArrowScript.gameObject.SetActive(false);
                }
                else
                {
                    ArrowScript.gameObject.SetActive(false);
                }

            }
        
        }


    }
    public void checkDistance()
    {

        distance = Vector3.Distance(transform.position,aicar.position);
        distanceLabel.text = ((int)distance).ToString();
        if (distance > 30f)
        {
            CheckTimer();
            resetTimer = false;
        }
        else if (distance<=30f)
        {
            if (!resetTimer)
            {
                resetTimer = true;
                timer = 10f;
                GameManager.CountDown.text = "";
            }
        
        }
    }
    public void CheckTimer()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            GameManager.CountDown.text = ((int)timer).ToString();
        }
        else
        {

            GameManager.MissionFail();
            GameManager.CountDown.text = "";
        }

    }
}
