using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Invector.CharacterController;
using WSMGameStudio.RailroadSystem;
using UnityStandardAssets.CrossPlatformInput;
public class Gamemanagers : MonoBehaviour {

	// Use this for initialization
	public GameObject Tut;
	public GameObject Forwardbtn;
	public GameObject Gmplaybtn,fadeinfadeoutpanel,thirdpersonpanel,fadeinfadeoutpanelcargo;
	//public GameObject Thirdpersonchar,Thirdpersoncam;
	public GameObject traincamera;
	public GameObject[] Starttcolliderdetect;
	public GameObject bokiconnect;
	private TrainController_v3 ___Tv;
	private DemoUI_v3 ___Dmi;
	public GameObject[] Miniindicator;
	//public GameObject levelcomplelpanel;
	public GameObject Nextbtn;
	public GameObject Locomotive,wagon;
	public GameObject[] Locopos,wagonspos;
	[Header("Cargo")]
	public GameObject[] Cargo1;
	public GameObject[] Cargo2, Cargo3, Cargo4;	 
	public GameObject[] gameplaypanel;
	private vThirdPersonController Inputcha;
	public GameObject gateopenbtn;
	private bool oncetimecargo;
	private  int levelselectionnumber;
	public GameObject[] levels;
	private float currenttime;
	private float timesteps=1.0f;
	private  bool isnotwin;
	private int cameraswitch=1;
	public GameObject cameraaon;
	public GameObject inputtouchcampanel;
	public Text leveltext;
	public Text scoretext,scoretext3;
	public GameObject[] Thirdpersonpos;
	public GameObject[] backcolliders;
	public GameObject Tutpanel1,Tutpanel2,Tutpanel3,Tutpanel4,Tutpanel5, Tutpanel6;
	public Slider Raceslidenew;
	bool forraceslidernew;
	public GameObject cargocar1,cargocar2;
	public GameObject rcccamera;
	public GameObject rcccanvas;
	public GameObject traincanvas;
	public GameObject[] Newcargolevels;
	public GameObject envirom;
	public GameObject containerlifterbtn,traincontainerbtn;
	public GameObject pickupcontainblue,pickupcontaingreen;
	public GameObject liftercam;
	public GameObject pausedpanel;
    public GameObject carpanel;
	public GameObject carbutton;
    public GameObject[] trainmodels;
	public int[] levelwisescroe;
    private GoogleMobileAdsDemoScript ads;
    RectangleBannerAd_aa recAds;
	AdaptiveBanner adapAds;
	private bool adoncetime;
	public GameObject rateuspanel;
	private bool IsAlreadyPaused=false;
    public levelsData[] Levels;
    public AudioSource levelfinishaudio;
    public AudioClip finish;
	public AudioSource Musicaudio;
	public GameObject TrafficManagerPlayNotification;
	
	//public AudioSource Musicaudio;
	[System.Serializable]
    public class levelsData
    {
        public string Scenename;
        public int levelno;
        public int nextLevelUnlock;
    }


