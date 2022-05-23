using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pullsignal : MonoBehaviour {

	// Use this for initialization
	public Animator Singnalanim;
	public GameObject greenlight, GreenSignalUI, redlight, RedSignalUI;
	public GameObject car1, car2;
	public Animator shipanim;
	public GameObject pathon, pathoff;
	public GameObject busypanel;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Locomotive") 
		{
			Singnalanim.SetBool("Barrierdown", true);
			Singnalanim.SetBool("Barrierup", false);
			shipanim.SetBool("Shippullopen", false);
			shipanim.SetBool("Shippullclose", true);
			Invoke ("Delay",20.0f);
			Invoke ("Delay1",5.0f);
			busypanel.SetActive (true);
			RedSignalUI.SetActive(true);
			greenlight.SetActive (false);
			GreenSignalUI.SetActive(false);
			redlight.SetActive (true);
			pathon.SetActive (false);
			pathoff.SetActive (false);
			if (car1||car2) 
			{
				car1.gameObject.GetComponent<Movement> ().enabled = true;
				car2.gameObject.GetComponent<Movement> ().enabled = true;
			}
		}
	}
	void Delay1()
	{
		busypanel.SetActive (false);
	}
	void Delay()
	{
		print ("DDDDDDDD");
		Singnalanim.SetBool("Barrierdown", false);
		Singnalanim.SetBool("Barrierup", true);
		shipanim.SetBool("Shippullopen", true);
		shipanim.SetBool("Shippullclose", false);
		redlight.SetActive (false);
		RedSignalUI.SetActive(false);
		greenlight.SetActive (true);
		GreenSignalUI.SetActive(true);
		Invoke("greenUIOfffunc", 3f);
		pathon.SetActive (true);
		pathoff.SetActive (true);
		FindObjectOfType<GameManager>().indictorspeed (true);
		if (car1||car2) 
		{
			car1.gameObject.GetComponent<Movement> ().enabled = false;
			car2.gameObject.GetComponent<Movement> ().enabled = false;
		}
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
