using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSMGameStudio.RailroadSystem;
public class Levelcom : MonoBehaviour 
{

	// Use this for initialization

	private bool trainstop;
    private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Locomotive") 
		{
			
			if (other.gameObject.GetComponent<TrainController_v3> ().Speed_KPH == 0 &&!trainstop) 
			{
				trainstop = true;
                //FindObjectOfType<Trainmanagers> ().dooropenclose1 (2);
                //FindObjectOfType<GameManager> ().thirdcollideron();
                FindObjectOfType<GameManager> ().lvlcomp();
				Destroy (this.gameObject.transform.parent.gameObject);
			}


		}

	}

}
