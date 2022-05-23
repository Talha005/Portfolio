using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainsignal : MonoBehaviour {

	// Use this for initialization
	public Animator Singnalanim;
	public GameObject greenlight, GreenSignalUI, redlight, RedSignalUI;
	public GameObject trainai;
	public GameObject left2,center;
	public GameObject busypanel;
	public float Delaytime;
	public GameManager Scriptacs12;
	public Gamemanagers Scriptacs22;
    public GamemanagerRace Scriptacs33;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Locomotive") 
		{
			Singnalanim.SetBool("Barrierdown", true);
			Singnalanim.SetBool("Barrierup", false);
			Invoke ("Delay",Delaytime);
			Invoke ("Delay1",5.0f);
			busypanel.SetActive (true);
			RedSignalUI.SetActive(true);
			greenlight.SetActive (false);
			GreenSignalUI.SetActive(false);
			redlight.SetActive (true);
			trainai.SetActive (true);
			left2.SetActive (false);
			center.SetActive (false);
		}
	}
	void Delay()
	{
		Singnalanim.SetBool("Barrierdown", false);
		Singnalanim.SetBool("Barrierup", true);
		redlight.SetActive (false);
		greenlight.SetActive (true);
		RedSignalUI.SetActive(false);
		GreenSignalUI.SetActive(true);
		Invoke("greenUIOfffunc", 3f);
		left2.SetActive (true);
		center.SetActive (true);
		if (Scriptacs12 != null) 
		{
			Scriptacs12.indictorspeed (true);
		}
		if (Scriptacs22 != null) 
		{
			Scriptacs22.indictorspeednew (true);
		}
        if (Scriptacs33 != null)
        {
            Scriptacs33.indictorspeedRace(true);
        }

    }
	void Delay1()
	{
		busypanel.SetActive (false);
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