    void Start ()
	{
        CustomAnalytics.eventMessage("Secondscene" + (levelselectionnumber + 1) + "start");
        Time.timeScale = 1f;
        ads = FindObjectOfType<GoogleMobileAdsDemoScript>();
        if (ads)
        {
            ads.MGGLink();
        }
        recAds = FindObjectOfType<RectangleBannerAd_aa>();
        if (recAds)
        {
         recAds.MGGLink();
        }
		adapAds = FindObjectOfType<AdaptiveBanner>();
		if (adapAds)
		{
		adapAds.MGLink();
		}
		//		trainsimplayer [PlayerPrefs.GetInt ("Trainnum")].SetActive (true);
		//		//		if (SystemInfo.systemMemorySize > 0 && SystemInfo.systemMemorySize <= 2200) 
		//		//		{
		//		//			Cameraplayer.GetComponent<Camera> ().farClipPlane = 300;
		//		//		}
		//		//		else if (SystemInfo.systemMemorySize > 2200 && SystemInfo.systemMemorySize <= 3200) 
		//		//		{
		//		//			Cameraplayer.GetComponent<Camera> ().farClipPlane = 400;
		//		//		}
		//		//		else if (SystemInfo.systemMemorySize > 3200) 
		//		//		{
		//		//			Cameraplayer.GetComponent<Camera> ().farClipPlane = 500;
		//		//		}
		//		//		 Handheld.StopActivityIndicator ();
		//PlayerPrefs.DeleteAll ();
		if (PlayerPrefs.GetInt("Musiconoff") == 0)
		{
			//Musicaudio.volume = 1f;
			Musicaudio.mute = false;			
		}
		if (PlayerPrefs.GetInt("Musiconoff") == 1)
		{
			//Musicaudio.volume = 0f;
			Musicaudio.mute = true;
		}
		Handheld.StopActivityIndicator ();
		IsAlreadyPaused=false;

		if (!PlayerPrefs.HasKey("Unlocklevels")) 
		{
			PlayerPrefs.SetInt ("Unlocklevels", 1);
		}
		AudioListener.volume = 1;
		trainmodels [PlayerPrefs.GetInt ("Trainnum")].SetActive (true);
		scoretext.text = PlayerPrefs.GetInt ("Cashscore").ToString ();
		levelselectionnumber=Menu.Levelnumberspass; 

		if (levelselectionnumber < 6) 
		{
			cameraaon.SetActive (true);
			Locomotive.SetActive (true);
			rcccamera.SetActive (false);
			traincanvas.SetActive (true);
			rcccanvas.SetActive (false);
			containerlifterbtn.SetActive (false);
			traincontainerbtn.SetActive (true);
			liftercam.SetActive (false);
			
		}
        else if (levelselectionnumber < 9) 
		{
			Newcargolevels [levelselectionnumber - 6].SetActive (true);
			Locomotive.SetActive (false);
			rcccamera.SetActive (true);
			traincanvas.SetActive (false);
			rcccanvas.SetActive (true);
			cameraaon.SetActive (false);
			envirom.SetActive (true);
			liftercam.SetActive (false);
			cargocar1 = GameObject.FindGameObjectWithTag ("Cargocarone");
			cargocar2 = GameObject.FindGameObjectWithTag ("Cargocartwo");
			rcccamera.GetComponent<RCC_Camera> ().playerCar = cargocar1.GetComponent<RCC_CarControllerV3>();
			containerlifterbtn.SetActive (false);
			traincontainerbtn.SetActive (true);
			liftercam.SetActive (false);
		} else 
		{
			containerlifterbtn.SetActive (true);
			Newcargolevels [levelselectionnumber - 6].SetActive (true);
			traincanvas.SetActive (true);
			liftercam.SetActive (true);

		}
		cameraaon.GetComponent<SmoothFollow> ().distance = 15f;
		cameraaon.GetComponent<SmoothFollow> ().height = 5f;
		cameraaon.GetComponent<Came> ().distance = 8f;
//		adobj = (GoogleMobileAdsDemoScript)FindObjectOfType (typeof(GoogleMobileAdsDemoScript));
//		#if !UNITY_EDITOR
//		adobj.HideBannerFunc1();
//		adobj.HideBannerFunc();
//		#endif
		levels[levelselectionnumber-1].transform.gameObject.SetActive(true);
		Locomotive.gameObject.transform.position=Locopos[levelselectionnumber-1].transform.position;
		Locomotive.gameObject.transform.rotation=Locopos[levelselectionnumber-1].transform.rotation;
		wagon.gameObject.transform.position=wagonspos[levelselectionnumber-1].transform.position;
		wagon.gameObject.transform.rotation=wagonspos[levelselectionnumber-1].transform.rotation;
		//Thirdpersonchar.transform.position=Thirdpersonpos[levelselectionnumber-1].gameObject.transform.position;
		//Thirdpersonchar.transform.rotation=Thirdpersonpos[levelselectionnumber-1].gameObject.transform.rotation;
		
		Menu.Levelnumbersunlock = Levels[levelselectionnumber].nextLevelUnlock;
        leveltext.text = Menu.Levelnumbersunlock.ToString ();
		//Inputcha = Thirdpersonchar.GetComponent<vThirdPersonController>();
		___Tv= FindObjectOfType<TrainController_v3>();
		___Dmi = FindObjectOfType<DemoUI_v3> ();
		wagon.SetActive (true);
		Cargo1 [levelselectionnumber-1].SetActive (true);
		Cargo2 [levelselectionnumber-1].SetActive (true);
		Cargo3 [levelselectionnumber-1].SetActive (true);
		Cargo4 [levelselectionnumber-1].SetActive (true);
		indictorspeednew (true);
		
	}
	void LateUpdate()
	{
		if(IsAlreadyPaused)
			Time.timeScale = 0;
	}

