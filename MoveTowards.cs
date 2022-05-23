using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour 
{

    public Transform Target;
    public int speed = 10;
	public int rotationspeed;
	[HideInInspector]
	public PlayerCarScript Player;

	// Use this for initialization
	void Start()
	{
		Player = FindObjectOfType<PlayerCarScript>();
		if (PlayerPrefs.GetInt("CurrentLevel") >= 21)
		{
			Target = Player.MoveToTarget;
		}
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Invoke("SpeedSet", 6f);
        transform.position = Vector3.MoveTowards(transform.position,Target.position,speed*Time.deltaTime);
	    transform.rotation = Quaternion.RotateTowards(transform.rotation, Target.rotation, Time.deltaTime * rotationspeed);
		
	}

	public void SpeedSet()
    {	
		speed = 20;	
    }
}
