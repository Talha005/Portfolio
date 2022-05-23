using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSMGameStudio.RailroadSystem;
public class LevelupRace : MonoBehaviour {

	// Use this for initialization
	private bool trainstop;
	private Tankertrainman ___Traintank;
	private DemoUI_v3 ___Dm;
	private GamemanagerRace ___Gmr;
    public GameObject lvlcmpEffect;
	public TrainData train;

	void Start()
	{
		___Traintank = FindObjectOfType<Tankertrainman> ();
		___Dm = FindObjectOfType<DemoUI_v3> ();
		___Gmr = FindObjectOfType<GamemanagerRace> ();
	}
	//private void OnTriggerStay(Collider other)
	//{
	//	{
	//		if (other.gameObject.tag == "Locomotive")
	//		{
	//			___Gmr.PlayerWin();
	//			train.StopTrain();
	//			//other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	//			___Gmr.lvlcomp();
	//			this.gameObject.transform.parent.gameObject.SetActive(false);
	//		}
	//		if (other.gameObject.tag == "Rival")
	//		{
	//			___Gmr.RivalWin();
	//			train.StopTrain();
	//			//other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	//			___Gmr.levelFailed();
	//			this.gameObject.transform.parent.gameObject.SetActive(false);
	//		}
	//	}

	//}
}
