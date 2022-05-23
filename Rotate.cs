using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	public enum Axis { x,y,z};
	public enum Direction {x,y,z};
	public Axis rotationAxis;
	public Direction direction;
	public float RotationSpeed=5f,directionSpeeed=5f;
	public bool Translate,rotate,usePingpong;
	public float finalvalue;
	Vector3 startingPosition;
	Vector3 startingrotation;
	// Use this for initialization
	void Start () {
		startingPosition = gameObject.transform.position;
		startingrotation = gameObject.transform.eulerAngles;

	}
	void OnDisable()
	{
	
		transform.position = startingPosition;
		transform.eulerAngles = startingrotation;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (rotate) {
			if (rotationAxis == Axis.x) {
				transform.Rotate (Vector3.right * RotationSpeed * Time.deltaTime);
			} else if (rotationAxis == Axis.y) {
				transform.Rotate (Vector3.up * RotationSpeed * Time.deltaTime);
			} else if (rotationAxis == Axis.z) {
				transform.Rotate (Vector3.forward * RotationSpeed * Time.deltaTime);
			}
		}
		if (Translate) {
			
			if (direction == Direction.x) {
				transform.Translate (Vector3.right * directionSpeeed * Time.deltaTime);
			} else if (direction == Direction.y) {
				transform.Translate (Vector3.up * directionSpeeed * Time.deltaTime);
			} else if (direction == Direction.z) {
				//if (transform.position.z < finalvalue) {
				transform.Translate (Vector3.forward * directionSpeeed * Time.deltaTime);
				//} else {
				//	transform.position = new Vector3 (transform.position.x,transform.position.y,finalvalue);
				//}
			}
		}
	}

	}
	

