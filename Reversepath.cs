using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Reversepath : MonoBehaviour {

	public GameObject LeftPath,RightPath,CenterPath;
	public static  bool TrainEnter;
	public  Leftrightbuttontrain Trainbutton;
	public GameObject leftrightimageonff;
	void Start () 
	{
		//Trainbutton = FindObjectOfType<Leftrightbuttontrain> ();
		if (leftrightimageonff == null)
			return;


	}

	// Update is called once per frame
	void Update () 
	{
		if (TrainEnter)
		{
			if (Trainbutton.lefttrainmoveint==0&&Trainbutton.righttrainmoveint==0) 
			{
				CenterPath.SetActive (true);
				LeftPath.SetActive (false);
				RightPath.SetActive (false);
			}
			if (Trainbutton.lefttrainmoveint==1) 
			{
				CenterPath.SetActive (false);
				LeftPath.SetActive (true);
				RightPath.SetActive (false);
				print ("Left");
				if (leftrightimageonff != null)
					leftrightimageonff.SetActive (false);

			}
			if (Trainbutton.righttrainmoveint==1) 
			{
				CenterPath.SetActive (false);
				LeftPath.SetActive (false);
				RightPath.SetActive (true);
				print ("right");
				if (leftrightimageonff != null)
					leftrightimageonff.SetActive (false);

			}
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Reversedetec") 
		{
			TrainEnter = true;
			//Trainbutton = FindObjectOfType<Leftrightbuttontrain> ();
			if (leftrightimageonff != null)
				leftrightimageonff.SetActive (true);

		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Reversedetec") 
		{
			TrainEnter = false;
			Invoke ("Indicatoronoff",3.0f);
			if (leftrightimageonff != null)
				leftrightimageonff.SetActive (false);
		}
	}
	private void Indicatoronoff()
	{
		Trainbutton.indicatoroff ();
	}
}