	public void Thirdpersonoff()
	{
		//thirdpersonpanel.SetActive (true);
		// Thirdpersonchar.SetActive (true);
		//Thirdpersoncam.SetActive(true);
		//traincamera.SetActive (false);
		Starttcolliderdetect [levelselectionnumber-1].SetActive (true);
		FindObjectOfType<DemoUI_v3> ().maxSpeedSlider.value = 0;
		Miniindicator [levelselectionnumber-1].SetActive (false);
		Gmplaybtn.SetActive (false);		
	}

	public void Fadeinfadeout()
	{
		fadeinfadeoutpanel.SetActive (false);
	}
	
	public void fadeloadpanel()
	{
		fadeinfadeoutpanelcargo.SetActive (true);
		rcccamera.GetComponent<RCC_Camera> ().playerCar = null;
		Invoke ("cargopa",2.0f);
        Invoke ("Fadeinfadeoutcargo",2.0f);
	}
	private void cargopa()
	{
		rcccamera.GetComponent<RCC_Camera> ().playerCar = cargocar2.GetComponent<RCC_CarControllerV3>();
		cargocar2.GetComponent<RCC_CarControllerV3> ().enabled = true;
	}
	private void Fadeinfadeoutcargo()
	{
		fadeinfadeoutpanelcargo.SetActive (false);
	}
	public void fadeloadpaneltwo()
	{
		fadeinfadeoutpanelcargo.SetActive (true);

		Invoke ("cargopatwo",2.0f);
		Invoke ("Fadeinfadeoutcargo",2.0f);
	}
	public void fadeloadpanelcontainer()
	{
		fadeinfadeoutpanel.SetActive (true);
		Invoke ("cargopatwocontainer",2.0f);
		Invoke ("Fadeinfadeout",2.0f);
	}
	private void cargopatwo()
	{
		cameraaon.SetActive (true);
		Locomotive.SetActive (true);
		rcccamera.SetActive(false);
		rcccanvas.SetActive(false);
		traincanvas.SetActive(true);
		Newcargolevels [levelselectionnumber - 6].SetActive (false);
		envirom.SetActive (false);

	}
	private void cargopatwocontainer()
	{
		cameraaon.SetActive (true);
		Locomotive.SetActive (true);
		Newcargolevels [levelselectionnumber - 6].SetActive (false);
		containerlifterbtn.SetActive (false);
		traincontainerbtn.SetActive (true);
		Destroy (GameObject.FindGameObjectWithTag ("Maincoll"));
	}
	public void traindetachfunnewoff()
	{
		Locomotive.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		fadeinfadeoutpanel.SetActive (true);
		Debug.Log("TrainONfront");
		FindObjectOfType<TrainController_v3>().ConnectWagons();
		indictorspeednew (true);		
		Invoke ("Fadeinfadeout",1.0f);
	}
	public void Thirdpersonoff1()
	{
		Bokiconnection();
		//thirdpersonpanel.SetActive (false);
		//Thirdpersonchar.SetActive (false);
		//Thirdpersoncam.SetActive(false);
		//traincamera.SetActive(true);
		
		FindObjectOfType<DemoUI_v3>().Forward();
		Gmplaybtn.SetActive(true);
	}
	public void Bokiconnection()
	{
		traindetachfunnewoff();
		cameraaon.GetComponent<SmoothFollow>().distance = 21f;
		cameraaon.GetComponent<SmoothFollow>().height = 7f;
		cameraaon.GetComponent<Came>().distance = 15f;
		Raceslidenew.value = 0;

	}
	public void traindetachfun()
	{
		fadeinfadeoutpanel.SetActive(true);
		Invoke("Thirdpersonoff1", 2.0f);
		Invoke("Fadeinfadeout", 2.0f);
	}
	public void Panelins()
	{
		//Tut.SetActive (true);
		backcolliders[levelselectionnumber-1].SetActive (false);
		Miniindicator[levelselectionnumber-1].SetActive (true);
		Tutpanel2.SetActive (false);
		ok ();
	}
	public void ok()
	{
		Tut.SetActive (false);
		//Revernew (true);
		Locomotive.gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		//FindObjectOfType<DemoUI_v3> ().maxSpeedSlider.value = 0;

	}
	public void pickupcontainerblue(bool buttonpickupbool)
	{
		if (buttonpickupbool == false) 
		{
			pickupcontainblue.SetActive (false);
		} else 
		{
			pickupcontainblue.SetActive (true);
		}

	}
	public void pickupcontainerbutton()
	{
		FindObjectOfType<containterlift> ().pickupcon ();
		FindObjectOfType<Dects> ().pickupbtn ();
		pickupcontainblue.SetActive (false);
	}
	public void dropupcontainerbutton()
	{
		FindObjectOfType<Containerdown> ().Dropupcon ();
		pickupcontaingreen.SetActive (false);
	}
	public void pickupcontainergreen(bool buttonpickup)
	{
		if (buttonpickup == false) 
		{
			pickupcontaingreen.SetActive (false);
		} else 
		{
			pickupcontaingreen.SetActive (true);
		}

	}
	public void Revernew(bool revonff)
	{
		if (revonff == false) 
		{
			Tutpanel4.SetActive (false);
		} 
		else 
		{
			Tutpanel4.SetActive (true);
		}
	}
	public void indictorcamerasec()
	{
		Tutpanel3.SetActive (true);
		Invoke ("Tutoff",2.0f);
	}

