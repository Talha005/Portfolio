using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;

public class LevelcombarrierRace : MonoBehaviour {

	// Use this for initialization
	private Tankertrainman Tm1;
	private GamemanagerRace Gmr;
    public GameObject lvlcmpEffect1;
    public GameObject lvlcmpEffect2;
    public AudioSource levelfinishaudio;
    public AudioClip finish;
	public TrainData train;
	// Use this for initialization
	void Start () 
	{
		Tm1= FindObjectOfType<Tankertrainman> ();
		Gmr = FindObjectOfType<GamemanagerRace> ();
	}

	// Update is called once per frame
	void Update () 
	{

	}
	private void OnTriggerEnter(Collider other)
	{		
		if (other.gameObject.tag == "Win") 
		{
			//Debug.Log("Win");
			//Gmr.PlayerWin();
			//train.targetspeed = 0f;
		 //  //other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		 //  levelfinishaudio.PlayOneShot(finish, 0.7F);
   //        lvlcmpEffect1.SetActive(true);
   //        lvlcmpEffect2.SetActive(true);
           //Gmr.lvlcomp();
			//this.gameObject.transform.parent.gameObject.SetActive (false);
		}
        if(other.gameObject.tag == "Lose")
		{
			//Debug.Log("Lose");
			//Gmr.RivalWin();
			//train.StopTrain();
			////other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			//Gmr.levelFailed();
   //         //this.gameObject.transform.parent.gameObject.SetActive(false);
        }
	}
}
