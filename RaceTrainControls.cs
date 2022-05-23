using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.Curvy.Examples;

public class RaceTrainControls : MonoBehaviour {
	public bool Stop = false;
	float TrainSpeed;
	bool stopped;
	bool change;	
	GamemanagerRace gmr;
	public List<SplineController> Buggies;

	void Start () 
	{
		gmr = FindObjectOfType<GamemanagerRace>();
	}

	// Update is called once per frame

	void OnTriggerStay(Collider col)
	{
		//Debug.Log(col.gameObject.name);

		if (col.CompareTag("detect"))
		{
			if (col.GetComponent<TrackController>().Stop)
			{
				if (!stopped)
				{
					stopped = true;
					StopTrain();
				}
			}
			else
			{
				if (stopped)
				{

					stopped = false;
					ResumeTrain();
				}
			}
		}
	}

	

	public void ChangeTrainSpeed(bool increase)
	{
		if (!stopped)
		{
			if (increase)
			{
				for (int i = 0; i < Buggies.Count; i++)
				{
					Buggies[i].GetComponent<SplineController>().Speed += 25;
				}
			}
			else
			{
				for (int i = 0; i < Buggies.Count; i++)
				{
					Buggies[i].GetComponent<SplineController>().Speed -= 25;
				}
			}
		}
	}
	
	public void ResumeTrain()
	{
		for (int i = 0; i < Buggies.Count; i++)
		{
			Buggies[i].Speed = TrainSpeed;
		}
	}
	public void StopTrain()
	{
		for (int i = 0; i < Buggies.Count; i++)
		{
			Buggies[i].Speed = 0f;

		}

	}
}
