using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Cargomovement : MonoBehaviour {

	// Use this for initialization
	private float speed=10;
	private float y;
	private float x;
	private float z;
	public Transform cargolifter;
	public Transform cargochild;
	public GameObject leftbtn, rightbtn, upbtn, downbtn, forwardbtn, backwardbtn;
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (CrossPlatformInputManager.GetAxis ("Horizontal") != 0) 
		{
			var horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
			y += horizontal * speed*Time.deltaTime;
			y = Mathf.Clamp(y, -2754, -2725);
			transform.position = new Vector3(y, transform.position.y, transform.position.z);
			if (CrossPlatformInputManager.GetAxis ("Horizontal") <0) 
			{
				forwardbtn.GetComponent<Animator> ().enabled = false;

			}
			if (CrossPlatformInputManager.GetAxis ("Horizontal") >0) 
			{
				backwardbtn.GetComponent<Animator> ().enabled = false;
			}

		}
		if (CrossPlatformInputManager.GetAxis ("Vertical") != 0)
		{
			var vertical = CrossPlatformInputManager.GetAxis("Vertical");
			z += vertical * speed*Time.deltaTime;
			z = Mathf.Clamp(z , -48, -2);
			cargolifter.transform.localPosition = new Vector3(cargolifter.transform.localPosition.x, cargolifter.transform.localPosition.y,z);
			if (CrossPlatformInputManager.GetAxis ("Vertical") >0) 
			{
				leftbtn.GetComponent<Animator> ().enabled = false;
			}
			if (CrossPlatformInputManager.GetAxis ("Vertical") <0) 
			{
				rightbtn.GetComponent<Animator> ().enabled = false;
			}
		}
		if (CrossPlatformInputManager.GetAxis ("Vertical1") != 0) 
		{
			var vertical1 = CrossPlatformInputManager.GetAxis("Vertical1");
			x += vertical1*speed*Time.deltaTime;
			x = Mathf.Clamp(x, -25, -5);
			cargochild.transform.localPosition = new Vector3(cargochild.transform.localPosition.x,cargochild.transform.localPosition.y,x);

			if (CrossPlatformInputManager.GetAxis ("Vertical1") >0) 
			{
				upbtn.GetComponent<Animator> ().enabled = false;
			}
			if (CrossPlatformInputManager.GetAxis ("Vertical1") <0) 
			{
				downbtn.GetComponent<Animator> ().enabled = false;
			}
		}
	}

}