	private void Tutoff()
	{
		Tutpanel3.SetActive(false);
		Tutpanel5.SetActive(false);
	}
	public void btnforgameopen (bool gtonoff)
	{
		if (gtonoff) 
		{
			gateopenbtn.SetActive (true);
			
		} else 
		{
			gateopenbtn.SetActive (false);
		}

	}
	public void btnopentraingate()
	{
		FindObjectOfType<Gateon> ().gateonofffun (true);
		gateopenbtn.SetActive (false);
	}


	public void jummpchrater()
	{
		Inputcha.Jump();
	}
	public void cameraswitchnew()
	{

		if (cameraswitch == 0)
        {

			cameraaon.GetComponent<SmoothFollow> ().enabled = true;
			cameraaon.GetComponent<Came> ().enabled = false;
			FindObjectOfType<Tankertrainman> ().resetcame ();
			inputtouchcampanel.SetActive (false);
			cameraaon.GetComponent<SmoothFollow> ().distance = 25f;
			cameraaon.GetComponent<SmoothFollow> ().height = 8f;
			cameraswitch = 1;
		}

        else if (cameraswitch == 1)
        {
			cameraaon.GetComponent<SmoothFollow> ().enabled = false;
			cameraaon.GetComponent<Came> ().enabled = true;
			cameraaon.GetComponent<Came> ().enabled = true;
			FindObjectOfType<Tankertrainman> ().resetcame ();
			inputtouchcampanel.SetActive (true);
			cameraswitch = 2;
		}

        else if (cameraswitch == 2)
        {
			cameraswitch = 0;
			cameraaon.GetComponent<SmoothFollow> ().enabled = true;
			cameraaon.GetComponent<Came> ().enabled = false;
			inputtouchcampanel.SetActive (false);
			cameraaon.GetComponent<SmoothFollow> ().distance = 0.1f;
			cameraaon.GetComponent<SmoothFollow> ().height = 0.0f;
			FindObjectOfType<Tankertrainman> ().frontcam ();
		}
	}

