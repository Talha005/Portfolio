using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargolifter : MonoBehaviour {

	// Use this for initialization
	private Gamemanagers _Gamemanger;
	public GameObject secondparkingon;
	public GameObject level1, level2;
	public int newcounter;
    public AudioSource chpointSound;
    //public AudioClip chPointSound;

    public GameObject[] checkpoint;
	void Start ()
    {
		_Gamemanger = FindObjectOfType<Gamemanagers> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Firstparking") 
		{
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			this.GetComponent<RCC_CarControllerV3> ().enabled = false;
			other.gameObject.SetActive (false);
			_Gamemanger.fadeloadpanel ();
			if (secondparkingon !=null)
				secondparkingon.SetActive (true);
			if (level1 != null || level2 != null) 
			{
				level1.SetActive (false);
				level2.SetActive (true);
				this.gameObject.GetComponent<AudioSource> ().enabled = false;
			}
		}
		if (other.gameObject.tag == "Secondparking") 
		{
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			this.GetComponent<RCC_CarControllerV3> ().enabled = false;
			other.gameObject.SetActive (false);
			_Gamemanger.fadeloadpaneltwo ();

		}
		if(other.gameObject.tag=="checkpoint")
		{
			newcounter += 1;
            chpointSound.Play();                                //checkpoint sound
            checkpoint [newcounter-1].SetActive (false);

			if (newcounter < checkpoint.Length) 
			{
				checkpoint [newcounter].SetActive (true);
			}
             
		}
	}
    
}
