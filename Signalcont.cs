using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signalcont : MonoBehaviour {

	// Use this for initialization
	public Animator Singnalanim;
	public GameObject greenlight, GreenSignalUI, redlight, RedSignalUI;
	public GameObject vehicle1,vehicle2;
	public GameObject busypanel;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Locomotive") 
		{
			Singnalanim.SetBool("Barrierdown", true);
			Singnalanim.SetBool("Barrierup", false);
			Invoke ("Delay",20.0f);
			Invoke ("Delay1",4.0f);
			busypanel.SetActive (true);
			RedSignalUI.SetActive(true);
			greenlight.SetActive (false);
			GreenSignalUI.SetActive(false);
			redlight.SetActive (true);
			vehicle1.SetActive(true);
			vehicle2.SetActive(true);
		}
	}
	void Delay1()
	{
		busypanel.SetActive (false);
	}
	void Delay()
	{
		Singnalanim.SetBool("Barrierdown", false);
		Singnalanim.SetBool("Barrierup", true);
		redlight.SetActive (false);
		RedSignalUI.SetActive(false);
		greenlight.SetActive (true);
		GreenSignalUI.SetActive(true);
		Invoke("greenUIOfffunc",3f);
		FindObjectOfType<GameManager>().indictorspeed (true);
	}
	void OnDisable()
	{

		if (IsInvoking ("Delay"))
			CancelInvoke ("Delay");
		if (IsInvoking ("Delay1"))
			CancelInvoke ("Delay1");
	}
	void greenUIOfffunc()
    {
		GreenSignalUI.SetActive(false);
    }
}
