using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Utility;
using UnityEngine.Analytics;
public class InGameManager : MonoBehaviour
{
    [HideInInspector]
    public SmoothFollow rccCameraScript;
    public RCC_Camera CamScript;
    [HideInInspector]
    public GameObject Player;
    //[HideInInspector]
    public GameObject[] PlayerVehicles, ParkingPlayervehicle;
    [HideInInspector]
    public RCC_CarControllerV3 playerCarController;
    [HideInInspector]
    public Rigidbody PlayerRB;
    [HideInInspector]
    public Transform FLCollider, FRCollider, RLCollider, RRCollider, FLW, FRW, RLW, RRW;
    [HideInInspector]
    RCC_WheelCollider FLWC, FRWC, RLWC, RRWC;
    //[HideInInspector]
    public bool TrafficMode, NormalMode = true, startStopSuspension_WheelDistance = false, Switching = false;
    [HideInInspector]
    public float OriginalWheelDistance, CurrentDistance, LerpWheelDistance, TotalWheelDistance = 0.03f, currentSuspension = 0.1f, LerpSuspension = 0.15f, TotalSuspension = 4f;
    [HideInInspector]
    public Transform FLAxil, FLAxilMidddlepart, FLAxilLowerPart, FRAxil, FRAxilMiddlePart, FRAxilLowerpart, RLAxil, RLAxilMiddlepart, RLAxilLowerpart, RRAxil, RRAxilMiddlepart, RRAxilLowerpart;
    [HideInInspector]
    public Transform FLAxilJunctionPart, FLAxilWheelPart, FRAxilJunctionpart, FRAxilWheelPart, RLAxilJunctionpart, RLAxilWheelpart, RRAxilJunctionpart, RRAxilWheelpart, CEnterOfMass;
    [HideInInspector]
    public PlayerCarScript PlayerScript;
    [HideInInspector]
    public float transformingSpeed, axilX_Value, currentAxilX_Value, LerpAxilX_Value, axilY_Value, currentAxilY_Value, LerpAxilY_Value;
    [HideInInspector]
    public float middleAxilScale, middleAxilCurrentScale, lerpMiddleAxilScale, lowerAxilScale, lowerAxilCurrentScale, LerpLowerAxilScale;
    [HideInInspector]
    public float middleAxilY_Value, middleAxilYCurrent_Value, LerpMiddleAxilY_Value, LowerAxilY_Value, LowerAxilYCurrent_Value, LerpLowerAxilY_Value;
    [HideInInspector]
    public float junctionPartY_Value, currentJunctionPartY_Value, LerpJunctionPartY_Value, wheelPartY_Value, currentWheelPartY_Value, lerpWheelPartY_Value;
    //[HideInInspector]
    public GameObject SwitchBTN, RotateWheelsBTN;
    public GameObject[] OptionsObjects;
    public Button[] invisibleObjects;
    public GameObject[] Buttons;    
    public bool useSteeringWheel, canRotate, RotateWheels, StartRotatingWheels, stopRotatingWheels;
    [HideInInspector]
    public RCC_UIController rightBtn, BrakeBTN;
    public LevelDataScript[] LevlesScript;
    [HideInInspector]
    public int currentLevel, DifficultyLevel;
    [HideInInspector]
    public Transform spawnPoint, NewTarget;
    public GameObject MissionCompletePanel, MissionFailPanel, HitsFinishedPanel;
    [HideInInspector]
    public SmoothFollow PlayerCamera;
    [HideInInspector]
    public lookAtDest ArrowScript;
    [HideInInspector]
    public Color Parking1, Parking2, Parking3, Parking4;
    bool MissionCompletebool;
    GameObject ProgressBar;
    int CurrentCamera;
    [HideInInspector]
    public Image ParkingProgressBarImage;
    [HideInInspector]
    public Text ParkingProgressBarText;
    float Progress = 0f;
    int CurrentParkingArea = 0;
    GameObject[] ParkingArea;
    [HideInInspector]
    public GameObject StopMark, ReverseMark;
    [HideInInspector]
    public bool changeCamHeight;
    [HideInInspector]
    public float camHeight, currentCamHeight, lerpCamHeight, camDis, currentCamDis, lerpCamDis, elevatedCamHeight = 10f, elevatedCamDistance = 6f;
    public Text InfoText, TotalPoints, currentPoints;

    //for points Calculating
    public int points;
    ////////////////////for Tutorial/////////////////////
    [HideInInspector]
    public bool ShowTutorial, applyBrakes, StartMovingCar, showSwitchBTN, ShowRotateBTN, realparkingbool;
    int tutorialNo;
    [HideInInspector]
    public bool skiped;
    public GameObject switchHighlightingBTN, RotateHighlightingBTN;
    [HideInInspector]
    bool canSwitchHBTN, CanRotateHBTN;
    [HideInInspector]
    public GameObject TutorialPAnel;
    [HideInInspector]
    public Text tutorialLabel;
    public GameObject LeftRightSteerBtns;
    bool moveLeft, MoveRight;
    float xposition;
    int currentDirection;
    [HideInInspector]
    public bool firstTimeGearChange; // for camera Direction
    bool parkingLevelOnly, damageStart = true;
    public Scrollbar GearSlider;
    public float SliderValue;
    // hitting text
    public int hits = 10;
    public Text hitsLabel;
    public GameObject HitsObject;
    bool AiLevel, showBTNs, showLevelBTNAllTime;
    public Text CountDown;
    public bool ShowMsg, useTimer, GameFinished;
    public Text msgText;
    [HideInInspector]
    public GameObject msgPanel;
    [HideInInspector]
    public Text DistanceLabel;
    [HideInInspector]
    public GameObject DistancePanel;
    [HideInInspector]
    public GameObject TimePanel;
    public Text TimeShow;
    public float timer;
    private int minutes = 0;
    private int seconds = 0;
    public GameObject timeFinishedPanel;
    public GameObject NextBTN;
    public RCC_Settings settingsScript;
    public GameObject tutorialBtn;
    public Settings settingScript;
    public GameObject accelerateionBTN;
    bool showSwitchBtn = true;
    public Image LoadingImage;
    private GoogleMobileAdsDemoScript Adobj;
    private RectangleBannerAd_aa rectbanerobj;
    private INAPP inappobj;
    GameSceneBanner gamebanner;
    // string leaderBoardID = "com.dkm.earned.traffic";
    public GameObject SkipButton;
    public Light Direc;
    public Camera mycamer;
    private bool showads = false;
    public GameObject Hand;
    public bool showRotateNow = false;
    public GameObject RotateHighlight;
    public Text ReverseText;
    int level4stepscounter;
    public GameObject LeftHighlightingBar;
    public Material Skyboxmat;
    public Color skyboxcolor;
    public AudioSource warningHitSound, EngineSound;
    public GameObject FadeObj, navArrowObj;
    public RCC_UIController reverseBtn, forwardBtn, GearRev;
    public bool ChangeCamTrigPos;
    public Animator hitcarImg, Hittxt, hitlablanim;
    bool checkCarDirctnbol;
    public GameObject LoadingPanel;
    //	hitcarImg, hitTxt, hitsLabel;
    public GameObject HandReverse;
    
    public GameObject[] neon;
    public Carss[] car;   
    public RCC_Camera CamerScript;
    public GameObject Lvlhit;
    public GameObject Parkingtut;
    public MainMenuManager MManager;
    public void ShowHandFirstTime()
	{
	if (PlayerPrefs.GetInt("CurrentLevel") == 21) 
       {
		 Hand.SetActive (true);
         Invoke("handoff",4f);
			//PlayerPrefs.SetInt ("Hand1", 1);
		} 
//		else if (PlayerPrefs.GetInt ("CurrentLevel") == 4) {
//			Hand.SetActive (true);
//		}
	}


    void handoff()
    {
        Hand.SetActive(false);
    }
	void Start () 
    {      
        LoadingPanel.SetActive (true);
		Invoke("DelayOffObj",2f);      
        Time.timeScale = 1f;
        TimePanel.SetActive(false);
        AudioListener.volume = 1f;
        CheckPurchaseBTNS();      
        setEnviornment();
        //activateEnviornment();
        Invoke("activateEnviornment",0.5f);
        if (PlayerPrefs.GetInt("CurrentLevel") < 21)
        {
            RotateWheelsBTN.GetComponent<Button>().interactable = false; 
            //RotateWheelsBTN.SetActive(false);
            Parkingtut.SetActive(false);
        }
        else
        {
            RotateWheelsBTN.SetActive(false);
            Parkingtut.SetActive(true);
        }
  
        ProgressBar = ParkingProgressBarImage.transform.parent.gameObject;
        LeftRightSteerBtns.SetActive(false);
        UpdateHits();
        if (AiLevel)
        {
            ArrowScript.gameObject.SetActive(false);
        }

        transformingSpeed = LevlesScript[currentLevel].TransformingSpeed;
        showBTNs = LevlesScript[currentLevel].showBTNs;
        ShowMsg = LevlesScript[currentLevel].ShowMsg;
        checkMsg();
        
        if (AiLevel)
        { 
            DistanceLabel.gameObject.SetActive(true); 
        }
        SetControls();
       
        Adobj = (GoogleMobileAdsDemoScript)FindObjectOfType(typeof(GoogleMobileAdsDemoScript));
		if (Adobj != null)
			Adobj.GMLink();

        rectbanerobj = (RectangleBannerAd_aa)FindObjectOfType(typeof(RectangleBannerAd_aa));
        
        inappobj = (INAPP)FindObjectOfType(typeof(INAPP));
        if (inappobj != null)
			inappobj.GamePlayLinker ();

        gamebanner = FindObjectOfType<GameSceneBanner>();
        if (gamebanner)
        {
            gamebanner.GMLink();
        }
        if (Application.platform == RuntimePlatform.Android) 
		{
			if (SystemInfo.systemMemorySize <= 2700) 
			{
				Direc.shadows = LightShadows.None;
				mycamer.farClipPlane = 100f;
			}
		}
		showads = true;
        if (PlayerPrefs.GetInt ("CurrentLevel") < 21) 
        {
			Gear.SetActive (false);			
			TimePanel.SetActive (false);
		} 
        else 
        {
			Gear.SetActive (false);
			TimeShow.text = "--";
			TimePanel.SetActive (false);
			
		}

		if (DifficultyLevel == 0) 
        {
			CustomAnalytics.eventMessage ("elevateddriving"+(currentLevel)+"_start");
		}
        else if(DifficultyLevel == 1)
        {
			CustomAnalytics.eventMessage ("hardelevation"+(currentLevel)+"_start");
		}
        else if(DifficultyLevel == 3)
        {
			CustomAnalytics.eventMessage ("parking"+(currentLevel-20)+"_start");
		}
 
        for (int m = 0; m < PlayerVehicles.Length; m++)
        {
            if (PlayerPrefs.GetInt("Neon" + m) == 1)
            {
                neon[m].SetActive(true);
                Debug.Log("NEONGAME");
            }
        }
        if(currentLevel >= 31)
        {
            Lvlhit.SetActive(true);
        }
        
        LoadSpoiler();
       
       
    }

    public void RevGear()
    {
        playerCarController.currentGear = 2;
       
    }

    void DelayOffObj()
    {
		LoadingPanel.SetActive (false);
		EngineSound.Play ();
	}
	void changeSkybox()
    {
		RenderSettings.skybox = Skyboxmat;
		RenderSettings.ambientSkyColor = skyboxcolor;
	}

    public void ChangeCamera()
    {     
        if (CurrentCamera == 1)
        {
            Debug.Log("CurrentCam =" + CurrentCamera);
            rccCameraScript.target = NewTarget;           
            rccCameraScript.enabled = true;
            CurrentCamera = 0;
        }
        else if (CurrentCamera == 0)
        {
            Debug.Log("CurrentCam =" + CurrentCamera);
            rccCameraScript.transform.parent = PlayerScript.InnerCameraPosition;
            PlayerScript.MoveToTarget.parent = PlayerScript.InnerCameraPosition;
            rccCameraScript.transform.localPosition = Vector3.zero;
            rccCameraScript.transform.localEulerAngles = Vector3.zero;
            rccCameraScript.enabled = false;
            CurrentCamera = 1;

        }
    }

