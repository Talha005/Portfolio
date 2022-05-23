using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
public class CameraRotate : MonoBehaviour 
{
    public bool clicked = false;
    private float xAxis;
    private float yAxis;
    public SmoothFollow SmoothFollowCamera;
    public GameObject RotatePanel;
    public Transform CarCamObj;
    public float xSpeed,yspeed;
	public bool manualRotation;
	InGameManager GmScript;
	void Start () 
	{
#if UNITY_EDITOR
        xSpeed = 30f;
        yspeed = 10f;
#elif UNITY_IPHONE
  xSpeed = 65f;
  yspeed = 10f;
#elif UNITY_ANDROID
  xSpeed = 45f;
  yspeed = 5f;
#endif
	}
	void LateUpdate () 
	{
		if (clicked) 
		{
			
			if (Mathf.Abs (Input.GetAxis ("Mouse X")) > 0.1f) 
			{
				xAxis = Input.GetAxis ("Mouse X") * (xSpeed) * Time.deltaTime;
				CarCamObj.Rotate (0, xAxis, 0);			
			}

			if (Mathf.Abs (Input.GetAxis ("Mouse Y")) > 0.1f) {
				yAxis = Input.GetAxis ("Mouse Y") * (yspeed) * Time.deltaTime;
     			//CarCamObj.Rotate (yAxis, 0, 0);
//				GmScript.elevatedCamHeight+=(yAxis*Time.deltaTime);
			}
		}
	}
		 public void Click1()
			{
				clicked = true;

			}
		 public void click2()
			{
				clicked = false;
				manualRotation = true;
			}
	
		public void revrseBtn()
	    {
		CarCamObj.localEulerAngles = new Vector3(0f, 180f, 0f);
	
		}

		public void farwordBtn()
		{
		CarCamObj.localEulerAngles = new Vector3(0f, 0f, 0f);
		}

}