	public void GameplayController(string Gameplaybtn)
	{
		switch(Gameplaybtn)
		{
		case "Home":		
		    Handheld.StartActivityIndicator ();
			Time.timeScale = 1;
            //hideBanner();
				hideadapBanner();
            hideRecBanner();
            showInterstitial();
			gameplaypanel [3].SetActive (true);  //for loading
			IsAlreadyPaused =false;
			PlayerPrefs.SetInt ("IsNext", 0);
			SceneManager.LoadScene(1);		
			break;
		case "Carpanels": 
			carpanel.SetActive (false);
			carbutton.SetActive (true);
			break; 
		case "Restart": 
			Time.timeScale = 1;
            hideRecBanner();
            //hideBanner();
			hideadapBanner();
			Handheld.StartActivityIndicator ();
			IsAlreadyPaused =false;
			AudioListener.volume = 1;
			gameplaypanel [3].SetActive (true);  //for loading
			SceneManager.LoadScene(3);
			break; 
		case "Resume":
            //hideBanner();
			hideadapBanner();
            hideRecBanner();
     
			if (Raceslidenew.value > 0)
			{
			Time.timeScale = 2f;
			speed1xbtn.SetActive(true);
			speed2xbtn.SetActive(false);
			}
			else
			{
			Time.timeScale = 1;
			}
			gameplaypanel [2].SetActive (false);   //for unpause
			AudioListener.volume = 1;
			IsAlreadyPaused =false;
			break;
		case "Resumecar":
			//hideBanner();
			hideadapBanner();
            hideRecBanner();  
				if (Raceslidenew.value > 0)
				{
					Time.timeScale = 2f;
					speed1xbtn.SetActive(true);
					speed2xbtn.SetActive(false);
				}
				else
				{
					Time.timeScale = 1;
				}
				AudioListener.volume = 1;
			pausedpanel.SetActive (false);
			IsAlreadyPaused =false;
			if (PlayerPrefs.GetInt ("Musiconff") == 0) 
			{

				//mainaudiosource.mute = false;

			} else 
			{
				//mainaudiosource.mute = true;
			}
//			#if !UNITY_EDITOR
//			if(adobj!=null)
//			{
//			adobj.HideBannerFunc1();
//			adobj.HideBannerFunc();
//			}
//			#endif
			break;
		case "Rateus": 
			PlayerPrefs.SetInt ("oncetimeshow", 1);
			Application.OpenURL ("market://details?id=com.bettergames.euro.train.driver.sim");
			rateuspanel.SetActive (false);
			break;
		case "Laternow": 
			rateuspanel.SetActive (false);
			break;
		case "Paused":
            //showBanner();
				showAdapbanner();
            showRecBanner();
			Time.timeScale = 0;
			adoncetime = true;
			AudioListener.volume = 0;
			IsAlreadyPaused = true;
                
			PlayerPrefs.SetInt ("pausecounter", PlayerPrefs.GetInt ("pausecounter") + 1);
			gameplaypanel [2].SetActive (true);  //for pause
			//mainaudiosource.mute = true;
			break;
		case "Pausedcar":
           // showBanner();
				showAdapbanner();
            showRecBanner();
            Time.timeScale = 0;
			AudioListener.volume = 0;
			adoncetime = true;
			IsAlreadyPaused = true;
//			#if !UNITY_EDITOR
//			if(adobj!=null)
//			{
//			adobj.ShowBannerFunc1 ();
//			adobj.ShowBannerFunc();
//			}
//			#endif
			pausedpanel.SetActive(true);
			break;
		case "Next":
		    //levelcomplete();
			Time.timeScale = 1;       
            hideRecBanner();
            //hideBanner();
			hideadapBanner();
            showInterstitial();
			Handheld.StartActivityIndicator ();
			gameplaypanel [3].SetActive (true);  //for loading
			IsAlreadyPaused =false;
            PlayerPrefs.SetInt ("IsNext", 1);
             Menu.Levelnumberspass = Levels[levelselectionnumber].levelno;
			 //PlayerPrefs.SetInt("currentlevel", Levels[levelselectionnumber].levelno); 
             SceneManager.LoadScene(Levels[levelselectionnumber].Scenename);
             break;

		}

	}
	void OnApplicationPause (bool isPause)
	{
		if (isPause) { // App going to background
			Time.timeScale = 0;
		} else {
			if(IsAlreadyPaused)
				Time.timeScale = 0;
			else
				Time.timeScale = 1;
		}
	}

	
	void Update () 
	{
		if (!forraceslidernew) 
		{

			if (Raceslidenew.value > 1) 
			{		
				indictorspeednew (false);
			} 
		}

		if (!oncetimecargo) 
		{
			cargocar1 = GameObject.FindGameObjectWithTag ("Cargocarone");
			cargocar2 = GameObject.FindGameObjectWithTag ("Cargocartwo");
			oncetimecargo = true;
		}


	}


