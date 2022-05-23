using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
public class Menu : MonoBehaviour
{

	public GameObject[] Menupanels;
	public GameObject[] Cameratrain;
	public GameObject[] Specifaction;
	public GameObject[] Texturebtns;
	//public GameObject Levels;
	public int[] Trainsprice;
	private int previousnextcounter;
	private int previousnextcounter1;
	public Text showcarprice;
	public GameObject priceimage;
	public GameObject buybtn;
	public GameObject Purchsedimage;
	public GameObject Soundbtnon, Soundbtnoff;
	public GameObject Musicbtnon, Musicbtnoff;
	public AudioSource Menuaudio;
	public AudioSource Musicaudio;
	public AudioClip impact;
	public GameObject[] Qaulityimages;
	public GameObject[] Mobileguiimages;
	public GameObject levelbtns;
	public Text Scores1, Scores2, Scores3, Scores4, Scores5, Scores6, Scores7;
	public static bool gotomenu;
	public GameObject enoughcoinpanels;
	public string[] Unlockcarsstring;
	public GameObject Cameralerp;
	public static int Levelnumberspass, Levelnumberscargo, Levelnumbersunlock, Levelnumberstraffic, Levelnumbertrafficunlock, LevelnumbersRace, LevelnumberRaceunlock;
	//public GameObject[] Cargoleveltick;
	public GameObject[] Unlocklevels, lockimage;
	public GameObject[] UnlockTrafficlevels, lockimageStation;
	public GameObject[] UnlockRacelevels, lockimageRace;
	//public GameObject[] UnlockTrafficlevel;
	// public GameObject[] Trafficleveltick;
	private GoogleMobileAdsDemoScript ads;
	RectangleBannerAd_aa recAds;
	AdaptiveBanner adapads;
	public static bool isonce;
	public INAPP Inappscripts;
	public GameObject Inappbackfrommenu, Inappbackfromother;
	//public GameObject unlocklevelarrow, unlocktrainarrow;
	public GameObject menuobj, levelobj, gargeobj;
	public GameObject RaceModegauragetolevelbtn, passengertogaurage, ModifyCareer, modifytoRace;
	public TrainModify[] ModifyEngine, ModifyBuggie1;
	public GameObject TrafficManagerbtn, TrafficManagerLockbtn, RaceModebtn, RaceModeLockbtn;
	public INAPP inapp;
	private static bool requestonetime = true;
	public Animator camAnim;
	BannerAd_Puzzle Banpuzzle;
	// Use this for initialization
	void Start()
	{

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		MMLinkfunc();	
		inapp = GameObject.FindObjectOfType<INAPP>();
		inapp.LinkMenuManager();
		Time.timeScale = 1f;
		Banpuzzle = GetComponent<BannerAd_Puzzle>();
		
		//previousnextcounter = 2;
		//PlayerPrefs.DeleteAll();
		Handheld.StopActivityIndicator();
		Specifaction[previousnextcounter1].SetActive(true);
		Texturebtns[previousnextcounter1].SetActive(true);
		AudioListener.volume = 1;
		QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QuailitySetting"));
		Qaulityimages[PlayerPrefs.GetInt("QuailitySetting")].SetActive(true);
		PlayerPrefs.SetInt("Buycar0", 1);
		Scores1.text = PlayerPrefs.GetInt("Cashscore").ToString();
		Scores2.text = PlayerPrefs.GetInt("Cashscore").ToString();
		Scores3.text = PlayerPrefs.GetInt("Cashscore").ToString();
		Scores4.text = PlayerPrefs.GetInt("Cashscore").ToString();
		Scores5.text = PlayerPrefs.GetInt("Cashscore").ToString();
		Scores6.text = PlayerPrefs.GetInt("Cashscore").ToString();
		Scores7.text = PlayerPrefs.GetInt("Cashscore").ToString();
		if (PlayerPrefs.GetInt("Soundonoff") == 0)
		{
			Menuaudio.mute = false;
			Soundbtnon.SetActive(true);
			Soundbtnoff.SetActive(false);
		}
		if (PlayerPrefs.GetInt("Soundonoff") == 1)
		{
			Menuaudio.mute = true;
			Soundbtnon.SetActive(false);
			Soundbtnoff.SetActive(true);
		}
		if (PlayerPrefs.GetInt("Musiconoff") == 0)
		{
			Musicaudio.mute = false;
			Musicbtnon.SetActive(true);
			Musicbtnoff.SetActive(false);

		}
		if (PlayerPrefs.GetInt("Musiconoff") == 1)
		{
			Musicaudio.mute = true;
			Musicbtnon.SetActive(false);
			Musicbtnoff.SetActive(true);
		}
		if (PlayerPrefs.GetInt("IsNext") == 1)
		{
			Menupanels[1].SetActive(true);
			PlayerPrefs.SetInt("IsNext", 0);
		} else
		{
			Menupanels[0].SetActive(true);
		}

		if (PlayerPrefs.GetInt("Unlocklevels") > 3)
		{
			TrafficManagerLockbtn.SetActive(false);
			TrafficManagerbtn.SetActive(true);
		}

		for (int i = 0; i < PlayerPrefs.GetInt("Unlocklevels"); i++)
		{
			Unlocklevels[i].GetComponent<Button>().interactable = true;
			lockimage[i].SetActive(false);
		}


		if (PlayerPrefs.GetInt("UnlockTrafficlevels") > 4)
		{
			RaceModebtn.SetActive(true);
			RaceModeLockbtn.SetActive(false);
		}

		for (int i = 0; i <= PlayerPrefs.GetInt("UnlockTrafficlevels"); i++)
		{
			//print("PlayerPrefs.GetInt(UnlockTrafficlevels): " + PlayerPrefs.GetInt("UnlockTrafficlevels"));
			UnlockTrafficlevels[i].GetComponent<Button>().interactable = true;
			lockimageStation[i].SetActive(false);

		}

		for (int i = 0; i <= PlayerPrefs.GetInt("UnlockRacelevels"); i++)
		{
			UnlockRacelevels[i].GetComponent<Button>().interactable = true;
			lockimageRace[i].SetActive(false);

		}


		//adobj = (GoogleMobileAdsDemoScript)FindObjectOfType (typeof(GoogleMobileAdsDemoScript));
		if (PlayerPrefs.GetInt("Unlol") == 1)
		{
			//unlocklevelarrow.SetActive (false);
			ModeAlllevelunlockbtn.SetActive(false);
			CareerAlllevelunlockbtn.SetActive(false);
			TMAlllevelunlockbtn.SetActive(false);
			RaceAlllevelunlockbtn.SetActive(false);
		}
		if (PlayerPrefs.GetInt("Unlot") == 1)
		{
			//unlocktrainarrow.SetActive (false);
			GarageAllTrainsUnlock.SetActive(false);
		}
		if (PlayerPrefs.GetInt("Unloc") == 1)
		{
			// unlocktrainarrow.SetActive(false);
		}

		ModifyEngine[previousnextcounter1].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + previousnextcounter1 + "texture"));
		ModifyBuggie1[previousnextcounter1].GetComponent<TrainModify>().setMat(PlayerPrefs.GetInt("train" + previousnextcounter1 + "texture"));

		
	}

	// Update is called once per frame
	void Update()
	{
		Cameralerp.transform.position = Vector3.Lerp(Cameralerp.transform.position, Cameratrain[previousnextcounter1].transform.position, Time.deltaTime * 2.0f);
		Cameralerp.transform.rotation = Quaternion.Slerp(Cameralerp.transform.rotation, Cameratrain[previousnextcounter1].transform.rotation, Time.deltaTime * 2.0f);
	}

	public void MMLinkfunc()
	{
		recAds = FindObjectOfType<RectangleBannerAd_aa>();
		ads = FindObjectOfType<GoogleMobileAdsDemoScript>();
		
		if (ads)
		{
			ads.MMLink();
		}
		if (recAds)
		{
			recAds.MMLink();
		}
		if (Banpuzzle)
		{
			Banpuzzle.MGLink();
			Banpuzzle.MMLink();
		}
		if (requestonetime)
		{
			ShowMenuRectAds();
			ShowMenubannerAds();			
			requestonetime = false;
		}
		else
		{
			Debug.Log("MenuBanner");
			showBanner();
			showRecBanner();
			//hideBannerPuzzle();
		}
		//hideBannerPuzzle();
	}

	//public void AMLinkfunc()
	//   {
	//	adapAds = FindObjectOfType<AdaptiveBanner>();
	//       if(adapAds)
	//       {
	//		adapAds.MMLink();
	//		print("ADAPTAdsbanner");
	//       }
	//	if (requestonetime)
	//	{
	//		ShowMenuAdapAds();
	//		requestonetime = false;
	//	}
	//	else
	//       {
	//		showmenuadapfun();
	//       }
	//}
	public void SelectLevel(int no)
	{
		Menuaudio.PlayOneShot(impact, 0.7F);
		Menupanels[2].SetActive(false);
		Menupanels[4].SetActive(true);
		levelobj.SetActive(false);
		PlayerPrefs.SetInt("currentlevel", no);
	}

	public void loadScene(string name)
	{
		Handheld.StartActivityIndicator();
		SceneManager.LoadScene(name);
	}
	public void levelselecpassanger(int levelnums)
	{

		Menuaudio.PlayOneShot(impact, 0.7F);
		Levelnumberspass = levelnums;
		Menupanels[2].SetActive(false);
		Menupanels[4].SetActive(true);
		showInterstitial();
		hideBanner();
		SceneManager.LoadScene("SampleScene");
		Handheld.StartActivityIndicator();
		PlayerPrefs.SetInt("Levek", 1);
		levelobj.SetActive(false);
	}
	public void levelunlock(int levelnums)
	{
		Levelnumbersunlock = levelnums;
	}

	public void levelseleccargo(int levelnums)
	{
		Menuaudio.PlayOneShot(impact, 0.7F);
		Levelnumberspass = levelnums;
		Menupanels[2].SetActive(false);
		Menupanels[4].SetActive(true);
		showInterstitial();
		hideBanner();
		SceneManager.LoadScene("Secondscene");
		Handheld.StartActivityIndicator();
		PlayerPrefs.SetInt("Levek", 1);
		levelobj.SetActive(false);
	}

	public void levelunlockTraffic(int levelnums)
	{
		Levelnumbertrafficunlock = levelnums;
	}

	public void levelselectraffic(int levelnums)
	{
		Menuaudio.PlayOneShot(impact, 0.7F);
		Levelnumberstraffic = levelnums;
		Menupanels[7].SetActive(false);
		Menupanels[4].SetActive(true);
		Handheld.StartActivityIndicator();
		PlayerPrefs.SetInt("currentlevel", levelnums);
		levelobj.SetActive(false);
		showInterstitial();
		hideBanner();
		SceneManager.LoadScene("Thirdscene");
	}

	public void levelunlockRace(int levelnums)
	{
		LevelnumberRaceunlock = levelnums;
	}

	public void levelselectRace(int levelnums)
	{
		Menuaudio.PlayOneShot(impact, 0.7F);
		LevelnumbersRace = levelnums;
		Menupanels[9].SetActive(false);
		Menupanels[4].SetActive(true);
		Handheld.StartActivityIndicator();
		PlayerPrefs.SetInt("currentlevel", levelnums);
		levelobj.SetActive(false);

		showInterstitial();
		hideBanner();
		SceneManager.LoadScene("TrainRacescene");
	}

	public void prevcar()
	{
		if (previousnextcounter1 > 0)
		{
			previousnextcounter1 -= 1;
			Menuaudio.PlayOneShot(impact, 0.7F);
		}
		Specifaction[previousnextcounter1 + 1].SetActive(false);
		Texturebtns[previousnextcounter1 + 1].SetActive(false);
		Specifaction[previousnextcounter1].SetActive(true);
		Texturebtns[previousnextcounter1].SetActive(true);
		showcarprice.text = Trainsprice[previousnextcounter1].ToString();
		buybtnshow();
	}
	public void nextcar()
	{
		if (previousnextcounter1 < 3)
		{
			previousnextcounter1 += 1;
			Menuaudio.PlayOneShot(impact, 0.7F);
		}
		Specifaction[previousnextcounter1 - 1].SetActive(false);
		Texturebtns[previousnextcounter1 - 1].SetActive(false);
		Specifaction[previousnextcounter1].SetActive(true);
		Texturebtns[previousnextcounter1].SetActive(true);
		showcarprice.text = Trainsprice[previousnextcounter1].ToString();
		//Cameralerp.GetComponent<Animator>().enabled = false;
		buybtnshow();
	}


	public void buybtnshow()
	{

		if (PlayerPrefs.GetInt("Buycar" + previousnextcounter1.ToString()) == 1)
		{
			priceimage.SetActive(false);
			buybtn.SetActive(false);
			Purchsedimage.SetActive(true);
			ModifyCareer.SetActive(true);
		}
		else
		{
			priceimage.SetActive(true);
			buybtn.SetActive(true);
			ModifyCareer.SetActive(false);
			Purchsedimage.SetActive(false);
		}
	}

	public void buycarbtn()
	{
		Menuaudio.PlayOneShot(impact, 0.7F);
		if (PlayerPrefs.GetInt("Cashscore") >= Trainsprice[previousnextcounter1])
		{
			PlayerPrefs.SetInt("Cashscore", PlayerPrefs.GetInt("Cashscore") - Trainsprice[previousnextcounter1]);
			PlayerPrefs.SetInt("Buycar" + previousnextcounter1.ToString(), 1);
			buybtn.SetActive(false);
			priceimage.SetActive(false);
			ModifyCareer.SetActive(true);
			Purchsedimage.SetActive(true);
			Scores1.text = PlayerPrefs.GetInt("Cashscore").ToString();
			Scores2.text = PlayerPrefs.GetInt("Cashscore").ToString();
			Scores3.text = PlayerPrefs.GetInt("Cashscore").ToString();
			Scores4.text = PlayerPrefs.GetInt("Cashscore").ToString();
			Scores5.text = PlayerPrefs.GetInt("Cashscore").ToString();
			Scores6.text = PlayerPrefs.GetInt("Cashscore").ToString();
			Scores7.text = PlayerPrefs.GetInt("Cashscore").ToString();
		}
		else
		{
			enoughcoinpanels.SetActive(true);
			Invoke("Enoughpanelss", 3.0f);
			ModifyCareer.SetActive(false);
		}

	}
	public void Usebtn()
	{
		if (PlayerPrefs.GetInt("SelectedMode") == 0)
		{
			Menuaudio.PlayOneShot(impact, 0.7F);

			PlayerPrefs.SetInt("Trainnum", previousnextcounter1);
			Menupanels[2].SetActive(false);
			Menupanels[1].SetActive(true);
			PlayerPrefs.SetInt("Garge", 1);
			gargeobj.SetActive(false);
		}
		else
		{
			PlayerPrefs.SetInt("Trainnum", previousnextcounter1);
			Menupanels[8].SetActive(false);
			Menupanels[9].SetActive(true);
			Menuaudio.PlayOneShot(impact, 0.7F);
			//showBanner();
			hideRecBanner();
		}
	}
	public void Mainmenubtn(string Buttonlabel)
	{

		switch (Buttonlabel)
		{
			case "Gotolevels":
				PlayerPrefs.SetInt("SelectedMode", 0);
				hideRecBanner();
				CustomAnalytics.eventMessage("CareerMode");
				Menupanels[8].SetActive(false);
				Menupanels[2].SetActive(true);
				Menupanels[10].SetActive(false);
				RaceModegauragetolevelbtn.SetActive(false);
				passengertogaurage.SetActive(true);
				Menuaudio.PlayOneShot(impact, 0.7F);
				PlayerPrefs.SetInt("Menu", 1);
				menuobj.SetActive(false);
				//ModifyCareer.SetActive(true);
				//modifytoRace.SetActive(false);
				//			#if !UNITY_EDITOR
				//			adobj.HideBannerFunc1 ();
				//			#endif
				break;
			case "Backgaragetolevel":
				if (PlayerPrefs.GetInt("SelectedMode") == 0)
				{
					Menupanels[1].SetActive(false);
					Menupanels[10].SetActive(false);
					Menupanels[2].SetActive(true);
					Menuaudio.PlayOneShot(impact, 0.7F);
					ModifyCareer.SetActive(true);
					//modifytoRace.SetActive(false);
					passengertogaurage.SetActive(true);
				}
				else
				{
					PlayerPrefs.SetInt("SelectedMode", 1);
					Menupanels[8].SetActive(false);
					Menupanels[2].SetActive(true);
					Menupanels[10].SetActive(false);
					passengertogaurage.SetActive(true);
					Menuaudio.PlayOneShot(impact, 0.7F);
					//modifytoRace.SetActive(true);
					ModifyCareer.SetActive(true);
					//RaceModegauragetolevelbtn.SetActive(true);
					//showBanner();
					hideRecBanner();
				}
				break;
			case "BackMenutoMode":
				Menupanels[8].SetActive(false);
				Menupanels[0].SetActive(true);
				showRecBanner();
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "YesQuit":
				Menuaudio.PlayOneShot(impact, 0.7F);
				if (Application.platform == RuntimePlatform.Android) {
					new AndroidJavaClass("java.lang.System").CallStatic("exit", 0);
				} else {
					Application.Quit();
				}
				break;
			case "Quit":
				Menuaudio.PlayOneShot(impact, 0.7F);
				Menupanels[0].SetActive(false);
				Menupanels[3].SetActive(true);
				showinterstitialfunc();
				break;
			case "crossinapp":
				Menuaudio.PlayOneShot(impact, 0.7F);
				Menupanels[6].SetActive(false);
				hideRecBanner();
				break;
			case "crossinappMenu":
				Menuaudio.PlayOneShot(impact, 0.7F);
				Menupanels[6].SetActive(false);
				showRecBanner();
				//showBanner();
				break;
			case "menuinapp":
				hideRecBanner();
				Menuaudio.PlayOneShot(impact, 0.7F);
				Menupanels[6].SetActive(true);
				Inappbackfrommenu.SetActive(true);
				Inappbackfromother.SetActive(false);
				Scores1.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores2.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores3.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores4.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores5.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores6.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores7.text = PlayerPrefs.GetInt("Cashscore").ToString();
				break;
			case "Inaap":
				Menuaudio.PlayOneShot(impact, 0.7F);
				Menupanels[6].SetActive(true);
				Inappbackfromother.SetActive(true);
				Inappbackfrommenu.SetActive(false);
				hideRecBanner();
				Scores1.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores2.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores3.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores4.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores5.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores6.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores7.text = PlayerPrefs.GetInt("Cashscore").ToString();
				//			#if !UNITY_EDITOR
				//			adobj.HideBannerFunc1 ();
				//			#endif
				break;
			case "Gotomenu":
				Menupanels[2].SetActive(false);
				Menupanels[0].SetActive(true);
				Menupanels[5].SetActive(false);
				Menupanels[3].SetActive(false);
				Menuaudio.PlayOneShot(impact, 0.7F);
				//showBanner();
				showRecBanner();
				break;
			case "Gotomenufromsettings":
				Menupanels[2].SetActive(false);
				Menupanels[0].SetActive(true);
				Menupanels[5].SetActive(false);
				Menupanels[3].SetActive(false);
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "Gargetomode":
				Menupanels[2].SetActive(false);
				Menupanels[8].SetActive(true);
				Menuaudio.PlayOneShot(impact, 0.7F);
				hideRecBanner();
				//			#if !UNITY_EDITOR
				//			adobj.ShowBannerFunc1 ();
				//			#endif
				break;
			case "GotoSettings":
				//showBanner();
				//showRecBanner();
				Menupanels[0].SetActive(false);
				Menupanels[5].SetActive(true);
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "Rateus":
				Application.OpenURL("market://details?id=com.bettergames.euro.train.driver.sim");
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "More":
				Application.OpenURL("https://play.google.com/store/apps/developer?id=Better+Games+Studio+Pty+Ltd");
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "TrafficManager":
				CustomAnalytics.eventMessage("TrafficModeClick");
				Menupanels[8].SetActive(false);
				Menupanels[7].SetActive(true);
				Menuaudio.PlayOneShot(impact, 0.7F);
				hideRecBanner();
				break;
			case "ModeSeletion":
				Menupanels[0].SetActive(false);
				Menupanels[8].SetActive(true);
				Menuaudio.PlayOneShot(impact, 0.7F);
				hideRecBanner();
				break;
			case "TrafficManagertoModeselection":
				Menupanels[7].SetActive(false);
				Menupanels[8].SetActive(true);
				Menuaudio.PlayOneShot(impact, 0.7F);
				hideRecBanner();
				break;
			case "TrainModify":
				Menupanels[10].SetActive(true);
				Menupanels[2].SetActive(false);
				//RaceModegauragetolevelbtn.SetActive(true);
				//passengertogaurage.SetActive(false);
				Menuaudio.PlayOneShot(impact, 0.7F);
				//showBanner();
				hideRecBanner();
				break;

			case "TrainRaceGaurage":
				PlayerPrefs.SetInt("SelectedMode", 1);
				Menupanels[8].SetActive(false);
				Menupanels[2].SetActive(true);
				Menupanels[10].SetActive(false);
				passengertogaurage.SetActive(true);
				Menuaudio.PlayOneShot(impact, 0.7F);
				// ModifyCareer.SetActive(true);
				// passengertogaurage.SetActive(true);             
				// showBanner();
				hideRecBanner();
				break;
			case "TrainRaceGleveltogaurage":
				Menupanels[9].SetActive(false);
				Menupanels[2].SetActive(true);
				// RaceModegauragetolevelbtn.SetActive(true);
				passengertogaurage.SetActive(true);
				Menuaudio.PlayOneShot(impact, 0.7F);
				hideRecBanner();
				break;
		}
	}

	public void PuzzleMode()
	{
		Invoke("Puzzlemodefunc", 2f);
		Screen.orientation = ScreenOrientation.Portrait;
		CustomAnalytics.eventMessage("TrainPuzzleMode");
		hideBanner();
		ads.GetComponent<GoogleMobileAdsDemoScript>().enabled = false;
		//recAds.GetComponent<RectangleBannerAd_aa>().enabled = false;
		//adapads.GetComponent<AdaptiveBanner>().enabled = false;
	}

	public void Puzzlemodefunc()
	{
		SceneManager.LoadScene(7);
		
	}
	void OnApplicationPause(bool isPause)
	{
		if (isPause) { // App going to background
			Time.timeScale = 0;
		}
		else
			Time.timeScale = 1;
	}

	public void adsbtn(string Adslabel)
	{
		switch (Adslabel) {
			case "Fiftykcoins":
				Inappscripts.BuyfiftyKCoinsKCoins();
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "onelaccoins":
				Inappscripts.BuyCoinsHunderdKCoins();
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "Trainone":
				Inappscripts.Unlockonetrain();
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "Traintwo":
				Inappscripts.Unlocktwotrain();
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "Trainthree":
				Inappscripts.Unlockthreetrain();
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "Trainpack":
				Inappscripts.UnlockTrucks();
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "Removeads":
				Inappscripts.Removeadss();
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
			case "Unlocklevels":
				Inappscripts.Unlocklevels();
				Menuaudio.PlayOneShot(impact, 0.7F);
				break;
				//     case "Unlocktrains":
				//Inappscripts.UnlockTrucks ();
				//         Menuaudio.PlayOneShot(impact, 0.7F);
				//break;      
				/*case "UnlockTrafficlevels":
					 Inappscripts.UnlockTrafficlevels ();
					 Menuaudio.PlayOneShot(impact, 0.7F);
					 break;*/
		}
	}
	public GameObject ModeAlllevelunlockbtn;
	public GameObject CareerAlllevelunlockbtn;
	public GameObject TMAlllevelunlockbtn;
	public GameObject RaceAlllevelunlockbtn;
	public GameObject GarageAllTrainsUnlock;


	public void inappconbtn(string inappcon)
	{

		switch (inappcon)
		{
			case "Coinpackone":
				Scores1.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores2.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores3.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores4.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores5.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores6.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores7.text = PlayerPrefs.GetInt("Cashscore").ToString();
				break;
			case "Coinpacktwo":
				Scores1.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores2.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores3.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores4.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores5.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores6.text = PlayerPrefs.GetInt("Cashscore").ToString();
				Scores7.text = PlayerPrefs.GetInt("Cashscore").ToString();
				break;
			case "Unlocklevels":

				TrafficManagerLockbtn.SetActive(false);
				TrafficManagerbtn.SetActive(true);
				RaceModebtn.SetActive(true);
				RaceModeLockbtn.SetActive(false);

				for (int i = 0; i < PlayerPrefs.GetInt("Unlocklevels"); i++)
				{
					Unlocklevels[i].GetComponent<Button>().interactable = true;
					lockimage[i].SetActive(false);
				}

				for (int i = 1; i < PlayerPrefs.GetInt("UnlockTrafficlevels"); i++)
				{
					UnlockTrafficlevels[i].GetComponent<Button>().interactable = true;
					lockimageStation[i].SetActive(false);
				}

				for (int i = 1; i < PlayerPrefs.GetInt("UnlockRacelevels"); i++)
				{
					UnlockRacelevels[i].GetComponent<Button>().interactable = true;
					lockimageRace[i].SetActive(false);
				}

				PlayerPrefs.SetInt("Unlol", 1);
				CareerAlllevelunlockbtn.SetActive(false);
				TMAlllevelunlockbtn.SetActive(false);
				RaceAlllevelunlockbtn.SetActive(false);
				ModeAlllevelunlockbtn.SetActive(false);
				break;

			case "Unlocktrain":
				GarageAllTrainsUnlock.SetActive(false);
				PlayerPrefs.SetInt("Unlot", 1);
				break;
		}
	}

	void OnDisable()
	{
		if (IsInvoking("Enoughpanelss"))
			CancelInvoke("Enoughpanelss");

	}
	public void Enoughpanelss()
	{
		enoughcoinpanels.SetActive(false);
	}

	public void Soundon()
	{
		PlayerPrefs.SetInt("Soundonoff", 0);
		Menuaudio.mute = true;
		Menuaudio.PlayOneShot(impact, 0.7F);
		Soundbtnon.SetActive(false);
		Soundbtnoff.SetActive(true);
	}

	public void Soundoff()
	{
		PlayerPrefs.SetInt("Soundonoff", 1);
		Menuaudio.mute = false;
		Menuaudio.PlayOneShot(impact, 0.7F);
		Soundbtnon.SetActive(true);
		Soundbtnoff.SetActive(false);
	}

	public void Musicon()
	{
		PlayerPrefs.SetInt("Musiconoff", 0);

		Menuaudio.PlayOneShot(impact, 0.7F);
		Musicbtnon.SetActive(true);
		Musicbtnoff.SetActive(false);
		Musicaudio.mute = false;
	}

	public void Musicoff()
	{
		PlayerPrefs.SetInt("Musiconoff", 1);
		Musicaudio.mute = true;
		Musicbtnon.SetActive(false);
		Musicbtnoff.SetActive(true);
		Menuaudio.PlayOneShot(impact, 0.7F);

	}




	public void Quailitylow(int qaval)
	{
		PlayerPrefs.SetInt("QuailitySetting", qaval);
		QualitySettings.SetQualityLevel(qaval);
		Menuaudio.PlayOneShot(impact, 0.7F);
		Qaulityimages[1].SetActive(true);
		Qaulityimages[3].SetActive(false);
		Qaulityimages[5].SetActive(false);
	}
	public void QuailityHigh(int qaval)
	{
		QualitySettings.SetQualityLevel(qaval);
		PlayerPrefs.SetInt("QuailitySetting", qaval);
		Menuaudio.PlayOneShot(impact, 0.7F);
		Qaulityimages[1].SetActive(false);
		Qaulityimages[3].SetActive(true);
		Qaulityimages[5].SetActive(false);
	}

	public void QuailityUltra(int qaval)
	{
		QualitySettings.SetQualityLevel(qaval);
		PlayerPrefs.SetInt("QuailitySetting", qaval);
		Menuaudio.PlayOneShot(impact, 0.7F);
		Qaulityimages[1].SetActive(false);
		Qaulityimages[3].SetActive(false);
		Qaulityimages[5].SetActive(true);
	}



	public void setTexture(int no)
	{
		PlayerPrefs.SetInt("train" + previousnextcounter1 + "texture", no);
		ModifyEngine[previousnextcounter1].setMat(no);
		ModifyBuggie1[previousnextcounter1].setMat(no);
	}

	public Text[] texts;
	public void SetLocalPrices(string Localized, int no)
	{
		texts[no].text = Localized;
	}

	public void buyTrain1()
	{
		Inappscripts.Unlockonetrain();
	}

	public void buyTrain2()
	{
		Inappscripts.Unlocktwotrain();
	}

	public void buyTrain3()
	{
		Inappscripts.Unlockthreetrain();
	}

	public void buyTrainPack()
	{
		Inappscripts.UnlockTrucks();
	}

	public void buyTrainPackGarage()
	{
		Inappscripts.UnlockTrucksGarage();

	}
	public void RemoveAllAds()
	{
		Inappscripts.Removeadss();
	}

	public void buyfiftykcoins()
	{
		Inappscripts.BuyfiftyKCoinsKCoins();
	}

	public void buyhundredkcoins()
	{
		Inappscripts.BuyCoinsHunderdKCoins();
	}

	public void UnlockallLevels()
	{
		Inappscripts.Unlocklevels();
	}

	void showBanner()
	{
		if (ads != null)
			ads.ShowBannerFunc();

	}
	void hideBanner()
	{
		if (ads)
			ads.HideBannerFunc();
	}

	void showRecBanner()
	{
		if (recAds)
			recAds.ShowBanner();
	}

	void showinterstitialfunc()
	{
		if (ads)
			ads.ShowInterstitial();
	}

	public void hideRecBanner()
	{
		if (recAds)
			recAds.HideBanner();
	}


	void ShowMenuRectAds()
	{
		if (recAds)
		{
			recAds.RequestBanner();
			//recAds.ShowBanner();
		}
	}

    void showbannerPuzzlefunc()
    {
        Banpuzzle.ShowBanner();
        Debug.Log("ShowBannerPuzzleFromMenutoPAcks");
    }

    public void hideBannerPuzzle()
	{	
		if (Banpuzzle != null)
		Banpuzzle.HideBanner();
		Debug.Log("hidepuzzlebanner");
		
	}

	static bool ft;
	public void showInterstitial()
	{
		if (!ft)
		{
			ft = true;
			if (ads)
			ads.ShowInterstitial();
		}
	}
	void hideAdapAds()
    {
		if(adapads)
        {
			adapads.HideBanner();
        }
    }

    void ShowMenubannerAds()
    {
		if (ads)
		{
			ads.RequestBanner();
		}
	}
	
	public void setCameraAnimation(bool rotate)
	{
		camAnim.SetBool("rotate", rotate);
	}

	
}
