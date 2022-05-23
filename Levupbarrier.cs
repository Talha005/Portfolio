using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSMGameStudio.RailroadSystem;
public class Levupbarrier : MonoBehaviour {

	// Use this for initialization
	private Trainmanagers Tm;
	private bool trainstop;
	// Use this for initialization
	//public GameObject lvlcmpEffect1;
	//public GameObject lvlcmpEffect2;
	public AudioSource levelfinishaudio;
    public AudioClip finish;
    void Start () 
	{
		Tm = FindObjectOfType<Trainmanagers> ();

	}

	// Update is called once per frame
	void Update () 
	{

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Locomotive") 
		{
			
				//other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;           
				this.gameObject.transform.parent.gameObject.SetActive(false);
				FindObjectOfType<GameManager>().lvlcomp();
				//levelfinishaudio.PlayOneShot(finish, 0.7F);
				//FindObjectOfType<Trainmanagers> ().dooropenclose1 (2);
				//FindObjectOfType<GameManager> ().thirdcollideron();        
			
		}
	}
}