	public void indictorbrakenew(bool braketutonff)
	{
		if (braketutonff == false) 
		{
			Tutpanel2.SetActive (false);
		} 
		else 
		{
			Tutpanel2.SetActive (true);
		}

	}
	
    public void speedinicatorReverseON()
    {
        Tutpanel6.SetActive(true);
		Forwardbtn.SetActive(false);
    }

    public void speedinicatorReverseOFF()
    {
        Tutpanel6.SetActive(false);
    }

    public void indictorspeednew(bool speedtutonff)
	{
		if (speedtutonff == false) 
		{
			Tutpanel1.SetActive (false);
		} 
		else 
		{
			Tutpanel1.SetActive (true);
			forraceslidernew = false;
		}
	}
	

	public GameObject speed1xbtn, speed2xbtn;
	public void increaseSpeedfunc()
	{
      if (Raceslidenew.value > 0)
        {
            Time.timeScale = 2f;
            speed1xbtn.SetActive(true);
            speed2xbtn.SetActive(false);
        }
    }
	public void decreaseSpeedfunc()
    {
			Time.timeScale = 1f;
			speed1xbtn.SetActive(false);
			speed2xbtn.SetActive(true);	
	}
	public void SliderSpeedbtnfunc()
	{
		if (Raceslidenew.value == 0)
		{
			Time.timeScale = 1;
			speed1xbtn.SetActive(false);
			speed2xbtn.SetActive(true);
		}
		else
		{
			speed2xbtn.SetActive(true);
		}
	}
	public void TMfunc(int levelnums)
    {
		TrafficManagerPlayNotification.SetActive(false);
		gameplaypanel[3].SetActive(true);
		Menu.Levelnumberstraffic = levelnums;
		PlayerPrefs.SetInt("currentlevel", levelnums);
		SceneManager.LoadScene("Thirdscene");
		AudioListener.volume = 1;
		hideRecBanner();
		//hideBanner();
		hideadapBanner();
	}

	public void lvlcomp()
	{        
        Invoke ("levelcomplete",1.0f);
		levelfinishaudio.PlayOneShot(finish, 3.0F);	
	}