    public int rotationspeed =50;
    void Update()
    {
		if(checkCarDirctnbol && !gameObject.GetComponent<CameraRotate>().manualRotation)
        {
           
		    if (Player.GetComponent<RCC_CarControllerV3>().direction==-1 && reverseBtn.pressing) 
            {
			    ChangeCamTrigPos = false; 
                PlayerScript.MoveToTarget.localEulerAngles = new Vector3(0f, 180f, 0f);
                //rccCameraScript.target.localEulerAngles = new Vector3 (0f,180f, 0f);
                
            } 
            else if(Player.GetComponent<RCC_CarControllerV3>().direction==1 && forwardBtn.pressing) 
             {
			    ChangeCamTrigPos = true;
                PlayerScript.MoveToTarget.localEulerAngles = new Vector3(0f, 0f, 0f);
                //rccCameraScript.target.localEulerAngles = new Vector3(0f, 0f, 0f);             
                
		      }
	    }

        if (moveLeft)
        {
            Player.transform.Translate(-Vector3.right * Time.deltaTime*1.5f);
            PlayerRB.velocity = Vector3.zero;
            FLW.transform.Rotate(Vector3.right * Time.deltaTime * 120.5f);
            FRW.transform.Rotate(-Vector3.right * Time.deltaTime * 120.5f);
            RLW.transform.Rotate(Vector3.right * Time.deltaTime * 120.5f);
            RRW.transform.Rotate(-Vector3.right * Time.deltaTime * 120.5f);
            
        }
        else if (MoveRight)
        {
            Player.transform.Translate(Vector3.right * Time.deltaTime * 1.5f);
            PlayerRB.velocity = Vector3.zero;
            FLW.transform.Rotate(-Vector3.right * Time.deltaTime * 120.5f);
            FRW.transform.Rotate(Vector3.right * Time.deltaTime * 120.5f);
            RLW.transform.Rotate(-Vector3.right * Time.deltaTime * 120.5f);
            RRW.transform.Rotate(Vector3.right * Time.deltaTime * 120.5f);      
        }

         
        if (changeCamHeight)
        {
            if (currentCamHeight < elevatedCamHeight)
            {
                lerpCamHeight = Mathf.Lerp(lerpCamHeight, elevatedCamHeight + 4f, 0.2f * Time.deltaTime);
                currentCamHeight = lerpCamHeight;
                rccCameraScript.height = currentCamHeight;
            }
            else
            {
                rccCameraScript.height = elevatedCamHeight;
            }
            if (currentCamDis <elevatedCamDistance)
            {
                lerpCamDis = Mathf.Lerp(lerpCamDis, elevatedCamDistance + 2f, 0.2f * Time.deltaTime);
                currentCamDis = lerpCamDis;
                rccCameraScript.distance = currentCamDis;
            }
            else
            {
                rccCameraScript.distance = elevatedCamDistance;
            }
        }
        else if (!changeCamHeight)
        {

            if (currentCamHeight > camHeight)
            {
                lerpCamHeight = Mathf.Lerp(lerpCamHeight, camHeight - 4f, 0.3f * Time.deltaTime);
                currentCamHeight = lerpCamHeight;
                rccCameraScript.height = currentCamHeight;
            }
            else
            {
                rccCameraScript.height = camHeight;
            }
            if (currentCamDis > camDis)
            {
                lerpCamDis = Mathf.Lerp(lerpCamDis, camDis- 2f, 0.3f * Time.deltaTime);
                currentCamDis = lerpCamDis;
                rccCameraScript.distance = currentCamDis;
            }
            else
            {
                rccCameraScript.distance = camDis;
            }
        }
        if (StartRotatingWheels)
        {
            rightBtn.pressing = true;
        }
        else if (!stopRotatingWheels)
        {
            rightBtn.pressing = false;
            stopRotatingWheels = true;
        }
        if (playerCarController)
        {
            if (ShowTutorial) // if tutorial
            {
				
				if (showSwitchBTN)
                {
					if (!showLevelBTNAllTime) 
                    {

						if (playerCarController.speed < 2f) 
                        {
							SwitchBTN.SetActive (true);
							if (canSwitchHBTN) 
                            {
								switchHighlightingBTN.SetActive (true);
							} 
                            else 
                            {
								switchHighlightingBTN.SetActive (false);
							}
						}
                        else 
                        {
							SwitchBTN.SetActive (false);
							switchHighlightingBTN.SetActive (false);
						}
					} 
                    else 
                    {
					
						SwitchBTN.SetActive (true);
					}
				}
                else
                {
					if (!StartRotatingWheels && currentLevel<21)
					{
						SwitchBTN.SetActive(true);
					}
					else
					{
						SwitchBTN.SetActive (false);
					}
					switchHighlightingBTN.SetActive(false);
                }
            }
            else if (playerCarController && !Switching)
            {
                if ( !showLevelBTNAllTime)
                {

                    if (playerCarController.speed < 0.1f)
                    {

                        if (!StartRotatingWheels)
                        {
                            SwitchBTN.SetActive(true);
                        }
                        else
                        {
                            SwitchBTN.SetActive(false);

                        }
                    }
                    else
                    {
                        SwitchBTN.SetActive(false);
                    }
                }
                else if (AiLevel || showLevelBTNAllTime)
                {

					if (!StartRotatingWheels && currentLevel<21)
                    {
                        SwitchBTN.SetActive(true);
                    }
                    else
                    {
						SwitchBTN.SetActive (false);
                    }
                }
            }

        }
        if (ShowTutorial) // if tutorial
       {
           if (ShowRotateBTN)
           {
                RotateWheelsBTN.GetComponent<Button>().interactable = true;// RotateWheelsBTN.SetActive(true);
                RotateHighlightingBTN.SetActive(true);
           }
           else
           {
                RotateWheelsBTN.GetComponent<Button>().interactable = false;//  RotateWheelsBTN.SetActive(false);
                RotateHighlightingBTN.SetActive(false);
            }
       }

       else if (canRotate && !Switching)
        {
			if (playerCarController.speed < 0.1f)
			{
                RotateWheelsBTN.GetComponent<Button>().interactable = true; //RotateWheelsBTN.SetActive(true);
                RotateHighlightingBTN.SetActive(true);
            }
			else
			{
                RotateWheelsBTN.GetComponent<Button>().interactable = false; //RotateWheelsBTN.SetActive(false);
                RotateHighlightingBTN.SetActive(false);
            }

        }
        else
        {
            RotateWheelsBTN.GetComponent<Button>().interactable = false;// RotateWheelsBTN.SetActive(false);
            RotateHighlightingBTN.SetActive(false);
        }
        if (TrafficMode && NormalMode)
        {
            SwitchBTN.SetActive(false);
            if (CurrentDistance < TotalWheelDistance)
            {
                //for colliders
                LerpWheelDistance = Mathf.Lerp(LerpWheelDistance, TotalWheelDistance + TotalWheelDistance, transformingSpeed/1.5f * Time.deltaTime);
                CurrentDistance = LerpWheelDistance;
                FLCollider.localPosition = new Vector3(CurrentDistance, FLCollider.localPosition.y, FLCollider.localPosition.z);
                FRCollider.localPosition = new Vector3(-CurrentDistance, FRCollider.localPosition.y, FRCollider.localPosition.z);
                RLCollider.localPosition = new Vector3(-CurrentDistance, RLCollider.localPosition.y, RLCollider.localPosition.z);
                RRCollider.localPosition = new Vector3(CurrentDistance, RRCollider.localPosition.y, RRCollider.localPosition.z);
                //for wheel
                FLW.localPosition = new Vector3(CurrentDistance, FLW.localPosition.y, FLW.localPosition.z);
                FRW.localPosition = new Vector3(-CurrentDistance, FRW.localPosition.y, FRW.localPosition.z);
                RLW.localPosition = new Vector3(-CurrentDistance, RLW.localPosition.y, RLW.localPosition.z);
                RRW.localPosition = new Vector3(CurrentDistance, RRW.localPosition.y, RRW.localPosition.z);
            }
            else
            {
                FLCollider.localPosition = new Vector3(TotalWheelDistance, FLCollider.localPosition.y, FLCollider.localPosition.z);
                FRCollider.localPosition = new Vector3(-TotalWheelDistance, FRCollider.localPosition.y, FRCollider.localPosition.z);
                RLCollider.localPosition = new Vector3(-TotalWheelDistance, RLCollider.localPosition.y, RLCollider.localPosition.z);
                RRCollider.localPosition = new Vector3(TotalWheelDistance, RRCollider.localPosition.y, RRCollider.localPosition.z);
                FLW.localPosition = new Vector3(TotalWheelDistance, FLW.localPosition.y, FLW.localPosition.z);
                FRW.localPosition = new Vector3(-TotalWheelDistance, FRW.localPosition.y, FRW.localPosition.z);
                RLW.localPosition = new Vector3(-TotalWheelDistance, RLW.localPosition.y, RLW.localPosition.z);
                RRW.localPosition = new Vector3(TotalWheelDistance, RRW.localPosition.y, RRW.localPosition.z);
            }
            //move axil in x direction...
            if (currentAxilX_Value > -0.218f)
            {
                LerpAxilX_Value = Mathf.Lerp(LerpAxilX_Value, 0.027f - 1.5F, transformingSpeed*1f * Time.deltaTime);
                currentAxilX_Value = LerpAxilX_Value;
                FLAxil.localPosition = new Vector3(currentAxilX_Value, FLAxil.localPosition.y, FLAxil.localPosition.z);
                FRAxil.localPosition = new Vector3(-currentAxilX_Value, FRAxil.localPosition.y, FRAxil.localPosition.z);
                RLAxil.localPosition = new Vector3(currentAxilX_Value, RLAxil.localPosition.y, RLAxil.localPosition.z);
                RRAxil.localPosition = new Vector3(-currentAxilX_Value, RRAxil.localPosition.y, RRAxil.localPosition.z); 
            }
            else
            {
                FLAxil.localPosition = new Vector3(-0.218f, FLAxil.localPosition.y, FLAxil.localPosition.z);
                FRAxil.localPosition = new Vector3(0.218f, FRAxil.localPosition.y, FRAxil.localPosition.z);
                RLAxil.localPosition = new Vector3(-0.218f, RLAxil.localPosition.y, RLAxil.localPosition.z);
                RRAxil.localPosition = new Vector3(0.218f, RRAxil.localPosition.y, RRAxil.localPosition.z);
            }
            //move axil in y Direction
            if (currentAxilY_Value > 0.5f)
            {
                LerpAxilY_Value = Mathf.Lerp(LerpAxilY_Value, 0, transformingSpeed*3f * Time.deltaTime);
                currentAxilY_Value = LerpAxilY_Value;
                FLAxil.localPosition = new Vector3(FLAxil.localPosition.x, currentAxilY_Value, FLAxil.localPosition.z);
                FRAxil.localPosition = new Vector3(FRAxil.localPosition.x, currentAxilY_Value, FRAxil.localPosition.z);
                RLAxil.localPosition = new Vector3(RLAxil.localPosition.x, currentAxilY_Value, RLAxil.localPosition.z);
                RRAxil.localPosition = new Vector3(RRAxil.localPosition.x, currentAxilY_Value, RRAxil.localPosition.z);
            }
            else
            {
                FLAxil.localPosition = new Vector3(FLAxil.localPosition.x, 0.5f, FLAxil.localPosition.z);
                FRAxil.localPosition = new Vector3(FRAxil.localPosition.x, 0.5f, FRAxil.localPosition.z);
                RLAxil.localPosition = new Vector3(RLAxil.localPosition.x, 0.5f, RLAxil.localPosition.z);
                RRAxil.localPosition = new Vector3(RRAxil.localPosition.x, 0.5f, RRAxil.localPosition.z);
            }
            //scale middlePart axil in y direction..
            if (currentAxilY_Value < 0.8f)
            {
                if (middleAxilCurrentScale < 0.5f)
                {
                    lerpMiddleAxilScale = Mathf.Lerp(lerpMiddleAxilScale, 1f, transformingSpeed*1.5f * Time.deltaTime);
                    middleAxilCurrentScale = lerpMiddleAxilScale;
                    FLAxilMidddlepart.localScale = new Vector3(FLAxilMidddlepart.localScale.x, middleAxilCurrentScale, FLAxilMidddlepart.localScale.z);
                    FRAxilMiddlePart.localScale = new Vector3(FRAxilMiddlePart.localScale.x, middleAxilCurrentScale, FRAxilMiddlePart.localScale.z);
                    RLAxilMiddlepart.localScale = new Vector3(RLAxilMiddlepart.localScale.x, middleAxilCurrentScale, RLAxilMiddlepart.localScale.z);
                    RRAxilMiddlepart.localScale = new Vector3(RRAxilMiddlepart.localScale.x, middleAxilCurrentScale, RRAxilMiddlepart.localScale.z);
                }
                else
                {
                    FLAxilMidddlepart.localScale = new Vector3(FLAxilMidddlepart.localScale.x, 0.5f, FLAxilMidddlepart.localScale.z);
                    FRAxilMiddlePart.localScale = new Vector3(FRAxilMiddlePart.localScale.x, 0.5f, FRAxilMiddlePart.localScale.z);
                    RLAxilMiddlepart.localScale = new Vector3(RLAxilMiddlepart.localScale.x, 0.5f, RLAxilMiddlepart.localScale.z);
                    RRAxilMiddlepart.localScale = new Vector3(RRAxilMiddlepart.localScale.x, 0.5f, RRAxilMiddlepart.localScale.z);
                }
            }
            //transform MiddleAxil in y direction......
            if (middleAxilCurrentScale > 0.3f)
            {
                if (middleAxilYCurrent_Value > -0.45f)
                {
                    LerpMiddleAxilY_Value = Mathf.Lerp(LerpMiddleAxilY_Value, -1f, transformingSpeed*2f * Time.deltaTime);
                    middleAxilYCurrent_Value = LerpMiddleAxilY_Value;
                    FLAxilMidddlepart.localPosition = new Vector3(FLAxilMidddlepart.localPosition.x, middleAxilYCurrent_Value, FLAxilMidddlepart.localPosition.z);
                    FRAxilMiddlePart.localPosition = new Vector3(FRAxilMiddlePart.localPosition.x, middleAxilYCurrent_Value, FRAxilMiddlePart.localPosition.z);
                    RLAxilMiddlepart.localPosition = new Vector3(RLAxilMiddlepart.localPosition.x, middleAxilYCurrent_Value, RLAxilMiddlepart.localPosition.z);
                    RRAxilMiddlepart.localPosition = new Vector3(RRAxilMiddlepart.localPosition.x, middleAxilYCurrent_Value, RRAxilMiddlepart.localPosition.z);
                }
                else
                {
                    FLAxilMidddlepart.localPosition = new Vector3(FLAxilMidddlepart.localPosition.x, -0.45f, FLAxilMidddlepart.localPosition.z);
                    FRAxilMiddlePart.localPosition = new Vector3(FRAxilMiddlePart.localPosition.x, -0.45f, FRAxilMiddlePart.localPosition.z);
                    RLAxilMiddlepart.localPosition = new Vector3(RLAxilMiddlepart.localPosition.x, -0.45f, RLAxilMiddlepart.localPosition.z);
                    RRAxilMiddlepart.localPosition = new Vector3(RRAxilMiddlepart.localPosition.x, -0.45f, RRAxilMiddlepart.localPosition.z);
                }
            }
            //scale LowerPart axil in y direction...............................
            if (middleAxilYCurrent_Value < 0.5f)
            {
                if (lowerAxilCurrentScale < 0.45f)
                {
                    LerpLowerAxilScale = Mathf.Lerp(LerpLowerAxilScale, 1, transformingSpeed * Time.deltaTime);
                    lowerAxilCurrentScale = LerpLowerAxilScale;
                    FLAxilLowerPart.localScale = new Vector3(FLAxilLowerPart.localScale.x, lowerAxilCurrentScale, FLAxilLowerPart.localScale.z);
                    FRAxilLowerpart.localScale = new Vector3(FRAxilLowerpart.localScale.x, lowerAxilCurrentScale, FRAxilLowerpart.localScale.z);
                    RLAxilLowerpart.localScale = new Vector3(RLAxilLowerpart.localScale.x, lowerAxilCurrentScale, RLAxilLowerpart.localScale.z);
                    RRAxilLowerpart.localScale = new Vector3(RRAxilLowerpart.localScale.x, lowerAxilCurrentScale, RRAxilLowerpart.localScale.z);
                }
                else
                {
                    FLAxilLowerPart.localScale = new Vector3(FLAxilLowerPart.localScale.x, 0.45f, FLAxilLowerPart.localScale.z);
                    FRAxilLowerpart.localScale = new Vector3(FRAxilLowerpart.localScale.x, 0.45f, FRAxilLowerpart.localScale.z);
                    RLAxilLowerpart.localScale = new Vector3(RLAxilLowerpart.localScale.x, 0.45f, RLAxilLowerpart.localScale.z);
                    RRAxilLowerpart.localScale = new Vector3(RRAxilLowerpart.localScale.x, 0.45f, RRAxilLowerpart.localScale.z);
                }
            } 
            //transform LowerAxil in y direction...............................
            if (lowerAxilCurrentScale > 0.35f)
            {
                if (LowerAxilYCurrent_Value > -1.571f)
                {
                    LerpLowerAxilY_Value = Mathf.Lerp(LerpLowerAxilY_Value, -2f, transformingSpeed*3f * Time.deltaTime);
                    LowerAxilYCurrent_Value = LerpLowerAxilY_Value;
                    FLAxilLowerPart.localPosition = new Vector3(FLAxilLowerPart.localPosition.x, LowerAxilYCurrent_Value, FLAxilLowerPart.localPosition.z);
                    FRAxilLowerpart.localPosition = new Vector3(FRAxilLowerpart.localPosition.x, LowerAxilYCurrent_Value, FRAxilLowerpart.localPosition.z);
                    RLAxilLowerpart.localPosition = new Vector3(RLAxilLowerpart.localPosition.x, LowerAxilYCurrent_Value, RLAxilLowerpart.localPosition.z);
                    RRAxilLowerpart.localPosition = new Vector3(RRAxilLowerpart.localPosition.x, LowerAxilYCurrent_Value, RRAxilLowerpart.localPosition.z);
                }
                else
                {
                    FLAxilLowerPart.localPosition = new Vector3(FLAxilLowerPart.localPosition.x, -1.571f, FLAxilLowerPart.localPosition.z);
                    FRAxilLowerpart.localPosition = new Vector3(FRAxilLowerpart.localPosition.x, -1.571f, FRAxilLowerpart.localPosition.z);
                    RLAxilLowerpart.localPosition = new Vector3(RLAxilLowerpart.localPosition.x, -1.571f, RLAxilLowerpart.localPosition.z);
                    RRAxilLowerpart.localPosition = new Vector3(RRAxilLowerpart.localPosition.x, -1.571f, RRAxilLowerpart.localPosition.z);
                }
            }
            //transform JunctionPart in Y Direction
            if (middleAxilYCurrent_Value <= -0.1f)
            {
                if (currentJunctionPartY_Value > -0.931f)
                {
                    LerpJunctionPartY_Value = Mathf.Lerp(LerpJunctionPartY_Value, -2f, transformingSpeed*2f * Time.deltaTime);
                    currentJunctionPartY_Value = LerpJunctionPartY_Value;
                    FLAxilJunctionPart.localPosition = new Vector3(FLAxilJunctionPart.localPosition.x, currentJunctionPartY_Value, FLAxilJunctionPart.localPosition.z);
                    FRAxilJunctionpart.localPosition = new Vector3(FRAxilJunctionpart.localPosition.x, currentJunctionPartY_Value, FRAxilJunctionpart.localPosition.z);
                    RLAxilJunctionpart.localPosition = new Vector3(RLAxilJunctionpart.localPosition.x, currentJunctionPartY_Value, RLAxilJunctionpart.localPosition.z);
                    RRAxilJunctionpart.localPosition = new Vector3(RRAxilJunctionpart.localPosition.x, currentJunctionPartY_Value, RRAxilJunctionpart.localPosition.z);
                }
                else
                {
                    FLAxilJunctionPart.localPosition = new Vector3(FLAxilJunctionPart.localPosition.x, -0.931f, FLAxilJunctionPart.localPosition.z);
                    FRAxilJunctionpart.localPosition = new Vector3(FRAxilJunctionpart.localPosition.x, -0.931f, FRAxilJunctionpart.localPosition.z);
                    RLAxilJunctionpart.localPosition = new Vector3(RLAxilJunctionpart.localPosition.x, -0.931f, RLAxilJunctionpart.localPosition.z);
                    RRAxilJunctionpart.localPosition = new Vector3(RRAxilJunctionpart.localPosition.x, -0.931f, RRAxilJunctionpart.localPosition.z);
                }
            }
            //transform wheel part in y direction....
            if (currentAxilY_Value <0.65f)
            {
                if (currentWheelPartY_Value > -2.023f)
                {

                    lerpWheelPartY_Value = Mathf.Lerp(lerpWheelPartY_Value, -3f, transformingSpeed*2f * Time.deltaTime);
                    currentWheelPartY_Value = lerpWheelPartY_Value;
                    //currentWheelPartY_Value -= transformingSpeed * 2f * Time.deltaTime;
                    FLAxilWheelPart.localPosition = new Vector3(FLAxilWheelPart.localPosition.x, currentWheelPartY_Value, FLAxilWheelPart.localPosition.z);
                    FRAxilWheelPart.localPosition = new Vector3(FRAxilWheelPart.localPosition.x, currentWheelPartY_Value, FRAxilWheelPart.localPosition.z);
                    RLAxilWheelpart.localPosition = new Vector3(RLAxilWheelpart.localPosition.x, currentWheelPartY_Value, RLAxilWheelpart.localPosition.z);
                    RRAxilWheelpart.localPosition = new Vector3(RRAxilWheelpart.localPosition.x, currentWheelPartY_Value, RRAxilWheelpart.localPosition.z);
                }
                else
                {
                    FLAxilWheelPart.localPosition = new Vector3(FLAxilWheelPart.localPosition.x, -2.023f, FLAxilWheelPart.localPosition.z);
                    FRAxilWheelPart.localPosition = new Vector3(FRAxilWheelPart.localPosition.x, -2.023f, FRAxilWheelPart.localPosition.z);
                    RLAxilWheelpart.localPosition = new Vector3(RLAxilWheelpart.localPosition.x, -2.023f, RLAxilWheelpart.localPosition.z);
                    RRAxilWheelpart.localPosition = new Vector3(RRAxilWheelpart.localPosition.x, -2.023f, RRAxilWheelpart.localPosition.z);
                }
            }
            //suspension
            if (currentSuspension < TotalSuspension)
            {
                LerpSuspension = Mathf.Lerp(LerpSuspension, TotalSuspension + TotalSuspension, transformingSpeed * Time.deltaTime);
                currentSuspension = LerpSuspension;
                playerCarController.FrontLeftWheelCollider.wheelCollider.suspensionDistance = currentSuspension;
                playerCarController.FrontRightWheelCollider.wheelCollider.suspensionDistance = currentSuspension;
                playerCarController.RearLeftWheelCollider.wheelCollider.suspensionDistance = currentSuspension;
                playerCarController.RearRightWheelCollider.wheelCollider.suspensionDistance = currentSuspension;
            }
            else
            {
                playerCarController.FrontLeftWheelCollider.wheelCollider.suspensionDistance = TotalSuspension;
                playerCarController.FrontRightWheelCollider.wheelCollider.suspensionDistance = TotalSuspension;
                playerCarController.RearLeftWheelCollider.wheelCollider.suspensionDistance = TotalSuspension;
                playerCarController.RearRightWheelCollider.wheelCollider.suspensionDistance = TotalSuspension;
                NormalMode = false;
                //startStopSuspension_WheelDistance = false;
                SwitchBTN.SetActive(true);
                setTrafficModeSettinngs();
                canRotate = true;
                ShowObjects();
                EnableBarColliders();
                ObjectVisible();
                PlayerRB.constraints = RigidbodyConstraints.FreezePositionY;
                GearSlider.value = 0f;
                GearSlider.value = SliderValue;
				if (currentLevel == 4) {
					if (level4stepscounter == 0) {
						//level4stepscounter = 1;
						//RotateHighlightingBTN.SetActive (true);
						//ShowRotateBTN = true;
						//showSwitchBTN = false;

					}
				}

            }
        }
        else if (!TrafficMode && !NormalMode)
        {
            SwitchBTN.SetActive(false);

            if (CurrentDistance >= OriginalWheelDistance)
            {
                //start wheel Expansion..
                //for colliders
                LerpWheelDistance = Mathf.Lerp(LerpWheelDistance, -OriginalWheelDistance*2, transformingSpeed/2.5f * Time.deltaTime);
                CurrentDistance = LerpWheelDistance;
                FLCollider.localPosition = new Vector3(CurrentDistance, FLCollider.localPosition.y, FLCollider.localPosition.z);
                FRCollider.localPosition = new Vector3(-CurrentDistance, FRCollider.localPosition.y, FRCollider.localPosition.z);
                RLCollider.localPosition = new Vector3(-CurrentDistance, RLCollider.localPosition.y, RLCollider.localPosition.z);
                RRCollider.localPosition = new Vector3(CurrentDistance, RRCollider.localPosition.y, RRCollider.localPosition.z);
                FLW.localPosition = new Vector3(CurrentDistance, FLW.localPosition.y, FLW.localPosition.z);
                FRW.localPosition = new Vector3(-CurrentDistance, FRW.localPosition.y, FRW.localPosition.z);
                RLW.localPosition = new Vector3(-CurrentDistance, RLW.localPosition.y, RLW.localPosition.z);
                RRW.localPosition = new Vector3(CurrentDistance, RRW.localPosition.y, RRW.localPosition.z);
            }
            else
            {
                FLCollider.localPosition = new Vector3(OriginalWheelDistance, FLCollider.localPosition.y, FLCollider.localPosition.z);
                FRCollider.localPosition = new Vector3(-OriginalWheelDistance, FRCollider.localPosition.y, FRCollider.localPosition.z);
                RLCollider.localPosition = new Vector3(-OriginalWheelDistance, RLCollider.localPosition.y, RLCollider.localPosition.z);
                RRCollider.localPosition = new Vector3(OriginalWheelDistance, RRCollider.localPosition.y, RRCollider.localPosition.z);
                FLW.localPosition = new Vector3(OriginalWheelDistance, FLW.localPosition.y, FLW.localPosition.z);
                FRW.localPosition = new Vector3(-OriginalWheelDistance, FRW.localPosition.y, FRW.localPosition.z);
                RLW.localPosition = new Vector3(-OriginalWheelDistance, RLW.localPosition.y, RLW.localPosition.z);
                RRW.localPosition = new Vector3(OriginalWheelDistance, RRW.localPosition.y, RRW.localPosition.z);
            }
            //move axil in x direction...
            if (currentAxilX_Value < axilX_Value)
            {
                LerpAxilX_Value = Mathf.Lerp(LerpAxilX_Value, axilX_Value*1.3f, transformingSpeed * 1.6f * Time.deltaTime);
                currentAxilX_Value = LerpAxilX_Value;
                FLAxil.localPosition = new Vector3(currentAxilX_Value, FLAxil.localPosition.y, FLAxil.localPosition.z);
                FRAxil.localPosition = new Vector3(-currentAxilX_Value, FRAxil.localPosition.y, FRAxil.localPosition.z);
                RLAxil.localPosition = new Vector3(currentAxilX_Value, RLAxil.localPosition.y, RLAxil.localPosition.z);
                RRAxil.localPosition = new Vector3(-currentAxilX_Value, RRAxil.localPosition.y, RRAxil.localPosition.z);
            }
            else
            {
                FLAxil.localPosition = new Vector3(axilX_Value, FLAxil.localPosition.y, FLAxil.localPosition.z);
                FRAxil.localPosition = new Vector3(-axilX_Value, FRAxil.localPosition.y, FRAxil.localPosition.z);
                RLAxil.localPosition = new Vector3(axilX_Value, RLAxil.localPosition.y, RLAxil.localPosition.z);
                RRAxil.localPosition = new Vector3(-axilX_Value, RRAxil.localPosition.y, RRAxil.localPosition.z);
            }
            //move axil in y Direction
            if (currentAxilY_Value < axilY_Value)
            {
                LerpAxilY_Value = Mathf.Lerp(LerpAxilY_Value, axilY_Value*2, transformingSpeed*1.5f * Time.deltaTime);
                currentAxilY_Value = LerpAxilY_Value;
                FLAxil.localPosition = new Vector3(FLAxil.localPosition.x, currentAxilY_Value, FLAxil.localPosition.z);
                FRAxil.localPosition = new Vector3(FRAxil.localPosition.x, currentAxilY_Value, FRAxil.localPosition.z);
                RLAxil.localPosition = new Vector3(RLAxil.localPosition.x, currentAxilY_Value, RLAxil.localPosition.z);
                RRAxil.localPosition = new Vector3(RRAxil.localPosition.x, currentAxilY_Value, RRAxil.localPosition.z);
            }
            else
            {
                FLAxil.localPosition = new Vector3(FLAxil.localPosition.x, axilY_Value, FLAxil.localPosition.z);
                FRAxil.localPosition = new Vector3(FRAxil.localPosition.x, axilY_Value, FRAxil.localPosition.z);
                RLAxil.localPosition = new Vector3(RLAxil.localPosition.x, axilY_Value, RLAxil.localPosition.z);
                RRAxil.localPosition = new Vector3(RRAxil.localPosition.x, axilY_Value, RRAxil.localPosition.z);
            }
            //scale middlePart axil in y direction..
            if (middleAxilCurrentScale > middleAxilScale)
            {
                lerpMiddleAxilScale = Mathf.Lerp(lerpMiddleAxilScale, -middleAxilScale*2, transformingSpeed*1f * Time.deltaTime);
                middleAxilCurrentScale = lerpMiddleAxilScale;
                FLAxilMidddlepart.localScale = new Vector3(FLAxilMidddlepart.localScale.x, middleAxilCurrentScale, FLAxilMidddlepart.localScale.z);
                FRAxilMiddlePart.localScale = new Vector3(FRAxilMiddlePart.localScale.x, middleAxilCurrentScale, FRAxilMiddlePart.localScale.z);
                RLAxilMiddlepart.localScale = new Vector3(RLAxilMiddlepart.localScale.x, middleAxilCurrentScale, RLAxilMiddlepart.localScale.z);
                RRAxilMiddlepart.localScale = new Vector3(RRAxilMiddlepart.localScale.x, middleAxilCurrentScale, RRAxilMiddlepart.localScale.z);
            }
            else
            {
                FLAxilMidddlepart.localScale = new Vector3(FLAxilMidddlepart.localScale.x, middleAxilScale, FLAxilMidddlepart.localScale.z);
                FRAxilMiddlePart.localScale = new Vector3(FRAxilMiddlePart.localScale.x, middleAxilScale, FRAxilMiddlePart.localScale.z);
                RLAxilMiddlepart.localScale = new Vector3(RLAxilMiddlepart.localScale.x, middleAxilScale, RLAxilMiddlepart.localScale.z);
                RRAxilMiddlepart.localScale = new Vector3(RRAxilMiddlepart.localScale.x, middleAxilScale, RRAxilMiddlepart.localScale.z);
            }
            //transform MiddleAxil in y direction......
            if (middleAxilYCurrent_Value < middleAxilY_Value)
            {
                LerpMiddleAxilY_Value = Mathf.Lerp(LerpMiddleAxilY_Value, -middleAxilY_Value*2, transformingSpeed*1f * Time.deltaTime);
                middleAxilYCurrent_Value = LerpMiddleAxilY_Value;
                FLAxilMidddlepart.localPosition = new Vector3(FLAxilMidddlepart.localPosition.x, middleAxilYCurrent_Value, FLAxilMidddlepart.localPosition.z);
                FRAxilMiddlePart.localPosition = new Vector3(FRAxilMiddlePart.localPosition.x, middleAxilYCurrent_Value, FRAxilMiddlePart.localPosition.z);
                RLAxilMiddlepart.localPosition = new Vector3(RLAxilMiddlepart.localPosition.x, middleAxilYCurrent_Value, RLAxilMiddlepart.localPosition.z);
                RRAxilMiddlepart.localPosition = new Vector3(RRAxilMiddlepart.localPosition.x, middleAxilYCurrent_Value, RRAxilMiddlepart.localPosition.z);
            }
            else
            {
                FLAxilMidddlepart.localPosition = new Vector3(FLAxilMidddlepart.localPosition.x, middleAxilY_Value, FLAxilMidddlepart.localPosition.z);
                FRAxilMiddlePart.localPosition = new Vector3(FRAxilMiddlePart.localPosition.x, middleAxilY_Value, FRAxilMiddlePart.localPosition.z);
                RLAxilMiddlepart.localPosition = new Vector3(RLAxilMiddlepart.localPosition.x, middleAxilY_Value, RLAxilMiddlepart.localPosition.z);
                RRAxilMiddlepart.localPosition = new Vector3(RRAxilMiddlepart.localPosition.x, middleAxilY_Value, RRAxilMiddlepart.localPosition.z);
            }
            //scale LowerPart axil in y direction..
            if (lowerAxilCurrentScale > lowerAxilScale)
            {
                LerpLowerAxilScale = Mathf.Lerp(LerpLowerAxilScale, -lowerAxilScale*2, transformingSpeed * Time.deltaTime);
                lowerAxilCurrentScale = LerpLowerAxilScale;
                FLAxilLowerPart.localScale = new Vector3(FLAxilLowerPart.localScale.x, lowerAxilCurrentScale, FLAxilLowerPart.localScale.z);
                FRAxilLowerpart.localScale = new Vector3(FRAxilLowerpart.localScale.x, lowerAxilCurrentScale, FRAxilLowerpart.localScale.z);
                RLAxilLowerpart.localScale = new Vector3(RLAxilLowerpart.localScale.x, lowerAxilCurrentScale, RLAxilLowerpart.localScale.z);
                RRAxilLowerpart.localScale = new Vector3(RRAxilLowerpart.localScale.x, lowerAxilCurrentScale, RRAxilLowerpart.localScale.z);
            }
            else
            {
                FLAxilLowerPart.localScale = new Vector3(FLAxilLowerPart.localScale.x, lowerAxilScale, FLAxilLowerPart.localScale.z);
                FRAxilLowerpart.localScale = new Vector3(FRAxilLowerpart.localScale.x, lowerAxilScale, FRAxilLowerpart.localScale.z);
                RLAxilLowerpart.localScale = new Vector3(RLAxilLowerpart.localScale.x, lowerAxilScale, RLAxilLowerpart.localScale.z);
                RRAxilLowerpart.localScale = new Vector3(RRAxilLowerpart.localScale.x, lowerAxilScale, RRAxilLowerpart.localScale.z);
            }
            //transform LowerAxil in y direction......
            if (LowerAxilYCurrent_Value < LowerAxilY_Value)
            {
                LerpLowerAxilY_Value = Mathf.Lerp(LerpLowerAxilY_Value, LowerAxilY_Value*2, transformingSpeed * Time.deltaTime);
                LowerAxilYCurrent_Value = LerpLowerAxilY_Value;
                FLAxilLowerPart.localPosition = new Vector3(FLAxilLowerPart.localPosition.x, LowerAxilYCurrent_Value, FLAxilLowerPart.localPosition.z);
                FRAxilLowerpart.localPosition = new Vector3(FRAxilLowerpart.localPosition.x, LowerAxilYCurrent_Value, FRAxilLowerpart.localPosition.z);
                RLAxilLowerpart.localPosition = new Vector3(RLAxilLowerpart.localPosition.x, LowerAxilYCurrent_Value, RLAxilLowerpart.localPosition.z);
                RRAxilLowerpart.localPosition = new Vector3(RRAxilLowerpart.localPosition.x, LowerAxilYCurrent_Value, RRAxilLowerpart.localPosition.z);
            }
            else
            {
                FLAxilLowerPart.localPosition = new Vector3(FLAxilLowerPart.localPosition.x, LowerAxilY_Value, FLAxilLowerPart.localPosition.z);
                FRAxilLowerpart.localPosition = new Vector3(FRAxilLowerpart.localPosition.x, LowerAxilY_Value, FRAxilLowerpart.localPosition.z);
                RLAxilLowerpart.localPosition = new Vector3(RLAxilLowerpart.localPosition.x, LowerAxilY_Value, RLAxilLowerpart.localPosition.z);
                RRAxilLowerpart.localPosition = new Vector3(RRAxilLowerpart.localPosition.x, LowerAxilY_Value, RRAxilLowerpart.localPosition.z);
            }
            //transform JunctionPart in Y Direction
            if (currentJunctionPartY_Value < junctionPartY_Value)
            {
                LerpJunctionPartY_Value = Mathf.Lerp(LerpJunctionPartY_Value, junctionPartY_Value*2, transformingSpeed*2f * Time.deltaTime);
                currentJunctionPartY_Value = LerpJunctionPartY_Value;
                FLAxilJunctionPart.localPosition = new Vector3(FLAxilJunctionPart.localPosition.x, currentJunctionPartY_Value, FLAxilJunctionPart.localPosition.z);
                FRAxilJunctionpart.localPosition = new Vector3(FRAxilJunctionpart.localPosition.x, currentJunctionPartY_Value, FRAxilJunctionpart.localPosition.z);
                RLAxilJunctionpart.localPosition = new Vector3(RLAxilJunctionpart.localPosition.x, currentJunctionPartY_Value, RLAxilJunctionpart.localPosition.z);
                RRAxilJunctionpart.localPosition = new Vector3(RRAxilJunctionpart.localPosition.x, currentJunctionPartY_Value, RRAxilJunctionpart.localPosition.z);
            }
            else
            {
                FLAxilJunctionPart.localPosition = new Vector3(FLAxilJunctionPart.localPosition.x, junctionPartY_Value, FLAxilJunctionPart.localPosition.z);
                FRAxilJunctionpart.localPosition = new Vector3(FRAxilJunctionpart.localPosition.x, junctionPartY_Value, FRAxilJunctionpart.localPosition.z);
                RLAxilJunctionpart.localPosition = new Vector3(RLAxilJunctionpart.localPosition.x, junctionPartY_Value, RLAxilJunctionpart.localPosition.z);
                RRAxilJunctionpart.localPosition = new Vector3(RRAxilJunctionpart.localPosition.x, junctionPartY_Value, RRAxilJunctionpart.localPosition.z);

            }
            //transform wheel part in y direction....
            if (currentWheelPartY_Value < wheelPartY_Value)
            {
                lerpWheelPartY_Value = Mathf.Lerp(lerpWheelPartY_Value, junctionPartY_Value*2, transformingSpeed*1.5f * Time.deltaTime);
                currentWheelPartY_Value = lerpWheelPartY_Value;
                FLAxilWheelPart.localPosition = new Vector3(FLAxilWheelPart.localPosition.x, currentWheelPartY_Value, FLAxilWheelPart.localPosition.z);
                FRAxilWheelPart.localPosition = new Vector3(FRAxilWheelPart.localPosition.x, currentWheelPartY_Value, FRAxilWheelPart.localPosition.z);
                RLAxilWheelpart.localPosition = new Vector3(RLAxilWheelpart.localPosition.x, currentWheelPartY_Value, RLAxilWheelpart.localPosition.z);
                RRAxilWheelpart.localPosition = new Vector3(RRAxilWheelpart.localPosition.x, currentWheelPartY_Value, RRAxilWheelpart.localPosition.z);
            }
            else
            {
                FLAxilWheelPart.localPosition = new Vector3(FLAxilWheelPart.localPosition.x, junctionPartY_Value, FLAxilWheelPart.localPosition.z);
                FRAxilWheelPart.localPosition = new Vector3(FRAxilWheelPart.localPosition.x, junctionPartY_Value, FRAxilWheelPart.localPosition.z);
                RLAxilWheelpart.localPosition = new Vector3(RLAxilWheelpart.localPosition.x, junctionPartY_Value, RLAxilWheelpart.localPosition.z);
                RRAxilWheelpart.localPosition = new Vector3(RRAxilWheelpart.localPosition.x, junctionPartY_Value, RRAxilWheelpart.localPosition.z);
            }
            //suspension
            if (currentSuspension > 0.15f)
            {
                LerpSuspension = Mathf.Lerp(LerpSuspension, -2f, transformingSpeed * Time.deltaTime);
                currentSuspension = LerpSuspension;
                playerCarController.FrontLeftWheelCollider.wheelCollider.suspensionDistance = currentSuspension;
                playerCarController.FrontRightWheelCollider.wheelCollider.suspensionDistance = currentSuspension;
                playerCarController.RearLeftWheelCollider.wheelCollider.suspensionDistance = currentSuspension;
                playerCarController.RearRightWheelCollider.wheelCollider.suspensionDistance = currentSuspension;
            }
            else
            {
                playerCarController.FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.15f;
                playerCarController.FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.15f;
                playerCarController.RearLeftWheelCollider.wheelCollider.suspensionDistance = 0.15f;
                playerCarController.RearRightWheelCollider.wheelCollider.suspensionDistance = 0.15f;
                NormalMode = true;
                SwitchBTN.SetActive(true);
                setNormalModeSettinngs();
                ShowObjects();
                canRotate = false;
                ObjectVisible();
                GearSlider.value = 0f;
                GearSlider.value = SliderValue;
				if (currentLevel == 4)
                {
					if (level4stepscounter == 3) 
                    {
						//level4stepscounter = 4;
						//Debug.Log ("step 1");
						//ShowRotateBTN = false;
						//showSwitchBTN = false;
						//switchHighlightingBTN.SetActive (false);
						//SwitchBTN.SetActive (false);
					}
				}

            }
        }
        if (ShowTutorial)
        {
			if (applyBrakes) 
            {
				playerCarController.GetComponent<Rigidbody> ().drag = 5f;
			} 
        }
        /////////////////////////
        // For PArking
        if (!MissionCompletebool)
        {
            if (PlayerScript)
            {
                if (PlayerScript.OnParkingPoint)
                {
                    if (!ProgressBar.activeInHierarchy)
                    {
                        ProgressBar.SetActive(true);
                        Progress = 0;
                    }
                    if (Progress < 100f)
                    {
                        Progress += Time.timeScale * 5f;
                        ParkingProgressBarImage.fillAmount = Progress / 100;
                        ParkingProgressBarText.text = ((int)Progress).ToString() + " <size=24><b>%</b></size>";
                    }
                    else if (Progress >= 100f)
                    {
						
//						if (PlayerScript.GetComponent<ParkingCollider> ().Completeparticle != null)
//							PlayerScript.GetComponent<ParkingCollider> ().Completeparticle.SetActive (true);
                        ParkingProgressBarText.text = "100 <size=24><b>%</b></size>";
                        if (CurrentParkingArea < ParkingArea.Length - 1)
                        {
                            PlayerScript.OnParkingPoint = false;
                            ProgressBar.SetActive(false);
											
                            ParkingArea[CurrentParkingArea].SetActive(false);
                            CurrentParkingArea++;
                            ParkingArea[CurrentParkingArea].SetActive(true);
                            ArrowScript.gameObject.SetActive(true);
                            if (LevlesScript[currentLevel].useHurdels)
                            {
                                LevlesScript[currentLevel].buses[CurrentParkingArea - 1].enabled = true;
                            }
                        }
                        else
                        {
                            if (!MissionCompletebool)
                            {
                                MissionCompletebool = true;                              
                                ProgressBar.SetActive(false);
                                PlayerScript.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                                PlayerScript.GetComponent<RCC_CarControllerV3>().engineRunning = false;
//                                HideObjects();
                                ObjectVisible();
                               
                                if (PlayerPrefs.GetInt("Parking") == 1)
                                {
                                    Debug.Log("THISLEVEL" + currentLevel);
                                    if(currentLevel == 22)
                                    {
                                        if (PlayerPrefs.GetInt("Giftcar1") == 1)
                                        {                                                                                   
                                            GetCar[0].SetActive(false);      //Gift
                                            Invoke("MissionComplete", 2f);
                                        }
                                        else
                                        {                                 
                                            GetCarbtn();                                      
                                        }
                                    }
                                    
                                   else if (currentLevel == 30)
                                   {
                                        Debug.Log("Levelfinish30");
                                        if (PlayerPrefs.GetInt("Getcar3") == 1)
                                        {                                          
                                            
                                            GetCar[3].SetActive(false);
                                            Invoke("MissionComplete", 2f);
                                        }
                                        else
                                        {
                                       
                                            GetCarbtn();                                          
                                        }
                                    }
                                    
                                   else if (currentLevel == 35)
                                   {
                                        if (PlayerPrefs.GetInt("Getcar4") == 1)
                                        {                                                                                 
                                            GetCar[4].SetActive(false);
                                            Invoke("MissionComplete", 2f);
                                        }
                                        else
                                        {                                          
                                            GetCarbtn();
                                        }
                                    }
                                   
                                    else if (currentLevel == 40)
                                   {
                                        if (PlayerPrefs.GetInt("Getcar5") == 1)
                                        {
                                            GetCar[5].SetActive(false);
                                            Invoke("MissionComplete", 2f);
                                        }
                                        else
                                        {                                        
                                            GetCarbtn();
                                        }
                                    }
                                    
                                   else if (currentLevel == 45)
                                   {
                                        if (PlayerPrefs.GetInt("Getcar6") == 1)
                                        {
                                            GetCar[6].SetActive(false);
                                            Invoke("MissionComplete", 2f);
                                        }
                                        else
                                        {
                                            GetCarbtn();
                                        }
                                    }
                                    
                                    else if (currentLevel == 50)
                                    {
                                        if (PlayerPrefs.GetInt("Giftcar7") == 1)
                                        {
                                            GetCar[7].SetActive(false);            //Gift
                                            Invoke("MissionComplete", 2f);
                                        }
                                        else
                                        {
                                            GetCarbtn();
                                        }
                                    }
                                   else 
                                    {
                                        MissionCompletebool = true;
                                        Debug.Log("FINISH*****");
                                        Invoke("MissionComplete", 2f);
                                    }

                                }

                                if (PlayerPrefs.GetInt("Elevated1") == 1)
                                {
                                    Debug.Log("THISLEVEL" + currentLevel);
                                    if (currentLevel == 3)
                                    {
                                        if (PlayerPrefs.GetInt("Giftcar14") == 1)
                                        {
                                            GetCar[2].SetActive(false);              //Gift
                                            Invoke("MissionComplete", 2f);
                                        }
                                        else
                                        {
                                           GetCarbtn();
                                        }
                                    }                                   

                                    else if (currentLevel == 8)
                                    {
                                        if (PlayerPrefs.GetInt("Getcar7") == 1)
                                        {                                           
                                            GetCar[7].SetActive(false);
                                            Invoke("MissionComplete", 2f);
                                        }
                                        else
                                        {                                          
                                            GetCarbtn();
                                        }
                                    }                               
                                   else  if (currentLevel == 14)
                                   {
                                        if (PlayerPrefs.GetInt("Getcar8") == 1)
                                        {                                           
                                            GetCar[8].SetActive(false);
                                            Invoke("MissionComplete", 2f);
                                        }
                                        else
                                        {                                           
                                            GetCarbtn();
                                        }

                                   }

                                  else
                                    {
                                        MissionCompletebool = true;
                                        Invoke("MissionComplete", 2f);
                                    }
                                }

                                if (PlayerPrefs.GetInt("Elevated2") == 1)
                                {
                                    Debug.Log("THISLEVEL" + currentLevel);
                                    if (currentLevel == 6)
                                    {
                                        if (PlayerPrefs.GetInt("Getcar9") == 1)
                                        {
                                            Debug.Log("Elevated2HERE");
                                            GetCar[9].SetActive(false);
                                            Invoke("MissionComplete", 2f);
                                        }
                                        else
                                        {
                                            Debug.Log("Elevated2HERE");
                                            GetCarbtn();
                                        }

                                    }
                                  
                                   else if (currentLevel == 12)
                                    {
                                        if (PlayerPrefs.GetInt("Getcar10") == 1)
                                        {

                                            GetCar[10].SetActive(false);
                                            Invoke("MissionComplete", 2f);
                                        }
                                        else
                                        {

                                            GetCarbtn();
                                        }

                                    }
                                    else
                                    {
                                        MissionCompletebool = true;                                      
                                        Invoke("MissionComplete", 2f);
                                    }

                                }
                               
                            }
                        }
                    }
                }
                else if (!PlayerScript.OnParkingPoint)
                {
                    if (ProgressBar.activeInHierarchy)
                    {
                        ProgressBar.SetActive(false);
                    }
                }
            }
            if (playerCarController) //for checking player direction
            {
                if (playerCarController.direction == -1)
                {
                    if (firstTimeGearChange)
                    {
                        rccCameraScript.target.localEulerAngles = new Vector3(0f, 0f, 0f);
                        
                        firstTimeGearChange = false;
                    }
                }
                else if (playerCarController.direction == 1)
                {
                    if (firstTimeGearChange)
                    {
                        rccCameraScript.target.localEulerAngles = new Vector3(0f, 180f, 0f);
                        
                        firstTimeGearChange = false;
                    }
                }
            }
            CalculateTime();
        }
    }
    public void SpawnPlayerVehicle()
    {
		if (PlayerPrefs.GetInt ("CurrentLevel") < 21) 
        {
            //Player = Instantiate (PlayerVehicles [PlayerPrefs.GetInt ("SelectedVehicle")], spawnPoint.position, spawnPoint.rotation);
            PlayerVehicles[PlayerPrefs.GetInt("SelectedVehicle")].SetActive(true);
            
            PlayerVehicles[PlayerPrefs.GetInt("SelectedVehicle")].transform.position = spawnPoint.position;
            PlayerVehicles[PlayerPrefs.GetInt("SelectedVehicle")].transform.rotation = spawnPoint.rotation;
            Player = PlayerVehicles[PlayerPrefs.GetInt("SelectedVehicle")];
            
            //FadeObj.SetActive (false);
		} 
        else 
        {
            //Player = Instantiate (ParkingPlayervehicle [PlayerPrefs.GetInt ("SelectedVehicle")], spawnPoint.position, spawnPoint.rotation);
            
            ParkingPlayervehicle[PlayerPrefs.GetInt("SelectedVehicle")].SetActive(true);
            ParkingPlayervehicle[PlayerPrefs.GetInt("SelectedVehicle")].transform.position = spawnPoint.position;
            ParkingPlayervehicle[PlayerPrefs.GetInt("SelectedVehicle")].transform.rotation = spawnPoint.rotation;
           
            Player = ParkingPlayervehicle[PlayerPrefs.GetInt("SelectedVehicle")];
           
            // FadeObj.SetActive (true);
            navArrowObj.SetActive (false);
			checkCarDirctnbol = true;
		}
        Player.name = "Player";
        playerCarController = Player.GetComponent<RCC_CarControllerV3>();
        PlayerScript = Player.GetComponent<PlayerCarScript>();
        PlayerRB=Player.GetComponent<Rigidbody>();
        PlayerScript.Parking1 = Parking1; //Assigning Color1
        PlayerScript.Parking2 = Parking2; //Assigning Color1
        PlayerScript.Parking3 = Parking3; //Assigning Color1
        PlayerScript.Parking4 = Parking4; //Assigning Color1
        camDis = Player.GetComponent<RCC_CameraConfig>().distance;
        currentCamDis = camDis;
        lerpCamDis = camDis;
        camHeight = Player.GetComponent<RCC_CameraConfig>().height;
        currentCamHeight = camHeight;
        lerpCamHeight = camHeight;
        PlayerScript.FLAxilpart.GetComponent<MeshRenderer>().enabled = false;
        PlayerScript.FRAxilpart.GetComponent<MeshRenderer>().enabled = false;
        PlayerScript.RRAxilpart.GetComponent<MeshRenderer>().enabled = false;
        PlayerScript.RLAxilpart.GetComponent<MeshRenderer>().enabled = false;
    }

