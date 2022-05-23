using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelcombarrier : MonoBehaviour {

	// Use this for initialization
	private Tankertrainman Tm1;
	private Gamemanagers ___Gms;
    public GameObject lvlcmpEffect1;
    public GameObject lvlcmpEffect2;
    public AudioSource levelfinishaudio;
    public AudioClip finish;
    // Use this for initialization
    void Start () 
	{
		Tm1= FindObjectOfType<Tankertrainman> ();
		___Gms = FindObjectOfType<Gamemanagers> ();
	}

	// Update is called once per frame
	void Update () 
	{

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Locomotive") 
		{
           levelfinishaudio.PlayOneShot(finish, 0.7F);
           lvlcmpEffect1.SetActive(true);
           lvlcmpEffect2.SetActive(true);
            ___Gms.lvlcomp();
			this.gameObject.transform.parent.gameObject.SetActive (false);
		}
	}
}