	public void TMplaybackbtn()
	{
		TrafficManagerPlayNotification.SetActive(false);
		//showRecBanner();
	}
	public void levelcomplete()
	{
		gameplaypanel[0].SetActive(true);
		//levelcomplelpanel.SetActive (false);
		if (PlayerPrefs.GetInt("Unlocklevels") == 3)
		{
			TrafficManagerPlayNotification.SetActive(true);
			//hideRecBanner();
		}
		
		//showBanner();
		showAdapbanner();
		showRecBanner();
		//PlayerPrefs.SetInt("Tick"+Menu.Levelnumbersunlock.ToString(),1);
		CustomAnalytics.eventMessage("Secondscene" + (levelselectionnumber + 1) + "_complete");
		if (Menu.Levelnumbersunlock == 15 )
		{	
			Nextbtn.SetActive(false);
		}
		//isnotwin = true;
		//mainaudiosource.mute = true;
		//Menu.Levelnumbersunlock = Levels[levelselectionnumber].nextLevelUnlock;

		if (Menu.Levelnumbersunlock == PlayerPrefs.GetInt("Unlocklevels"))
		{
			if (PlayerPrefs.GetInt("Unlocklevels") < 15)
			{
				PlayerPrefs.SetInt("Unlocklevels", PlayerPrefs.GetInt("Unlocklevels") + 1);
			}
		}

		AudioListener.volume = 0;

		adoncetime = false;
		PlayerPrefs.SetInt ("Cashscore",PlayerPrefs.GetInt("Cashscore")+levelwisescroe[levelselectionnumber-1]);//work on coin text
		//scoretext3.text = PlayerPrefs.GetInt ("Cashscore").ToString ();
		PlayerPrefs.SetInt ("Rateus",PlayerPrefs.GetInt("Rateus")+1);
		Debug.Log("RateUS =" + PlayerPrefs.GetInt("Rateus"));
		//if (PlayerPrefs.GetInt ("oncetimeshow") == 2) 
		//{
			Debug.Log("RateUS");
			if (PlayerPrefs.GetInt ("Rateus")==2||PlayerPrefs.GetInt ("Rateus")==6||PlayerPrefs.GetInt ("Rateus")==9||PlayerPrefs.GetInt ("Rateus")==12||PlayerPrefs.GetInt ("Rateus")==15 || PlayerPrefs.GetInt("Rateus") == 18 || PlayerPrefs.GetInt("Rateus") == 20)
			{
				rateuspanel.SetActive (true);
			}
            
       // }


		Time.timeScale = 0;
	}
	public void levelFailed()
	{
        //		if (PlayerPrefs.GetInt ("Failed"+levelselectionnumber) == 0) {
        //			if (MenuManager.Careerstring == "Carrermode")
        //			{
        //				CustomAnalytics.eventMessage ("Failed_Career_1stAttempt" + levelselectionnumber);
        //				PlayerPrefs.SetInt ("Failed"+levelselectionnumber, 1);
        //			}
        //		} else 
        //		{
        //			if (MenuManager.Careerstring == "Carrermode") {
        //				CustomAnalytics.eventMessage ("Failed_Career_" + levelselectionnumber);
        //			}
        //		}
   
        AudioListener.volume = 0;
		adoncetime = false;
		Invoke ("levelfaileddelay",2.5f);
        CustomAnalytics.eventMessage("Secondscene" + (levelselectionnumber + 1) + "_fail");
    }

	public void levelfaileddelay()
	{	
		print ("Levelsfaild");
		gameplaypanel [1].SetActive (true);
		showRecBanner();
		//showBanner();
		showAdapbanner();
	}
		
	void OnDisable()
	{
		if (IsInvoking ("levelfaileddelay"))
			CancelInvoke ("levelfaileddelay");
		if (IsInvoking ("levelcomplete"))
			CancelInvoke ("levelcomplete");
		if (IsInvoking ("Fadeinfadeout"))
			CancelInvoke ("Fadeinfadeout");
		if (IsInvoking ("Thirdpersonoff1"))
			CancelInvoke ("Thirdpersonoff1");
		if (IsInvoking ("Thirdpersonoff"))
			CancelInvoke ("Thirdpersonoff");
	}

	public void scorecollect2 ()
	{
		PlayerPrefs.SetInt ("Cashscore", PlayerPrefs.GetInt ("Cashscore") + 100);
		scoretext.text = PlayerPrefs.GetInt ("Cashscore").ToString ();
	}

    void showInterstitial()
    {
		if (ads)
			ads.ShowInterstitial();
    }
	void showBanner()
	{
		//if (ads)
			//ads.ShowBannerFunc();
	}
	void hideBanner()
	{
		//if (ads)
			//ads.HideBannerFunc();
	}
	void showRecBanner()
	{
		if (recAds)
			recAds.ShowBanner();

	}
	void hideRecBanner()
	{
		if (recAds)
			recAds.HideBanner();

	}
	void ShowMenuRectAds()
	{
		if (recAds)
			recAds.RequestBanner();
		//recAds.ShowBanner();
	}
	//void ShowMenubannerAds()
	//{
	//	if (ads)
	//		ads.RequestBanner();
	//	// ads.ShowBannerFunc();
	//}
	void showAdapbanner()
	{
		if (adapAds)
			adapAds.ShowBanner();
	}

	void hideadapBanner()
	{
		if (adapAds)
			adapAds.HideBanner();
	}
}
