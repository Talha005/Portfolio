using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aicollider : MonoBehaviour {

    public bool FirstTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter(Collider col)
    {
       
        if (col.CompareTag("ai"))
        {
            //if (!FirstTime)
           // {
                col.gameObject.GetComponent<AICarController>().TrafficMode = true;
                FirstTime = true;
           // }
        }
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("ai"))
        {
           // if (FirstTime)
           // {
                col.gameObject.GetComponent<AICarController>().TrafficMode = false;
                FirstTime = false;            
           // }
        }
    }
}
