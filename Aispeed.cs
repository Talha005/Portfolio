using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aispeed : MonoBehaviour
{
    public float speed;
    float PreviousSpeed=5;
	void Start () {
		
	}
	void Update () {
		
	}
    public void OnTriggerEnter(Collider col)
    {
       
        if (col.CompareTag("ai"))
        {
            PreviousSpeed = col.gameObject.GetComponent<AICarController>().carSpeed;
                col.gameObject.GetComponent<AICarController>().carSpeed = speed;
        }
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("ai"))
        {
            col.gameObject.GetComponent<AICarController>().carSpeed = PreviousSpeed;            
        }
    }
}
