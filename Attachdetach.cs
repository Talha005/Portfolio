using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachdetach : MonoBehaviour 
{
	// Use this for initialization
	// Use this for initialization
	public BoxCollider locmotoive;
	public BoxCollider bokiyan;
	private Gamemanagers ___Gmds;
	void Start () 
	{
		___Gmds = FindObjectOfType<Gamemanagers> ();
	}

	// Update is called once per frame

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Reversedetec") 
		{
			other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			___Gmds.traindetachfun ();
			___Gmds.indictorbrakenew (false);
			locmotoive.enabled = false;
			bokiyan.enabled = true;
			//Destroy (this.gameObject);

		}
	}

}
