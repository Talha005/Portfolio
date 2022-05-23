using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainmanagers : MonoBehaviour {

	// Use this for initialization
	public Animator door1,door2;
	public GameObject camepos;
	public GameObject civilianin;
	public GameObject civilianout;
	public GameObject set1civilianprefabe1,set1civilianprefabe2,set1civilianprefabe3,set1civilianprefabe4;
	public GameObject set2civilianprefabe1,set2civilianprefabe2,set2civilianprefabe3,set2civilianprefabe4;
	public GameObject civilianposin1,civilianposout1;
	public GameObject civilianposin,civilianposout;
	public bool oncetime;
	public GameManager gm;
	public GameObject Startpos,frontpos;
	public GameObject interioronff;
	public string door1open,door1close,door2open,door2close;
	void Start () 
	{
		gm = FindObjectOfType<GameManager> ();
		Instantiate(set1civilianprefabe1,civilianposin1.transform.GetChild(0).gameObject.transform.position, civilianposin1.transform.GetChild(0).gameObject.transform.transform.rotation);
		Instantiate(set1civilianprefabe2,civilianposin1.transform.GetChild(1).gameObject.transform.position, civilianposin1.transform.GetChild(1).gameObject.transform.transform.rotation);
		Instantiate(set1civilianprefabe3,civilianposin1.transform.GetChild(2).gameObject.transform.position, civilianposin1.transform.GetChild(2).gameObject.transform.transform.rotation);
		Instantiate(set1civilianprefabe4,civilianposin1.transform.GetChild(3).gameObject.transform.position, civilianposin1.transform.GetChild(3).gameObject.transform.transform.rotation);
		Instantiate(set2civilianprefabe1,civilianposout1.transform.GetChild(0).gameObject.transform.position, civilianposout1.transform.GetChild(0).gameObject.transform.transform.rotation);
		Instantiate(set2civilianprefabe2,civilianposout1.transform.GetChild(1).gameObject.transform.position, civilianposout1.transform.GetChild(1).gameObject.transform.transform.rotation);
		Instantiate(set2civilianprefabe3,civilianposout1.transform.GetChild(2).gameObject.transform.position, civilianposout1.transform.GetChild(2).gameObject.transform.transform.rotation);
		Instantiate(set2civilianprefabe4,civilianposout1.transform.GetChild(3).gameObject.transform.position, civilianposout1.transform.GetChild(3).gameObject.transform.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	public void dooropenclose1(int dooropen)
	{
		if (dooropen==1) 
		{
			door1.SetBool(door1open, false);
			door2.SetBool(door2open, false);
			door1.SetBool(door1close, true);
			door2.SetBool(door2close, true);
			if (!oncetime)
			{
				camepos.transform.Rotate (new Vector3 (0, -125, 0));
				gm.canvasonoff (true);
				oncetime = true;
			} 

		}
		if (dooropen==2) 
		{
			gm.levelcomthird();

		}


	}
	public void levelfail()
	{
		camepos.transform.Rotate (new Vector3 (0, -125, 0));
		gm.canvasonoff (false);
		gm.levelFailed ();
	}

	public void frontcam()
	{
		camepos.transform.position = frontpos.transform.position;
		camepos.transform.rotation = frontpos.transform.rotation;
		interioronff.SetActive (true);
	}
	public void resetcame()
	{
		camepos.transform.position = Startpos.transform.position;
		camepos.transform.rotation = Startpos.transform.rotation; 
		interioronff.SetActive (false);
	}
}
