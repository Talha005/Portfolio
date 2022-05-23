using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFar : MonoBehaviour
{ 
 private float SetMaxFar =600f;
private float SetMedFar = 350f;
private float SetMinFar = 150f;

// Use this for initialization
     void Start()
     {
	//CameraObject.farClipPlane = 300f;

	   if (SystemInfo.systemMemorySize > 0 && SystemInfo.systemMemorySize <= 2600)
	   {
	    this.gameObject.GetComponent<Camera>().farClipPlane = SetMinFar;
	   }

	   else if(SystemInfo.systemMemorySize > 2600 && SystemInfo.systemMemorySize <= 3600)
	   {

		this.gameObject.GetComponent<Camera>().farClipPlane = SetMedFar;
	   }

	   else if (SystemInfo.systemMemorySize > 3600)
		{
		this.gameObject.GetComponent<Camera>().farClipPlane = SetMaxFar;
		}
	}
	

}