     public void CameraON()
    {
        CamerScript.GetComponent<RCC_Camera>().playerCar = Player.transform;
        rccCameraScript.enabled = false;
        PlayerCamera.GetComponent<RCC_Camera>().enabled = true;
    }
    public void Suspension_toggleBtn()
    {
        
        TrafficMode = !TrafficMode;
        canRotate = false;
        ClearInfo();
        LeftRightSteerBtns.SetActive(false);
		playerCarController.GetComponent<Rigidbody> ().drag = 0.05f;
        if (TrafficMode)
        {
            CEnterOfMass.transform.position += new Vector3(0f, -2.09f, 0f);
            changeCamHeight = true;
            FLAxilWheelPart.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            FRAxilWheelPart.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            RLAxilWheelpart.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            RRAxilWheelpart.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            PlayerScript.FLAxilpart.GetComponent<MeshRenderer>().enabled = true;
            PlayerScript.FRAxilpart.GetComponent<MeshRenderer>().enabled = true;
            PlayerScript.RRAxilpart.GetComponent<MeshRenderer>().enabled = true;
            PlayerScript.RLAxilpart.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            CEnterOfMass.transform.position -= new Vector3(0f, -2.09f, 0f);
            DisableBarColliders();
            changeCamHeight = false;
            PlayerRB.constraints = RigidbodyConstraints.None;
        }
        if (!showBTNs)
        {
            ObjectInvisible();
            HideObjects();
        }
       
        SliderValue = GearSlider.value;
        if (ShowTutorial)
        {
            canSwitchHBTN = false;
        }
		if (currentLevel == 4) 
        {
          Debug.Log("HighlightSwitch");
        
          switchHighlightingBTN.SetActive (false);

        }
    }
    public void setTrafficModeSettinngs()
    {
        if (!showBTNs)
        {
            playerCarController.engineTorque -= 1000f;
            playerCarController.maxEngineRPM -= 2000f;
            playerCarController.maxspeed -= 50f;
        }
        playerCarController.orgSteerAngle = 20f;
    }
    public void setNormalModeSettinngs()
    {
        if (!AiLevel)
        {
            playerCarController.engineTorque += 1000f;
            playerCarController.maxEngineRPM += 2000f;
            playerCarController.maxspeed += 50f;
        }
       playerCarController.orgSteerAngle = 40f;
       FLAxilWheelPart.GetChild(0).GetComponent<BoxCollider>().enabled = false;
       FRAxilWheelPart.GetChild(0).GetComponent<BoxCollider>().enabled = false;
       RLAxilWheelpart.GetChild(0).GetComponent<BoxCollider>().enabled = false;
       RRAxilWheelpart.GetChild(0).GetComponent<BoxCollider>().enabled = false;
        PlayerScript.FLAxilpart.GetComponent<MeshRenderer>().enabled = false;
        PlayerScript.FRAxilpart.GetComponent<MeshRenderer>().enabled = false;
        PlayerScript.RRAxilpart.GetComponent<MeshRenderer>().enabled = false;
        PlayerScript.RLAxilpart.GetComponent<MeshRenderer>().enabled = false;
    }

   
    public GameObject Gear, Handbreak;
    public void showRotateWheels()
    {
        RotateWheelsBTN.GetComponent<Button>().interactable = true;
        RotateHighlightingBTN.SetActive(true);
    }
    public void NoshowRotateWheels()
    {
        RotateWheelsBTN.GetComponent<Button>().interactable = false;
        RotateHighlightingBTN.SetActive(false);
        Invoke("showRotateWheels", 1f);
    }
    public Button LeftSteer, RightSteer;
    public GameObject steering;
    public void RotateWheelsBtn()
    {
        StartRotatingWheels = !StartRotatingWheels;
        if (!StartRotatingWheels)
        {
            NoshowRotateWheels();
            showSwitchBTN = false;           
            SwitchBTN.SetActive(false);
            playerCarController.orgSteerAngle =40f;
            stopRotatingWheels = false;
            ObjectVisible();
            ShowObjects();
            Invoke("StartWheelsRotating",1f);
            ButtonFalse();
            LeftRightSteerBtns.SetActive(false);
            PlayerRB.constraints = RigidbodyConstraints.None;
            FLCollider.GetComponent<RCC_WheelCollider>().canRotateWheel = true;
            FRCollider.GetComponent<RCC_WheelCollider>().canRotateWheel = true;
            RLCollider.GetComponent<RCC_WheelCollider>().canRotateWheel = true;
            RRCollider.GetComponent<RCC_WheelCollider>().canRotateWheel = true;
            FLW.transform.localEulerAngles = FLW.transform.localEulerAngles;
            FRW.transform.localEulerAngles = FRW.transform.localEulerAngles;
            RLW.transform.localEulerAngles = RLW.transform.localEulerAngles;
            RRW.transform.localEulerAngles = RRW.transform.localEulerAngles;
            GearSlider.value = 0f;
            GearSlider.value = SliderValue;
            accelerateionBTN.GetComponent<RCC_UIController>().pressing = true;
            accelerateionBTN.GetComponent<RCC_UIController>().pressing = false; 
			if (currentLevel == 4) 
            {
                //ShowRotateBTN = false;
                if (level4stepscounter == 2) 
                {
					//level4stepscounter = 3;
					//Debug.Log ("step 1");
					//ShowRotateBTN = false;
					//showSwitchBTN = true;
					//switchHighlightingBTN.SetActive (true);
					//SwitchBTN.SetActive (true);
				}
			}
        }
        else
        {
            NoshowRotateWheels();
            SliderValue = GearSlider.value;
            currentDirection = playerCarController.direction;
            //PlayerRB.constraints = RigidbodyConstraints.FreezePositionY;
            PlayerRB.constraints = RigidbodyConstraints.FreezeRotation;
            showSwitchBTN = true;
            SwitchBTN.SetActive(true);
            playerCarController.orgSteerAngle=90f;
            RotateWheels = true;
            // HideObjects();
            settingsScript.useSteeringWheelForSteering = false;
			Gear.SetActive(false);
            Handbreak.SetActive(false);
            ObjectInvisible();
            LeftRightSteerBtns.SetActive(true);
            //FLAxilWheelPart.GetChild(0).localEulerAngles = new Vector3(0f, 0f, 0f);
            //FRAxilWheelPart.GetChild(0).localEulerAngles = new Vector3(0f, 0f, 0f);
            //RLAxilWheelpart.GetChild(0).localEulerAngles = new Vector3(0f, 0f, 0f);
            //RRAxilWheelpart.GetChild(0).localEulerAngles = new Vector3(0f, 0f, 0f);
            FLCollider.GetComponent<RCC_WheelCollider>().canRotateWheel = false;
            FRCollider.GetComponent<RCC_WheelCollider>().canRotateWheel = false;
            RLCollider.GetComponent<RCC_WheelCollider>().canRotateWheel = false;
            RRCollider.GetComponent<RCC_WheelCollider>().canRotateWheel = false;
            FLW.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
            FRW.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            RLW.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
            RRW.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            
			if (currentLevel == 4) 
            {
				if (level4stepscounter == 1) 
                {
				//	level4stepscounter = 2;				
				//	ShowRotateBTN = false;
				//	showSwitchBTN = false;
				//	SwitchBTN.SetActive (false);
				//	RotateHighlightingBTN.SetActive (false);
				//	LeftHighlightingBar.SetActive (true);
				}
			}

        }
    }

