using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarController : MonoBehaviour
{

	public List<Transform> path;
	public Transform PathGroup;
	public bool ApplyBrakes;
	public int currentPathObject=0;
	public float carSpeed=0.3f;
	public float currentSpeed=0f;
	public float damping=1;
	float WheelRotatingSpeed;
	public float DistanceFromWaypoint = 1f;
    public float maximumSuspensionValue=2.1f,defaultSuspension, currentSuspension=0f,suspensionSpeed;
    public float Maxwheelvalue = 2.5f,defaultwheelValue,currentWheelValue,wheelSpeed;
    public Transform Body,FLWheel,FRWheel,RLWheel,RRWheel,FLaxil,FRaxil,RLaxil,RRaxil,FLModel,FRModel,RLModel,RRModel;
    public float maxAxily = 2f, currentAxily, defaultAxily, axilSpeed;
   public bool TrafficMode;
   public float StartingTime=3f;
   InGameManager gameManager;
	void Start () {
        defaultSuspension = Body.localPosition.y;
        defaultwheelValue = FLWheel.localPosition.x;
        defaultAxily = FLaxil.localPosition.y;
		GetPath ();
        gameManager = FindObjectOfType<InGameManager>();
        
       
	}
    public void startTheCar()
    {
        Invoke("StartAICar", StartingTime);
    
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
        
        Body.localPosition = new Vector3(Body.localPosition.x, currentSuspension, Body.localPosition.z);
        FLWheel.localPosition = new Vector3(currentWheelValue, FLWheel.localPosition.y, FLWheel.localPosition.z);
        FRWheel.localPosition = new Vector3(-currentWheelValue, FRWheel.localPosition.y, FRWheel.localPosition.z);
        RLWheel.localPosition = new Vector3(currentWheelValue, RLWheel.localPosition.y, RLWheel.localPosition.z);
        RRWheel.localPosition = new Vector3(-currentWheelValue, RRWheel.localPosition.y, RRWheel.localPosition.z);
        FLaxil.localPosition = new Vector3(FLaxil.localPosition.x, currentAxily, FLaxil.localPosition.z);
        FRaxil.localPosition = new Vector3(FRaxil.localPosition.x, currentAxily, FRaxil.localPosition.z);
        RLaxil.localPosition = new Vector3(RLaxil.localPosition.x, currentAxily, RLaxil.localPosition.z);
        RRaxil.localPosition = new Vector3(RRaxil.localPosition.x, currentAxily, RRaxil.localPosition.z);
        FLModel.localPosition = new Vector3(currentWheelValue,FLModel.localPosition.y, FLModel.localPosition.z);
        FRModel.localPosition = new Vector3(-currentWheelValue,FRModel.localPosition.y,  FRModel.localPosition.z);
        RLModel.localPosition = new Vector3(currentWheelValue,RLModel.localPosition.y,  RLModel.localPosition.z);
        RRModel.localPosition = new Vector3(-currentWheelValue, RRModel.localPosition.y, RRModel.localPosition.z);
        if (TrafficMode)
        {
            if (currentSuspension < maximumSuspensionValue) //for suspension
                currentSuspension += Time.deltaTime * suspensionSpeed;
            else
                currentSuspension = maximumSuspensionValue;
            if (currentWheelValue > Maxwheelvalue) //for wheel expansion
                currentWheelValue -= Time.deltaTime * wheelSpeed;
            else
               currentWheelValue = Maxwheelvalue;
            if (currentAxily < maxAxily)
                currentAxily += Time.deltaTime * axilSpeed;
            else
                currentAxily = maxAxily;


        }

        else if (!TrafficMode)
            {
                if (currentSuspension > defaultSuspension)
                    currentSuspension -= Time.deltaTime * suspensionSpeed;
                else
                    currentSuspension = defaultSuspension;
                if (currentWheelValue < defaultwheelValue)
                    currentWheelValue += Time.deltaTime * wheelSpeed;
                else
                    currentWheelValue = defaultwheelValue;
                if (currentAxily > defaultAxily)
                    currentAxily -= Time.deltaTime * axilSpeed;
                else
                    currentAxily = defaultAxily;
            }
		Move ();
	}
    public void Move()
    {
        if(!ApplyBrakes)
        {
            var rotation = Quaternion.LookRotation(path[currentPathObject].position - transform.position);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(path[currentPathObject].position.x, 0, path[currentPathObject].position.z)) <= DistanceFromWaypoint)
        {
            
            if (currentPathObject == path.Count-1)
            {
                ApplyBrakes = true;
                //currentPathObject = 0;
            }
            else
            {
            currentPathObject++;

            }
        }
        currentSpeed = Mathf.Lerp(currentSpeed, carSpeed, Time.deltaTime * 5 / 15f);

        FLModel.Rotate(Vector3.right * WheelRotatingSpeed * 100f * Time.deltaTime);
        FRModel.Rotate(Vector3.right * WheelRotatingSpeed * 100f * Time.deltaTime);
        RLModel.Rotate(Vector3.right * WheelRotatingSpeed * 100f * Time.deltaTime);
        RRModel.Rotate(Vector3.right * WheelRotatingSpeed * 100f * Time.deltaTime);
    }   
        else if (ApplyBrakes)
        {
            currentSpeed = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[currentPathObject].position.x, transform.position.y, path[currentPathObject].position.z), currentSpeed * Time.deltaTime);
        
        WheelRotatingSpeed = Mathf.Lerp(WheelRotatingSpeed, carSpeed, currentSpeed);
        
    }
    public void StartTrafficMode()
    {
        TrafficMode = true;
    }
    public void StartAICar()
    {
        ApplyBrakes = false;
    
    }
}
