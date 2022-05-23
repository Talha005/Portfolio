using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;
public class TrainData : MonoBehaviour {

	public train[] trainpart;
	public train[] axilfront;
	public train[] axilback;
	public float TrainSpeed=25f,currentSpeed,targetspeed;
	public bool Stop = false;
	public RivalTrainController RTC;
	public int speed;
	public float speedfactor =1f;
	public  bool letsGo;
	public GamemanagerRace GMR;
	[System.Serializable]
	public class train
	{
		public SplineController spline;
		//public float position;

	}
	// Use this for initialization
	void Start()
	{
		currentSpeed = TrainSpeed;
		GMR = FindObjectOfType<GamemanagerRace>();
	}
	void Update ()
	{
		//currentSpeed=
		//if (currentSpeed <= 0)
		//{
		//	currentSpeed = 0;
		//}
		if(letsGo)
		currentSpeed = Mathf.Lerp(currentSpeed,targetspeed, speedfactor * Time.deltaTime);		
		//if (currentSpeed < 2f)
		//{
		//	currentSpeed = 0f;
		//}
		Invoke("applySpeed",4.5f);
	}
	public void SetSpline(CurvySpline spline)
	{
		for (int i = 0; i < trainpart.Length; i++)
		{
			trainpart[i].spline.Spline = spline;
			//trainpart[i].spline.Position = trainpart[i].position;
			trainpart[i].spline.enabled = true;			
		}
	}

	public void SetplayerSpline(CurvySpline spline, levelData data)
	{
		for (int i = 0; i < trainpart.Length; i++)
		{
			trainpart[i].spline.Spline = spline;
			axilfront[i].spline.Spline = spline;
			axilback[i].spline.Spline = spline;
			trainpart[i].spline.Position = data.trainPositions[i];
			axilfront[i].spline.Position = data.Playertrainaxilfront[i];
			axilback[i].spline.Position = data.Playertrainaxilback[i];
			trainpart[i].spline.enabled = true;
		}
	}

	public void SetRivalSpline(CurvySpline spline, levelData data)
	{
		for (int i = 0; i < trainpart.Length; i++)
		{
			trainpart[i].spline.Spline = spline;
			axilfront[i].spline.Spline = spline;
			axilback[i].spline.Spline = spline;
			trainpart[i].spline.Position = data.RivalPositions[i];
			axilfront[i].spline.Position = data.Rivaltrainaxilfront[i];
			axilback[i].spline.Position = data.Rivaltrainaxilback[i];
			trainpart[i].spline.enabled = true;
		}	
	}
	public void StartTrain()
	{

		for (int i = 0; i < trainpart.Length; i++)
		{
			trainpart[i].spline.Speed = currentSpeed;
			axilfront[i].spline.Speed = currentSpeed;
			axilback[i].spline.Speed = currentSpeed;
		}
	}

	public void applySpeed()
	{

		for (int i = 0; i < trainpart.Length; i++)
		{
			trainpart[i].spline.Speed = currentSpeed;
			axilfront[i].spline.Speed = currentSpeed;
			axilback[i].spline.Speed = currentSpeed;

		}
	}
    public void StopTrain()
    {
        for (int i = 0; i < trainpart.Length; i++)
        {
            trainpart[i].spline.Speed = 0f;
		    axilfront[i].spline.Speed = 0f;
			axilback[i].spline.Speed = 0f;

		}
    }
}