    public void ButtonFalse()
    {
        LeftSteer.GetComponent<RCC_UIController>().enabled = false;
        steering.GetComponent<RCC_UISteeringWheelController>().enabled = false;
        RightSteer.GetComponent<RCC_UIController>().enabled = false;
        Invoke("ButtonTrue",1f);
    }
    public void ButtonTrue()
    {
        LeftSteer.GetComponent<RCC_UIController>().enabled = true;
        RightSteer.GetComponent<RCC_UIController>().enabled = true;
        steering.GetComponent<RCC_UISteeringWheelController>().enabled = true;
    }
   

    public void StartWheelsRotating()
    {
        RotateWheels = false;
    }

    public void GetValues()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") >= 21)
        {
            rccCameraScript.target = NewTarget;
            changeCamHeight = true;
        }
        else
        {
            rccCameraScript.target = PlayerScript.CameraTarget;
        }
        gameObject.GetComponent<CameraRotate>().CarCamObj = rccCameraScript.target;
        rccCameraScript.distance = Player.GetComponent<RCC_CameraConfig>().distance;
        rccCameraScript.height = Player.GetComponent<RCC_CameraConfig>().height;
        FLCollider = playerCarController.FrontLeftWheelCollider.transform;
        FRCollider = playerCarController.FrontRightWheelCollider.transform;
        RLCollider = playerCarController.RearLeftWheelCollider.transform;
        RRCollider = playerCarController.RearRightWheelCollider.transform;
        FLW = playerCarController.FrontLeftWheelTransform;
        FRW = playerCarController.FrontRightWheelTransform;
        RLW = playerCarController.RearLeftWheelTransform;
        RRW = playerCarController.RearRightWheelTransform;
        OriginalWheelDistance = FLCollider.localPosition.x;
        CurrentDistance = OriginalWheelDistance;
        //Refrences....
        //FL wheel      
            FLAxil = PlayerScript.FLAxil;
            FLAxilMidddlepart = PlayerScript.FLAxilMidddlepart;
            FLAxilLowerPart = PlayerScript.FLAxilLowerPart;
            FLAxilJunctionPart = PlayerScript.FLAxilJunctionPart;
            FLAxilWheelPart = PlayerScript.FLAxilWheelPart;
            ///FR Wheel
            FRAxil = PlayerScript.FRAxil;
            FRAxilMiddlePart = PlayerScript.FRAxilMiddlePart;
            FRAxilLowerpart = PlayerScript.FRAxilLowerpart;
            FRAxilJunctionpart = PlayerScript.FRAxilJunctionpart;
            FRAxilWheelPart = PlayerScript.FRAxilWheelPart;
            //RL Wheel
            RLAxil = PlayerScript.RLAxil;
            RLAxilMiddlepart = PlayerScript.RLAxilMiddlepart;
            RLAxilLowerpart = PlayerScript.RLAxilLowerpart;
            RLAxilJunctionpart = PlayerScript.RLAxilJunctionpart;
            RLAxilWheelpart = PlayerScript.RLAxilWheelpart;
            //RR Wheel
            RRAxil = PlayerScript.RRAxil;
            RRAxilMiddlepart = PlayerScript.RRAxilMiddlepart;
            RRAxilLowerpart = PlayerScript.RRAxilLowerpart;
            RRAxilJunctionpart = PlayerScript.RRAxilJunctionpart;
            RRAxilWheelpart = PlayerScript.RRAxilWheelpart;
            CEnterOfMass = playerCarController.COM;
            //.....................
            //setting VAlues...............
            middleAxilScale = PlayerScript.FLAxilMidddlepart.localScale.y;
            lowerAxilScale = PlayerScript.FLAxilLowerPart.localScale.y;
            axilX_Value = FLAxil.localPosition.x;
            LerpAxilX_Value = axilX_Value;
            currentAxilX_Value = axilX_Value;
            axilY_Value = FLAxil.localPosition.y;
            LerpAxilY_Value = axilY_Value;
            currentAxilY_Value = axilY_Value;
            TotalWheelDistance += OriginalWheelDistance;
            lerpMiddleAxilScale = middleAxilScale;
            middleAxilCurrentScale = middleAxilScale;
            LerpLowerAxilScale = lowerAxilScale;
            lowerAxilCurrentScale = lowerAxilScale;
            junctionPartY_Value = PlayerScript.FLAxilJunctionPart.localPosition.y;
            LerpJunctionPartY_Value = junctionPartY_Value;
            currentJunctionPartY_Value = junctionPartY_Value;
            wheelPartY_Value = PlayerScript.FLAxilWheelPart.localPosition.y;
            currentWheelPartY_Value = wheelPartY_Value;
            lerpWheelPartY_Value = wheelPartY_Value;
            FLWC = FLCollider.GetComponent<RCC_WheelCollider>();
            FRWC = FRCollider.GetComponent<RCC_WheelCollider>();
            RLWC = RLCollider.GetComponent<RCC_WheelCollider>();
            RRWC = RRCollider.GetComponent<RCC_WheelCollider>();    
    }

    public void RestartScene()
    {  
        hidebanners();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }

    public void Showad_inGplay()
    {
        if (showads)
        {
            if (Adobj != null)
            {
                Adobj.ShowInterstitial();
            }
        }
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
		Analytics.CustomEvent ("Level" + PlayerPrefs.GetInt ("DifficultyLevel").ToString () + PlayerPrefs.GetInt ("CurrentLevel").ToString ());
    }

    public void PauseButton(GameObject GameObject)
    {
        showbanners();     
        Time.timeScale = 0;
        GameObject.SetActive(true);
        HideObjects();
        ObjectInvisible();
    }

    public void ResumeButton(GameObject GameObject)
    {
        Time.timeScale = 1;
        hidebanners();
        Adnotav.SetActive(false);
        GameObject.SetActive(false);
        AudioListener.volume = 1;
        ObjectVisible();
        ShowObjects();
        if (DifficultyLevel > 0)
        {
			TimePanel.SetActive(false);             //True
        }
    }

    public void LoadCustomization()
    {
        hidebanners();        
        Analytics.CustomEvent("Level_Comp" + PlayerPrefs.GetInt("DifficultyLevel").ToString() + PlayerPrefs.GetInt("CurrentLevel").ToString());
        settingScript.PauseSounds();
        GameFinished = true;
        HideObjects();
        ObjectInvisible();        
        if (currentLevel == LevlesScript.Length - 1)
        {
            NextBTN.SetActive(false);
        }
        else
        {
            UnlockNextLevel();
        }
       
        CalculatePoints();
        PlayerPrefs.SetInt("NowShow", PlayerPrefs.GetInt("NowShow") + 1);
 
        if (DifficultyLevel == 0)
        {
            if (PlayerPrefs.GetInt("FirstAttempt" + currentLevel) == 0)
            {
                CustomAnalytics.eventMessage("elevateddriving" + (currentLevel) + "_clear_1stAttempt");
                PlayerPrefs.SetInt("FirstAttempt" + currentLevel, 1);
            }
            CustomAnalytics.eventMessage("elevateddriving" + (currentLevel) + "_clear_overall");
        }
        else if (DifficultyLevel == 1)
        {
            if (PlayerPrefs.GetInt("FirstAttempt" + currentLevel) == 0)
            {
                CustomAnalytics.eventMessage("hardelevation" + (currentLevel) + "_clear_1stAttempt");
                PlayerPrefs.SetInt("FirstAttempt" + currentLevel, 1);
            }
            CustomAnalytics.eventMessage("hardelevation" + (currentLevel) + "_clear_overall");
        }
        else if (DifficultyLevel == 3)
        {
            if (PlayerPrefs.GetInt("FirstAttempt" + currentLevel) == 0)
            {
                CustomAnalytics.eventMessage("parking" + (currentLevel - 20) + "_clear_1stAttempt");
                PlayerPrefs.SetInt("FirstAttempt" + currentLevel, 1);
            }
            CustomAnalytics.eventMessage("parking" + (currentLevel - 20) + "_clear_overall");
        }
        PlayerPrefs.SetInt("customiz", 1);       
        LoadingPanel.SetActive(true);
        SceneManager.LoadScene(1);
        
    }
    public void LoadMAinMenu()
    {
        hidebanners();
        //int rand = Random.Range(0, 3);
        //LoadingImage.sprite = LoadingIMG[rand];
        SceneManager.LoadScene(1);
    }
    public void HideObjects()
    {
        for (int i = 0; i < OptionsObjects.Length; i++)
        {
            //OptionsObjects[i].SetActive(false);
            OptionsObjects[0].SetActive(false);
            OptionsObjects[1].SetActive(false);
            //OptionsObjects[2].SetActive(false);
            OptionsObjects[3].SetActive(false);
            OptionsObjects[4].SetActive(false);
            OptionsObjects[5].SetActive(false);
        }
        settingsScript.useSteeringWheelForSteering = false;
    }

    public void ShowObjects()
    {
        for (int i = 0; i < OptionsObjects.Length; i++)
        {
            //OptionsObjects[i].SetActive(true);
            OptionsObjects[0].SetActive(true);
            OptionsObjects[1].SetActive(true);
            //OptionsObjects[2].SetActive(true);
            OptionsObjects[3].SetActive(true);
            OptionsObjects[4].SetActive(true);
            OptionsObjects[5].SetActive(true);
        }
        if (useSteeringWheel)
        {
            settingsScript.useSteeringWheelForSteering = true;
        }
    }
    public void SetControls()
    {
        if (settingsScript.useSteeringWheelForSteering)
        {
            useSteeringWheel = true;
        }  
    }
    public void EnableBarColliders()
    {
        FLAxilLowerPart.GetComponent<CapsuleCollider>().enabled = true;
        FRAxilLowerpart.GetComponent<CapsuleCollider>().enabled = true;
        RLAxilLowerpart.GetComponent<CapsuleCollider>().enabled = true;
        FRAxilLowerpart.GetComponent<CapsuleCollider>().enabled = true;
    }
    public void DisableBarColliders()
    {
        FLAxilLowerPart.GetComponent<CapsuleCollider>().enabled = false;
        FRAxilLowerpart.GetComponent<CapsuleCollider>().enabled = false;
        RLAxilLowerpart.GetComponent<CapsuleCollider>().enabled = false;
        FRAxilLowerpart.GetComponent<CapsuleCollider>().enabled = false;
    }
    public void setEnviornment()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        DifficultyLevel = PlayerPrefs.GetInt("DifficultyLevel");
        NewTarget = LevlesScript[currentLevel].FirstCameraPosition;
        spawnPoint = LevlesScript[currentLevel].spawnPoint;
        ShowTutorial = LevlesScript[currentLevel].showTutorial;
        showSwitchBTN = LevlesScript[currentLevel].switchMode;
        ShowRotateBTN = LevlesScript[currentLevel].RotateMode;
        AiLevel = LevlesScript[currentLevel].aiLevel;
        showLevelBTNAllTime = LevlesScript[currentLevel].showLevelBTNAllTime;
		realparkingbool=LevlesScript [currentLevel].RealParkingMode;
        ParkingArea = new GameObject[LevlesScript[currentLevel].ParkingSlot.Length];
        for (int i = 0; i < ParkingArea.Length; i++)
        {
            ParkingArea[i] = LevlesScript[currentLevel].ParkingSlot[i];
        }
   
        if (AiLevel)
        {
            //DistancePanel.SetActive(true);
        }
        Invoke("handreverse", 8f);
        if (DifficultyLevel <= 0)
        {
            if (!AiLevel)
            {
                TimePanel.SetActive(false);         //True
            }
            if (currentLevel < 11)
            {
                hits = 15;
            }
            else
            {
                hits = 10;
            }
        }
        if (DifficultyLevel == 1)
        {
            timer = LevlesScript[currentLevel].ExtremeLevelTimer;
            hits = LevlesScript[currentLevel].extremeModeHits;
        }
        else if (DifficultyLevel == 2)
        {
            if (!AiLevel)
            {
                timer = LevlesScript[currentLevel].ProLevelTimer;
            }
            hits = LevlesScript[currentLevel].ProModeHits;
        }
		else if (DifficultyLevel == 3)
		{
			if (!AiLevel)
			{
				timer = LevlesScript[currentLevel].ProLevelTimer;
			}
			if (currentLevel >= 21 && currentLevel < 31)
            {
				hits = 10;
			}
            else if(currentLevel >= 31 && currentLevel < 41)
            {
				hits = 10;
			}
            else
            {
			    hits = LevlesScript[currentLevel].ProModeHits;
			}
		}
        
        //StartGame();
    }
    void handreverse()
    {
        if (currentLevel == 27)
        {
            HandReverse.SetActive(true);
            // Invoke("ReversehandOff", 2f);
        }
    }
    void ReversehandOff()
    {
        HandReverse.SetActive(false);
    }
    public void activateEnviornment()
    {
        SpawnPlayerVehicle();
        GetValues();
        
        LerpWheelDistance = CurrentDistance;
		if (currentLevel < 21)
        {
			LevlesScript [currentLevel].gameObject.SetActive (true);
			playerCarController.semiAutomaticGear = false;
		}
        else
        {
			playerCarController.semiAutomaticGear = false;
			playerCarController.maxspeed = 100f;
			LevlesScript [currentLevel].gameObject.SetActive (true);
			SceneManager.LoadScene (LevlesScript [currentLevel].level_name, LoadSceneMode.Additive);
		}
    }

   
     public void CarpanelOff()
    {
        showbanners();
        Analytics.CustomEvent("Level_Comp" + PlayerPrefs.GetInt("DifficultyLevel").ToString() + PlayerPrefs.GetInt("CurrentLevel").ToString());
        settingScript.PauseSounds();
        GameFinished = true;
        HideObjects();
        ObjectInvisible();
        MissionCompletePanel.SetActive(true);
        if (currentLevel == LevlesScript.Length - 1)
        {
            NextBTN.SetActive(false);
        }
        else
        {
            UnlockNextLevel();
        }

        Debug.Log("LevelNo=" + currentLevel);
        CalculatePoints();

        PlayerPrefs.SetInt("NowShow", PlayerPrefs.GetInt("NowShow") + 1);
        //if (currentLevel == 2 || currentLevel == 5 || currentLevel == 8)
        //{
        //    if (PlayerPrefs.GetInt("rated") == 0)
        //    {
        //        Rateus();
        //        hidebanners();
        //    }
        //}

        if (DifficultyLevel == 0)
        {
            if (PlayerPrefs.GetInt("FirstAttempt" + currentLevel) == 0)
            {
                CustomAnalytics.eventMessage("elevateddriving" + (currentLevel) + "_clear_1stAttempt");
                PlayerPrefs.SetInt("FirstAttempt" + currentLevel, 1);
            }
            CustomAnalytics.eventMessage("elevateddriving" + (currentLevel) + "_clear_overall");
        }
        else if (DifficultyLevel == 1)
        {
            if (PlayerPrefs.GetInt("FirstAttempt" + currentLevel) == 0)
            {
                CustomAnalytics.eventMessage("hardelevation" + (currentLevel) + "_clear_1stAttempt");
                PlayerPrefs.SetInt("FirstAttempt" + currentLevel, 1);
            }
            CustomAnalytics.eventMessage("hardelevation" + (currentLevel) + "_clear_overall");
        }
        else if (DifficultyLevel == 3)
        {
            if (PlayerPrefs.GetInt("FirstAttempt" + currentLevel) == 0)
            {
                CustomAnalytics.eventMessage("parking" + (currentLevel - 20) + "_clear_1stAttempt");
                PlayerPrefs.SetInt("FirstAttempt" + currentLevel, 1);
            }
            CustomAnalytics.eventMessage("parking" + (currentLevel - 20) + "_clear_overall");
        }
    }
    public void MissionComplete()
    {
   
         showbanners(); 
        Analytics.CustomEvent ("Level_Comp" + PlayerPrefs.GetInt ("DifficultyLevel").ToString () + PlayerPrefs.GetInt ("CurrentLevel").ToString ());
        settingScript.PauseSounds();
        GameFinished = true;
        HideObjects();
        ObjectInvisible();
        MissionCompletePanel.SetActive(true);
        if (currentLevel == LevlesScript.Length - 1)
        {
            NextBTN.SetActive(false);
        }
        else
        {
            UnlockNextLevel();
        }
        
        Debug.Log("LevelNo=" + currentLevel);
        CalculatePoints();
        
		PlayerPrefs.SetInt ("NowShow", PlayerPrefs.GetInt ("NowShow") + 1);
		//if (currentLevel == 2 || currentLevel == 5 || currentLevel == 8) 
  //      {
		//	if (PlayerPrefs.GetInt ("rated") == 0)
  //          {
		//		Rateus ();
  //              hidebanners();
		//	}
		//}

		if (DifficultyLevel == 0) 
        {
			if (PlayerPrefs.GetInt ("FirstAttempt" + currentLevel) == 0) 
            {
				CustomAnalytics.eventMessage ("elevateddriving" + (currentLevel) + "_clear_1stAttempt");
				PlayerPrefs.SetInt ("FirstAttempt" + currentLevel, 1);
			}
			CustomAnalytics.eventMessage ("elevateddriving" + (currentLevel) + "_clear_overall");
		}
        else if (DifficultyLevel == 1)
        {
			if (PlayerPrefs.GetInt ("FirstAttempt" + currentLevel) == 0) 
            {
				CustomAnalytics.eventMessage ("hardelevation" + (currentLevel) + "_clear_1stAttempt");
				PlayerPrefs.SetInt ("FirstAttempt" + currentLevel, 1);
			}
			CustomAnalytics.eventMessage ("hardelevation"+(currentLevel)+"_clear_overall");
		}
        else if (DifficultyLevel == 3)
        {
			if (PlayerPrefs.GetInt ("FirstAttempt" + currentLevel) == 0) 
            {
				CustomAnalytics.eventMessage ("parking" + (currentLevel-20) + "_clear_1stAttempt");
				PlayerPrefs.SetInt ("FirstAttempt" + currentLevel, 1);
			}
			CustomAnalytics.eventMessage ("parking"+(currentLevel-20)+"_clear_overall");
		}
    }
    public void UnlockNextLevel()
    {
        int currentModeLevel;
        if (DifficultyLevel == 0)
        {
            Debug.Log("Unlock");
            currentModeLevel = currentLevel;
            if (currentLevel < LevlesScript.Length - 1)
            {
                if (currentModeLevel + 1 > PlayerPrefs.GetInt("UnlockedSimpleLevels"))
                {
                    PlayerPrefs.SetInt("UnlockedSimpleLevels", currentModeLevel + 1);
                    Debug.Log("" + PlayerPrefs.GetInt("UnlockedSimpleLevels"));
                }
            }
        }
        if (DifficultyLevel == 1)
        {
            currentModeLevel = currentLevel-2;
            if (currentLevel < LevlesScript.Length - 1)
            {
                if (currentModeLevel + 1 > PlayerPrefs.GetInt("UnlockedExtremeLevels"))
                {
                    PlayerPrefs.SetInt("UnlockedExtremeLevels", currentModeLevel + 1);
                }
            }
        }
        if (DifficultyLevel == 2)
        {
            currentModeLevel = currentLevel - 2;
            if (currentLevel < LevlesScript.Length - 1)
            {
                if (currentModeLevel + 1 > PlayerPrefs.GetInt("UnlockedProLevels"))
                {
                    PlayerPrefs.SetInt("UnlockedProLevels", currentModeLevel + 1);
                }
            }
        }
		if (DifficultyLevel == 3)
		{
			currentModeLevel = currentLevel - 2;
			if (currentLevel < LevlesScript.Length - 1)
			{
				if (currentModeLevel + 1 > PlayerPrefs.GetInt("UnlockedParkingLevels"))
				{
					PlayerPrefs.SetInt("UnlockedParkingLevels", currentModeLevel + 1);
				}
			}
		}
    }

    public void CalculatePoints()
    {
        if (PlayerPrefs.GetInt("Parking") == 1)
        {
            if (currentLevel >= 21 && currentLevel <= 36)
            {
                points += 5000;
                if (timer > 15f)
                {
                    points += (int)timer;
                }
                PlayerPrefs.SetInt("TotalPoints", PlayerPrefs.GetInt("TotalPoints") + points);
                TotalPoints.text = (PlayerPrefs.GetInt("TotalPoints")).ToString();
                currentPoints.text = (points).ToString();
            }
            if (currentLevel >= 37 && currentLevel <= 50)
            {
                points += 10000;
                if (timer > 15f)
                {
                    points += (int)timer;
                }
                PlayerPrefs.SetInt("TotalPoints", PlayerPrefs.GetInt("TotalPoints") + points);
                TotalPoints.text = (PlayerPrefs.GetInt("TotalPoints")).ToString();
                currentPoints.text = (points).ToString();
            }
        }
        if (PlayerPrefs.GetInt("Elevated1") == 1)
        {
            if (currentLevel >= 1 && currentLevel <= 20)
            {
                points += 15000;
                if (timer > 15f)
                {
                    points += (int)timer;
                }
                PlayerPrefs.SetInt("TotalPoints", PlayerPrefs.GetInt("TotalPoints") + points);
                TotalPoints.text = (PlayerPrefs.GetInt("TotalPoints")).ToString();
                currentPoints.text = (points).ToString();
            }
        }
        if (PlayerPrefs.GetInt("Elevated2") == 1)
        {
            if (currentLevel >= 3 && currentLevel <= 20)
            {
                points += 25000;
                if (timer > 15f)
                {
                    points += (int)timer;
                }
                PlayerPrefs.SetInt("TotalPoints", PlayerPrefs.GetInt("TotalPoints") + points);
                TotalPoints.text = (PlayerPrefs.GetInt("TotalPoints")).ToString();
                currentPoints.text = (points).ToString();
            }
        }


    }
    public void StartTutorial(int tutorialno)
    {
        tutorialNo = tutorialno;
        
        applyBrakes = true;
        Invoke("ResetApplyBrakes", 3f);
        HideObjects();
        ObjectInvisible();
        if (tutorialNo == 0)
        {
            Debug.Log("MoveForwardCameraPosition");
			SkipButton.SetActive (false);
            tutorialBtn.SetActive(true);
            PlayerCamera.transform.parent = LevlesScript[currentLevel].tutCutScene[tutorialNo].transform;
            LevlesScript[currentLevel].tutCutScene[tutorialNo].transform.position = PlayerCamera.transform.position;
            LevlesScript[currentLevel].tutCutScene[tutorialNo].transform.rotation = PlayerCamera.transform.rotation;
            PlayerCamera.enabled = false;
            PlayerCamera.transform.localPosition = Vector3.zero;
            PlayerCamera.transform.localEulerAngles = Vector3.zero;
            LevlesScript[currentLevel].tutCutScene[tutorialNo].enabled = true;
            TutorialPAnel.SetActive(true);
            tutorialLabel.text = "ROAD BLOCKED";
            Invoke("resetCarPos", 23f);
        }
        if (tutorialNo == 1)
        {

            canSwitchHBTN = true;
        }
     
       
    }
    public void resetCarPos()
    {
     
		SkipButton.SetActive (false);     
    }
    public void EndTutorial()
    {

        PlayerCamera.transform.parent = null;
        PlayerCamera.enabled = true;
        if (tutorialNo == 0)
        {
            showSwitchBTN = true;
            canSwitchHBTN = true;
            TutorialPAnel.SetActive(false);
        }
        if (tutorialNo == 1)
        {
        }
    }
    public void ResetApplyBrakes()
    {
        applyBrakes = false;
		playerCarController.GetComponent<Rigidbody> ().drag = 0.05f;
        StartMovingCar = false;
    }
    public void ObjectInvisible()
    {
        for (int i = 0; i < invisibleObjects.Length; i++)
        {
           
            if (i != 0)
           {
               invisibleObjects[i].GetComponent<RCC_UIController>().CanBePressed = false;
                
           }
            invisibleObjects[i].enabled = false;
            invisibleObjects[i].GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            ReverseText.color = new Color(0f, 0f, 0f, 0f);          
        }
  

    }
    public void ObjectVisible()
    {
        for (int i = 0; i < invisibleObjects.Length; i++)
        {
            if (i != 0)
            {
                invisibleObjects[i].GetComponent<RCC_UIController>().CanBePressed = true;
                
            }
            invisibleObjects[i].enabled = true;
            invisibleObjects[i].GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            ReverseText.color = new Color(255f, 255f, 255f, 1f);           
        }
    
        Parkingtut.SetActive(false);

    }
    public void showInfo(int no)
    {
        if (no == 1)
        {
            if (NormalMode)
            {
                InfoText.text = "Road Blocked../n Switch Vehicle to Traffic Mode.";
            }
            else
            {
                InfoText.text = "";
            }            
        }    
    }
    public void ClearInfo()
    {
        InfoText.text = "";
    }
    public void steerLeftBTN()
    {
        moveLeft = true;
        showSwitchBtn = false;
    }
    public void cancelsteerLeftBTN()
    {
		
        moveLeft = false;
        showSwitchBtn = true;

    }
    public void SteerRightBTN()
    {
        showSwitchBtn = false;
        MoveRight = true;
    }
    public void cancelSteerRightBTN()
    {
		
        MoveRight = false;
        showSwitchBtn = true;
    }
   
    public void GetDamage()

    {
        if (currentLevel >= 30)
        {
            if (damageStart)
            {
                damageStart = false;
                Invoke("StartDamage", 1f);
                hits--;
                UpdateHits();
                Debug.Log("Damage");
                warningHitSound.Play();
                hitcarImg.enabled = true;
                hitcarImg.GetComponent<Image>().color = Color.red;
                Hittxt.enabled = true;
                Hittxt.GetComponent<Text>().color = Color.red;
                hitlablanim.enabled = true;
                hitlablanim.GetComponent<Text>().color = Color.red;
                if (hits <= 0)
                {
                    Player.GetComponent<Rigidbody>().isKinematic = true;
                    HitsFinished();
                }
            }
        }
    }
    void StartDamage()
    {
        damageStart = true;
		hitcarImg.enabled = false;
		hitcarImg.GetComponent<Image> ().color = Color.white;
		Hittxt.enabled = false;
		Hittxt.GetComponent<Text> ().color = Color.white;
		hitlablanim.enabled = false;
		hitlablanim.GetComponent<Text> ().color = Color.white;
    }

    public void MissionFail()
    {
        showbanners();
        HideObjects();
        ObjectInvisible();
        MissionFailPanel.SetActive(true);
        settingScript.PauseSounds();
		if (DifficultyLevel == 0) 
        {
			if (PlayerPrefs.GetInt ("FirstAttempt" + currentLevel) == 0) 
            {
				CustomAnalytics.eventMessage ("elevateddriving" + (currentLevel) + "_fail_1stAttempt");
				PlayerPrefs.SetInt ("FirstAttempt" + currentLevel, 1);
			}
			CustomAnalytics.eventMessage ("elevateddriving" + (currentLevel) + "_fail_overall");
		}
        else if (DifficultyLevel == 1)
        {
			if (PlayerPrefs.GetInt ("FirstAttempt" + currentLevel) == 0) 
            {
				CustomAnalytics.eventMessage ("hardelevation" + (currentLevel) + "_fail_1stAttempt");
				PlayerPrefs.SetInt ("FirstAttempt" + currentLevel, 1);
			}
			CustomAnalytics.eventMessage ("hardelevation"+(currentLevel)+"_fail_overall");
		}
        else if (DifficultyLevel == 3)
        {
			if (PlayerPrefs.GetInt ("FirstAttempt" + currentLevel) == 0) 
            {
				CustomAnalytics.eventMessage ("parking" + (currentLevel-20) + "_fail_1stAttempt");
				PlayerPrefs.SetInt ("FirstAttempt" + currentLevel, 1);
			}
			CustomAnalytics.eventMessage ("parking"+(currentLevel-20)+"_fail_overall");
		}
    }
    public void HitsFinished()
    {
        HideObjects();
        ObjectInvisible();
        HitsFinishedPanel.SetActive(true);
        settingScript.PauseSounds();
    }
    public void UpdateHits()
    {
        if (hits >= 0)
            hitsLabel.text = hits.ToString();
        else
            hitsLabel.text = "0";
    }
    public void checkMsg()
    {
        
        ShowHandFirstTime();
            if (AiLevel)
            {
                for (int i = 0; i < LevlesScript[currentLevel].Vehicles.Length; i++)
                {
                    LevlesScript[currentLevel].Vehicles[i].GetComponent<AICarController>().startTheCar();
                }
                LevlesScript[currentLevel].AIVehicle.GetComponent<AICarController>().startTheCar();
            }
        
    }
    public void StartGame()
    {    
        ShowObjects();
        ObjectVisible();
        
		if (AiLevel)
        {
            for (int i = 0; i < LevlesScript[currentLevel].Vehicles.Length; i++)
            {
                LevlesScript[currentLevel].Vehicles[i].GetComponent<AICarController>().startTheCar();
            }
            LevlesScript[currentLevel].AIVehicle.GetComponent<AICarController>().startTheCar();
        }
        if (DifficultyLevel > 0)
        {
			if (!AiLevel && PlayerPrefs.GetInt ("CurrentLevel") < 21)
				useTimer = true;
        }
		if (currentLevel == 4) 
        {
			//Suspension_toggleBtn ();
			ShowRotateBTN = false;
			showSwitchBtn = false;
		}
    }
    public void CalculateTime()
    {
        if (useTimer && !GameFinished)
        {
            timer -= Time.deltaTime;
            minutes = (int)timer / 60;
            seconds = (int)timer % 60;
            TimeShow.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            if (timer > 0)
            {
                if (seconds < 15 && minutes == 0 && TimeShow.color != Color.red)
                {
                    TimeShow.color = Color.red;
                }
                else if (timer > 15 && TimeShow.color != Color.green)
                {
                    TimeShow.color = Color.green;
                }
            }
            else
            {
                TimeFinished();
            }
        }
    }
    public void TimeFinished()
    {
        timeFinishedPanel.SetActive(true);
        useTimer = false;
        HideObjects();
        Player.GetComponent<Rigidbody>().isKinematic = true;
        settingScript.PauseSounds();
    }
    public void ExtraTimeBTN()
    {
        timer = 30f;
        useTimer = true;
        ShowObjects();
        Player.GetComponent<Rigidbody>().isKinematic = false;
        settingScript.ResumeSounds();
    }
    public void ExtraHitsBTN()
    {
        Debug.Log("Show Hits");
        hits = 3;
        MissionFailPanel.SetActive(false);
        ShowObjects();
        ObjectVisible();
        Player.GetComponent<Rigidbody>().isKinematic = false;
        UpdateHits();
        settingScript.ResumeSounds();
    }
    public void changeQualitySettings(int no)
    {
        QualitySettings.SetQualityLevel(no);
    }
    public void TutorialSkipBTN()
    {
        skiped = true;
        EndTutorial();
        resetCarPos();
        if (IsInvoking("resetCarPos"))
        {
            CancelInvoke("resetCarPos");
        }
    }
    public void gameplayFreeCash()
    {
        Adobj.RewardStatus = 2;
        Adobj.ShowRewardBasedVideo();
    }
    public void GiveCoins()
    {
        PlayerPrefs.SetInt("TotalPoints", (PlayerPrefs.GetInt("TotalPoints")) + 5000);
        UpdatePoints();
    }
    public void DoubleCash()
    {
        PlayerPrefs.SetInt("TotalPoints", (PlayerPrefs.GetInt("TotalPoints")) + 10000);
        points += 10000;
        UpdatePoints();
    }
    public void UpdatePoints()
    {
        TotalPoints.text = (PlayerPrefs.GetInt("TotalPoints")).ToString();
        currentPoints.text = (points).ToString();
    }
    public void GameplayDoubleCash()
    {
        Adobj.RewardStatus = 3;
        Adobj.ShowRewardBasedVideo();
        PlayerPrefs.SetInt("oncereward",0);
    }
    public void ExtraTimeCaller()
    {
        Adobj.RewardStatus = 4; 
        Adobj.ShowRewardBasedVideo(); 
    }
    public void ExtraHitsCaller()
    {
        Adobj.RewardStatus = 5;
        MissionFailPanel.SetActive(true);
        Adobj.ShowRewardBasedVideo();  
    }
    public GameObject Adnotav,extraCrashPanel,ExtraTimePanel;
    public void Adnotavfunc()
    {
        Debug.Log("klj");
        Adnotav.SetActive(true);
       
            MissionFailPanel.SetActive(true);
        
    }
    public void BuyRemoveAds()
    {
        inappobj.Buyremoveads();
    }

    
    public void Giftcar1()                                   //Gift
    {
        PlayerPrefs.SetInt("Giftcar1", 1);
        
    }
    public void Giftcar2()                                 //Gift
    {
        PlayerPrefs.SetInt("Giftcar7", 1);
        
    }
    public void Giftcar3()                                  //Gift
    {
        PlayerPrefs.SetInt("Giftcar14", 1);
    }
    public void Buycar1()
    {
        PlayerPrefs.SetInt("Getcar3", 1);
    }
    public void Buycar2()
    {
        PlayerPrefs.SetInt("Getcar4", 1);
    }
    public void Buycar3()
    {
        PlayerPrefs.SetInt("Getcar5", 1);
    }
    public void Buycar4()
    {
        PlayerPrefs.SetInt("Getcar6", 1);
    }
    public void Buycar5()
    {
        PlayerPrefs.SetInt("Getcar7", 1);
    }

    public void Buycar6()
    {
        PlayerPrefs.SetInt("Getcar8", 1);
    }
    public void Buycar7()
    {
        PlayerPrefs.SetInt("Getcar9", 1);
    }
    public void Buycar8()
    {
        PlayerPrefs.SetInt("Getcar10", 1);
    }
 

    public GameObject[] Removeads, suv1, suv2;         /*, suv3, suv4, suv5, suv6, suv7, suv8, suv9*/
    public void CheckPurchaseBTNS()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            for (int i = 0; i < Removeads.Length;i++ )
                Removeads[i].SetActive(false);
        }
      
    }

    public void showbanners()
    {
        rectbanerobj.ShowBanner();
        //Adobj.ShowBannerFunc();
        gamebanner.ShowBanner();
    }

    public void hidebanners()
    {
        rectbanerobj.HideBanner();
       // Adobj.HideBannerFunc();
        gamebanner.HideBanner();
    }
    public GameObject RateusPanel;
	public void Rateus()
	{
        //RateusPanel.SetActive (true);
        //Show_reveiwScript.showReviewCard();

    }

    //public void Rate()
    //{
    //	Application.OpenURL("https://play.google.com/store/apps/details?id=com.fcfg.elevation.parking");
    //	PlayerPrefs.SetInt ("rated",1);
    //	RateusPanel.SetActive (false);
    //}
    
    public void Parking()
    {      
        FindObjectOfType<MoveTowards>().SpeedSet();
    }
  
    public void LoadSpoiler()
    {
        //if(PlayerPrefs.GetInt(PlayerPrefs.GetInt("SelectedVehicle") + "Spoiler") == 1)
        if (PlayerPrefs.GetInt(PlayerPrefs.GetInt("SelectedVehicle") + "Spoiler") != -1)
        {
            car[PlayerPrefs.GetInt("SelectedVehicle")].Spoiler[PlayerPrefs.GetInt(PlayerPrefs.GetInt("SelectedVehicle") + "Spoiler")].SetActive(true);
        }
    }
     public void GetCarbtn()
    {
        if(PlayerPrefs.GetInt("Parking") ==1)
        {
            if (currentLevel == 22)
            {
                GetCar[0].SetActive(true);           //Gift
                
            }         
            if (currentLevel == 30)
            {
                
                GetCar[3].SetActive(true);
               
            }
            if (currentLevel == 35)
            {
                GetCar[4].SetActive(true);
               
            }
            if (currentLevel == 40)
            {
                GetCar[5].SetActive(true);
               
            }
            if (currentLevel == 45)
            {
                GetCar[6].SetActive(true);
                
            }
            if (currentLevel == 50)
            {
                GetCar[7].SetActive(true);   //Gift
               
            }
        }

        if (PlayerPrefs.GetInt("Elevated1") == 1)
        {
            if (currentLevel == 3)
            {
                GetCar[2].SetActive(true);            //Gift
                
            }

            if(currentLevel == 8)
            {
                GetCar[7].SetActive(true);
               
            }
            if(currentLevel == 14)
            {
                GetCar[8].SetActive(true);
                
            }
        }

        if (PlayerPrefs.GetInt("Elevated2") == 1)
        {
            if(currentLevel == 6)
            {
                GetCar[9].SetActive(true);
              
            }
            if(currentLevel == 12)
            {
                GetCar[10].SetActive(true);
               
            }
        }
    }

    

    [System.Serializable]
    public class Carss
     
    {
        public string name;
        public GameObject[] Spoiler;
       
    }
    public GameObject[] GetCar;


}


