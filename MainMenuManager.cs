using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Utility;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;
public class MainMenuManager : MonoBehaviour
{
    public AudioSource GameMusic, effectMusic;
    public Image GamemusicImg, EffectMusicImg;
    public GameObject Upperpanel, LowerPanel;
    public Sprite ONsprit, OFFsprit;
    public GameObject playfade, garagefade, settingfade, storefade;
    public int currentVehicle = 0;
    public GameObject[] PlayerVehicles;
    public GameObject Player;
    public Transform SpawnObject;
    public RotateCamera CameraScript;
    [HideInInspector]
    public Image ButtonsControlIMG, SteeringControlsIMG, TiltControlsIMG, soundIMG;
    [HideInInspector]
    public Sprite ButtonsSelected, ButtonsUnselected, SteeringSelected, SteeringUnselected, TiltSelected, TiltUnselected, SoundOn, SoundOff, currentLevel;
    public bool CheckSound;
    public Text TotalCoins, vehiclePrice, VehiclePurchasingInfoText;
    public int[] CarUnlockingPoints;
    public GameObject PlayBTN, paintBtn, backbtnGarage;
    //[HideInInspector]
    public GameObject insufficeintCoinsPanel, BuyBTN, LoadingPanel, ExitPanel, upperPanel, MainMenuPanel, LevelSelectionPanel, Storepanel;
    public Button[] EasyLevelBTNs, MediumLevelBTNs, HardLevelBTNs, ParkingBtns;
    public GameObject[] EasyLevelLocks, MediumLevelLocks, HardLevelLocks, ParkingLocks;
    public int[] easyLevelMode, HardLevelMode, ProLevelMode;
    public Sprite[] levelComplete;
    public SmoothFollow cameraScript;
    public Transform DefaultCameraPosition1, DefaultCameraPosition2, Camera;
    public GameObject MainMenu, CarSelection;
    public Material[] CarBodyMaterial, CarWindowMAterial;
    public Color[] colors;
    public GameObject[] bodyPaintHL, windowaPaintHL;
    public GameObject[] hlobject;
    public Sprite greenBack, blueBAck;
    public Image[] qualityBTNs;
    public int Controls;
    public Image[] ControlImage;
    public Sprite[] controlsSprite;
    public RCC_Settings rccSettings;
    public GameObject LockImage;
    Material currentSkybox;
    public GameObject ExtremeLvlBTN, ProLevelBTN, ElevatedDriveLevelBTN, ExtremeLockBTN, ProLockBTN, ElevatedDriveLockBTN;

    public Image LoadingImage;
    private GoogleMobileAdsDemoScript Adobj;
    GameSceneBanner gamebanner;
    private INAPP inappobj;
    public GameObject GaragePanel;
    static bool splash;
    public GameObject SplashPanel;
    public Text ShowName;
    public GameObject Hand;
    public GameObject Hand2;
    public GameObject Hand3;
    public GameObject Hand4;
   
    public Scrollbar lernerscrolbar, Expertscrolbar, Parkingscrolbar, promodescrolbar;
    bool lernerboolleft, lernerboolright;   
    public Texture[] textures;
    public Material[] rend;
    public Material[] rendwindow;
    public Color[] windowColors;
    public ParticleSystem PaintParticles;
    public bool UsePaintParticles = true;
    public GameObject[] neon;
    public Material[] rendNeon;
    public Color[] NeonColors;
    public Material[] PlayerBody;
    public Texture[] Playertextures;
    public int kar = 0;
    public int mycash;
    public GameObject[] PurchaseBTN;
    [HideInInspector]
    public CarCustominzation custom;
    public GameObject Custombtns;
    [HideInInspector]
    public GameObject Colorpanel,Texturepanel,Rimspanel,Windowpanel,Neonpanel,camberpanel,spoilerpanel;
    [HideInInspector]
    public GameObject Colorbtn, Texturebtn, Rimsbtn, Windowbtn, Neonbtn, camberbtn, spoilerbtn;
    public GameObject Exitbtn;
    public void lernerswipright_click()
    {
        if (lernerscrolbar.value < 1f)
            lernerscrolbar.value += .26f;
        else
            lernerscrolbar.value = 1f;
    }
    public void lernerswipleft_click()
    {
        if (lernerscrolbar.value > 0f)
            lernerscrolbar.value -= .26f;
        else
            lernerscrolbar.value = 0f;
    }
    public void Expertswipright_click()
    {
        if (Expertscrolbar.value < 1f)
            Expertscrolbar.value += .26f;
        else
            Expertscrolbar.value = 1f;
    }
    public void Expertswipleft_click()
    {
        if (Expertscrolbar.value > 0f)
            Expertscrolbar.value -= .26f;
        else
            Expertscrolbar.value = 0f;
    }
    public void parkingswipright_click()
    {
        if (Parkingscrolbar.value < 1f)
            Parkingscrolbar.value += .2f;
        else
            Parkingscrolbar.value = 1f;
    }

    public void parkingswipleft_click()
    {
        if (Parkingscrolbar.value > 0f)
            Parkingscrolbar.value -= .2f;
        else
            Parkingscrolbar.value = 0f;
    }
    public void promodeswipright_click()
    {
        if (promodescrolbar.value < 1f)
            promodescrolbar.value += .26f;
        else
            promodescrolbar.value = 1f;
    }

    public void promodeswipleft_click()
    {
        if (promodescrolbar.value > 0f)
            promodescrolbar.value -= .26f;
        else
            promodescrolbar.value = 0f;
    }
    bool fadeonce;

    void Start()
    {
         PlayerPrefs.SetInt("set", 0);
        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {               
            PlayerPrefs.SetInt("Controls", 0);
            PlayerPrefs.SetInt("EffectValue", 1);
            PlayerPrefs.SetInt("ControlsSensitivity", 3);
            PlayerPrefs.SetInt("EnvNo", 3);      
            if (Application.platform == RuntimePlatform.Android)
            {
                PlayerPrefs.SetInt("GraphicsQuality", 0);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                PlayerPrefs.SetInt("GraphicsQuality", 5);
            }
           
            //PlayerPrefs.SetInt("TotalPoints", 5000);
            PlayerPrefs.SetInt("FirstTime", 1);            
        }
        
        currentVehicle = PlayerPrefs.GetInt("SelectedVehicle");
        kar = PlayerPrefs.GetInt("car");
        custom = FindObjectOfType<CarCustominzation>();
        Time.timeScale = 1f;
        Handheld.StopActivityIndicator();
        AudioListener.volume = 1;
       
        if (PlayerPrefs.GetInt("FirstTimeMusicVolume") == 0)
        {
            PlayerPrefs.SetFloat("MusicValue", 0.35f);
            PlayerPrefs.SetInt("FirstTimeMusicVolume", 1);
            
        }
        Showname();
        PlayerPrefs.SetInt("car0", 1);
        
        Analytics.CustomEvent("MenuStart");
       // mycash = PlayerPrefs.GetInt("TotalPoints");
  
        if (!fadeonce)
        {
            StartCoroutine(fadeobjs_func());
            fadeonce = true;
        }
        SetValues();
        UpdatePoints();
        CheckLockLevels();
        CheckPurchaseBTNS();
        //Player = Instantiate(PlayerVehicles[currentVehicle], SpawnObject.position, SpawnObject.rotation);
        if (PlayerPrefs.GetInt("customiz") == 0)
        {
            PlayerVehicles[currentVehicle].SetActive(true);
        }
         Player = PlayerVehicles[currentVehicle];
        
        currentSkybox = RenderSettings.skybox;
        Adobj = (GoogleMobileAdsDemoScript)FindObjectOfType(typeof(GoogleMobileAdsDemoScript));
        gamebanner = FindObjectOfType<GameSceneBanner>();
        if (Adobj)
        {
            Adobj.MMLink();
        }
        inappobj = (INAPP)FindObjectOfType(typeof(INAPP));
        if (inappobj)
        {
            inappobj.MainMenuLinker();
        }

        if (gamebanner)
        {
            gamebanner.MMLink();
        }
        if (!splash)
        {
            SplashPanel.SetActive(true);
            Invoke("SplashDeactivate", 3.8f);
            splash = true;
        }
        else
        {
            SplashPanel.SetActive(false);
            if (PlayerPrefs.GetInt("FirstTime") == 1)
            {
                Upperpanel.SetActive(true);
                LowerPanel.SetActive(true);
            }
            MainMenu.SetActive(true);
        }
        setCurrentColors();

        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            Hand.SetActive(true);
            Hand2.SetActive(true);
            Hand3.SetActive(true);
            Hand4.SetActive(true);
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        if (PlayerPrefs.GetFloat("MusicValue") == 0.35f)
        {
            GamemusicImg.sprite = ONsprit;
            GameMusic.volume = PlayerPrefs.GetFloat("MusicValue");
        }
        else
        {
            GamemusicImg.sprite = OFFsprit;
        }
        if (PlayerPrefs.GetInt("EffectValue") == 1)
        {
            EffectMusicImg.sprite = ONsprit;
        }
        else
        {
            EffectMusicImg.sprite = OFFsprit;
        }
        showsimplebaner();
        InvokeRepeating("HideGameBanner", 1f, 0.3f);

        for (int m = 0; m < neon.Length; m++)
        {
            if (PlayerPrefs.GetInt("Neon" + kar) == m)
            {
                neon[kar].SetActive(true);
            }
        }
        UpdateColor();
        UpdateNeon();
        UpdateRim();
        UpdateTexture();
        UpdateWindow();
        //CheckVehicle();
        //checkVehicle2();
        if (currentVehicle == 4 || currentVehicle == 8 || currentVehicle == 10 || currentVehicle == 11 || currentVehicle == 12 || currentVehicle == 16 || currentVehicle == 18 || currentVehicle == 21)
        {
            Spoilerbtn.SetActive(false);
            SpoilerPanel.SetActive(false);
        }
        else
        {
            Spoilerbtn.SetActive(true);
        }

        if(PlayerPrefs.GetInt("customiz") == 1)
        {      
            MainMenu.SetActive(false);
            activateCameraScript();
            CarSelection.SetActive(true);
            Exitbtn.SetActive(false);
            GetVehicle();
            PlayerPrefs.SetInt("customiz", 0);
        }
    }

    public void ParkingFirst()
    {
        PlayerPrefs.SetInt("Parking", 1);
    }
    public void ElevatedPArk()
    {
        PlayerPrefs.SetInt("Elevated1", 1);
    }
    public void ElevatedPArkHard()
    {
        PlayerPrefs.SetInt("Elevated2", 1);
    }

    public void Drivebtn()
    {
        Analytics.CustomEvent("ModeSelection");
    }

    public void garagebtn()
    {
        Analytics.CustomEvent("CarSelection");
    }

    public void settingsbtn()
    {
        Analytics.CustomEvent("Settings");
    }

    public void storesbtn()
    {
        Analytics.CustomEvent("Store");
    }
    void HideGameBanner()
    {
        if (gamebanner)
        {
            gamebanner.HideBanner();
        }
    }

    public Text nameText;
    public GameObject EnterNameObject, confirmBtn;
    public void SplashDeactivate()
    {
        SplashPanel.SetActive(false);
        if (PlayerPrefs.GetInt("FirstTime") == 1)
        {
            Upperpanel.SetActive(true);
            LowerPanel.SetActive(true);
        }
        MainMenu.SetActive(true);
    }
    public void showsimplebaner()
    {
        if (Adobj != null)
            Adobj.ShowBannerFunc();
    }
    IEnumerator fadeobjs_func()
    {
        yield return new WaitForSeconds(4f);
        playfade.SetActive(true);
        garagefade.SetActive(false);
        settingfade.SetActive(false);
        storefade.SetActive(false);
        yield return new WaitForSeconds(4f);
        playfade.SetActive(false);
        garagefade.SetActive(true);
        settingfade.SetActive(false);
        storefade.SetActive(false);
        yield return new WaitForSeconds(4f);
        playfade.SetActive(false);
        garagefade.SetActive(false);
        settingfade.SetActive(true);
        storefade.SetActive(false);
        yield return new WaitForSeconds(4f);
        playfade.SetActive(false);
        garagefade.SetActive(false);
        settingfade.SetActive(false);
        storefade.SetActive(true);
        StartCoroutine(fadeobjs_func());
    }
    public void Update()
    {
        if (currentSkybox)
        {
            currentSkybox.SetFloat("_Rotation", Time.time * 1.2f);

        }
        if (nameText.text == "")
        {
            EnterNameObject.SetActive(true);
        }
        else
        {
            EnterNameObject.SetActive(false);
            confirmBtn.SetActive(true);
        }
        if (lernerboolright)
        {
            lernerscrolbar.value += lernerscrolbar.value * (Time.timeScale * 0.05f);
        }

        if (lernerboolleft)
        {
            lernerscrolbar.value -= lernerscrolbar.value * (Time.timeScale * 0.05f);
        }

    }
    public GameObject Spoilerbtn, SpoilerPanel;
    public void NextCar()
    {
        if (currentVehicle < PlayerVehicles.Length - 1)
        {
           
            PlayerVehicles[currentVehicle].SetActive(false);            
            if (currentVehicle > PlayerVehicles.Length - 1)
            currentVehicle = PlayerVehicles.Length;
            currentVehicle++;
            PlayerVehicles[currentVehicle].SetActive(true);
            Debug.Log("VehicleNOW==" + currentVehicle);
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);
            Debug.Log("CarSelect" + currentVehicle);
            CheckVehicle();
            setCurrentColors();
            if (currentVehicle == 4 || currentVehicle == 8 || currentVehicle == 10 || currentVehicle == 11 || currentVehicle == 12 || currentVehicle == 16 || currentVehicle == 18 || currentVehicle == 21)
            {
                Debug.Log("Spoilerbtnoff");
                Spoilerbtn.SetActive(false);
                SpoilerPanel.SetActive(false);
            }
            else
            {
                Spoilerbtn.SetActive(true);
            }
            if (newAllValue > 2)
            {

              if (PlayerPrefs.GetInt("set") == 0)
              {
                  // PlayerPrefs.SetInt("Color" + currentVehicle, newAllValue);
                   Debug.Log("colorNextYYYY" + newAllValue);
                   CarBodyMaterial[currentVehicle].mainTexture = null;
                   CarBodyMaterial[currentVehicle].color = colors[PlayerPrefs.GetInt("Color" + currentVehicle)];
              }

              else if (PlayerPrefs.GetInt("set") == 1)                
              {
                    buyColorBtn.SetActive(false);                  
                    CarBodyMaterial[currentVehicle].mainTexture = null;
                    CarBodyMaterial[currentVehicle].color = colors[PlayerPrefs.GetInt("Color" + currentVehicle)];
                }
            }
            else if (newAllValue < 2)
            {
                if (PlayerPrefs.GetInt("set") == 0)
                {
                    PlayerPrefs.SetInt("BuyColor" + currentBodyColor, newAllValue);
                    Debug.Log("defaultcolorNextdefault" + PlayerPrefs.GetInt("BuyColor" + currentBodyColor));
                    CarBodyMaterial[currentVehicle].mainTexture = null;
                    CarBodyMaterial[currentVehicle].color = colors[PlayerPrefs.GetInt("Color" + currentVehicle)];
                }
            }
        }     
    }

    public void PreviousCar()
    {
        if (currentVehicle > 0)
        {
          
            PlayerVehicles[currentVehicle].SetActive(false);
           
            if (currentVehicle < 0)
                currentVehicle = 0;
            currentVehicle--;
            //Player = Instantiate(PlayerVehicles[currentVehicle], SpawnObject.position, SpawnObject.rotation);
            PlayerVehicles[currentVehicle].SetActive(true);
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);
            Debug.Log("CarSelect" + currentVehicle);
            CheckVehicle();
            setCurrentColors();     
         
            if (currentVehicle == 4 || currentVehicle == 8 || currentVehicle == 10 || currentVehicle == 11 || currentVehicle == 12 || currentVehicle == 16 || currentVehicle == 18 || currentVehicle == 21)
            {
                Spoilerbtn.SetActive(false);
                SpoilerPanel.SetActive(false);
            }
            else
            {
                Spoilerbtn.SetActive(true);
            }
        
            if (newAllValue > 2)
            {
                if (PlayerPrefs.GetInt("set") == 0)
                {
                   // PlayerPrefs.SetInt("Color" + currentVehicle, newAllValue);
                    Debug.Log("colorprev" + PlayerPrefs.GetInt("Color" + currentVehicle));
                    CarBodyMaterial[currentVehicle].mainTexture = null;
                    CarBodyMaterial[currentVehicle].color = colors[PlayerPrefs.GetInt("Color" + currentVehicle)];
                    buyColorBtn.SetActive(false);
                }
                if (PlayerPrefs.GetInt("set") == 1)
                {
                    buyColorBtn.SetActive(false);                    
                    CarBodyMaterial[currentVehicle].mainTexture = null;
                    CarBodyMaterial[currentVehicle].color = colors[PlayerPrefs.GetInt("Color" + currentVehicle)];
                }
            }
            else if (newAllValue < 2)
            {
                if (PlayerPrefs.GetInt("set") == 0)
                {
                    PlayerPrefs.SetInt("BuyColor" + currentBodyColor, newAllValue);
                    Debug.Log("defaultcolorNextdefault" + newAllValue);
                    CarBodyMaterial[currentVehicle].mainTexture = null;
                    CarBodyMaterial[currentVehicle].color = colors[PlayerPrefs.GetInt("Color" + currentVehicle)];
                }

            }
        }
    
    }
    public void InstantiateVehicle()
    {
        if (PlayerPrefs.GetInt("car" + currentVehicle) == 0)
        {
            //Destroy(Player);
            currentVehicle = PlayerPrefs.GetInt("SelectedVehicle");
            //Player = Instantiate(PlayerVehicles[currentVehicle], SpawnObject.position, SpawnObject.rotation);
            Player = PlayerVehicles[currentVehicle];
        }
    }
    public void SelectCar()
    {
        if (PlayerPrefs.GetInt("car" + currentVehicle) == 1)
        {
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);      //Selects and save player car reference for gameplay
            
        }
    }
    public void LoadScene(int LevelNo)
    {
        Adobj.HideBannerFunc();
        PlayerPrefs.SetInt("CurrentLevel", LevelNo);
        //int rand = Random.Range(0,3);
        //LoadingImage.sprite = LoadingIMG[rand];
        StartCoroutine("StartScene");
        Upperpanel.SetActive(false);
        LowerPanel.SetActive(false);
       
    }
    public void DifficultyLevel(int DifficultyLevel)
    {
        PlayerPrefs.SetInt("DifficultyLevel", DifficultyLevel);
    }
    public void Setvalues(int LevelNo)
    {
        PlayerPrefs.SetInt("CurrentLevel", LevelNo);
    }
    public void EnableCameraScript()
    {
        CameraScript.EnableTouch = true;
    }
    public void DisableCameraScript()
    {
        CameraScript.EnableTouch = false;
    }
  
   public void colorbtn()
    {
        Colorbtn.SetActive(true);
        Neonbtn.SetActive(false);
        Rimsbtn.SetActive(false);
        Spoilerbtn.SetActive(false);
        camberbtn.SetActive(false);
        Windowbtn.SetActive(false);
        Texturebtn.SetActive(false);
    }

    public void neonbtn()
    {
        Colorbtn.SetActive(false);
        Neonbtn.SetActive(true);
        Rimsbtn.SetActive(false);
        Spoilerbtn.SetActive(false);
        camberbtn.SetActive(false);
        Windowbtn.SetActive(false);
        Texturebtn.SetActive(false);
    }
    public void rimsbtn()
    {
        Colorbtn.SetActive(false);
        Neonbtn.SetActive(false);
        Rimsbtn.SetActive(true);
        Spoilerbtn.SetActive(false);
        camberbtn.SetActive(false);
        Windowbtn.SetActive(false);
        Texturebtn.SetActive(false);
    }
    public void texturebtn()
    {
        Colorbtn.SetActive(false);
        Neonbtn.SetActive(false);
        Rimsbtn.SetActive(false);
        Spoilerbtn.SetActive(false);
        camberbtn.SetActive(false);
        Windowbtn.SetActive(false);
        Texturebtn.SetActive(true);
    }
    public void spoilerbtns()
    {
        Colorbtn.SetActive(false);
        Neonbtn.SetActive(false);
        Rimsbtn.SetActive(false);
        Spoilerbtn.SetActive(true);
        camberbtn.SetActive(false);
        Windowbtn.SetActive(false);
        Texturebtn.SetActive(false);
    }

    public void camberbtns()
    {
        Colorbtn.SetActive(false);
        Neonbtn.SetActive(false);
        Rimsbtn.SetActive(false);
        Spoilerbtn.SetActive(false);
        camberbtn.SetActive(true);
        Windowbtn.SetActive(false);
        Texturebtn.SetActive(false);
    }
    
    public void windowbtns()
    {
        Colorbtn.SetActive(false);
        Neonbtn.SetActive(false);
        Rimsbtn.SetActive(false);
        Spoilerbtn.SetActive(false);
        camberbtn.SetActive(false);
        Windowbtn.SetActive(true);
        Texturebtn.SetActive(false);
    }
    public void checkVehicle2()
    {
        if (GaragePanel.activeInHierarchy)
        {
            PlayBTN.SetActive(true);
            paintBtn.SetActive(true);
            backbtnGarage.SetActive(true);
        }
        BuyBTN.SetActive(false);
        
        for (int i = 1; i < PurchaseBTN.Length; i++)
        {
            PurchaseBTN[i].SetActive(false);
        }
        LockImage.SetActive(false);
    }
    public void CheckVehicle()
    {
        if (currentVehicle != 0)
        {
            if (PlayerPrefs.GetInt("car" + currentVehicle) == 0)
            {
                PlayBTN.gameObject.SetActive(false);
                backbtnGarage.SetActive(false);
                BuyBTN.SetActive(true);                    
                Custombtns.SetActive(false);
                Colorpanel.SetActive(false);
                Texturepanel.SetActive(false);
                Rimspanel.SetActive(false);
                Windowpanel.SetActive(false);
                Neonpanel.SetActive(false);
                camberpanel.SetActive(false);
                SpoilerPanel.SetActive(false);
                for (int i = 1; i < PurchaseBTN.Length; i++)
                {
                 PurchaseBTN[i].SetActive(false);
                }
                PurchaseBTN[currentVehicle].SetActive(true);
                vehiclePrice.text = (CarUnlockingPoints[currentVehicle]).ToString();
                LockImage.SetActive(true);
            }
            else
            {
                PlayBTN.gameObject.SetActive(true);
                backbtnGarage.SetActive(true);
                BuyBTN.SetActive(false);
                Custombtns.SetActive(true);
                Colorbtn.SetActive(true);
                Neonbtn.SetActive(false);
                Rimsbtn.SetActive(false);
                Spoilerbtn.SetActive(false);
                camberbtn.SetActive(false);
                Windowbtn.SetActive(false);
                Texturebtn.SetActive(false);
                Colorpanel.SetActive(true);
                //Texturepanel.SetActive(true);
                //Rimspanel.SetActive(true);
                //Windowpanel.SetActive(true);
                //Neonpanel.SetActive(true);
                //camberpanel.SetActive(true);
                //SpoilerPanel.SetActive(true);
                for (int i = 1; i < PurchaseBTN.Length; i++)
                {
                    PurchaseBTN[i].SetActive(false);
                }
                LockImage.SetActive(false);
                PlayerPrefs.SetInt("car", kar);
            }
        }
        else
        {
            PlayBTN.gameObject.SetActive(true);
            backbtnGarage.SetActive(true);
            paintBtn.SetActive(true);
            BuyBTN.SetActive(false);
            Custombtns.SetActive(true);
            Colorpanel.SetActive(true);
            LockImage.SetActive(false);
            for (int i = 1; i < PurchaseBTN.Length; i++)
            {
                PurchaseBTN[i].SetActive(false);
            }
        }
    }
    public void carbuycash()
    {

        if (mycash >= CarUnlockingPoints[kar])
        {
            Debug.Log("CASHNOOOOOOOOO");
            PlayerPrefs.SetInt("car" + kar.ToString(), 1);
            mycash -= CarUnlockingPoints[kar];
            UpdatePoints();
            //SelectedPlayer();
        }
        else
        {
            ToStore();
        }
    }
    public void BuyVehicle()
    {
        if (mycash >= CarUnlockingPoints[currentVehicle])
        {        
            PlayerPrefs.SetInt("car" + currentVehicle, 1);
            PlayerPrefs.SetInt("TotalPoints", (PlayerPrefs.GetInt("TotalPoints")) - CarUnlockingPoints[currentVehicle]);                      
            BuyBTN.SetActive(false);
            PlayBTN.gameObject.SetActive(true);
            backbtnGarage.SetActive(true);
            //paintBtn.SetActive(true);           
            PurchaseBTN[currentVehicle].SetActive(false);
            CheckVehicle();
            Debug.Log("BUYHEERERERERER******");
            UpdatePoints();
            if (currentVehicle == 4 || currentVehicle == 8 || currentVehicle == 10 || currentVehicle == 11 || currentVehicle == 12 || currentVehicle == 16 || currentVehicle == 18 || currentVehicle == 21)
            {
                Spoilerbtn.SetActive(false);
                SpoilerPanel.SetActive(false);
            }
            else
            {
                Spoilerbtn.SetActive(true);
            }
        } 
        else
        {
           
            BuyBTN.SetActive(false);
            insufficeintCoinsPanel.SetActive(true);
            backbtnGarage.SetActive(true);
            PurchaseBTN[currentVehicle].SetActive(false);
          
            //UpdatePoints();
            
        }
    }
    public void UpdatePoints()
    {
        PlayerPrefs.SetInt("TotalPoints", mycash);
        PlayerPrefs.SetInt("TotalPoints", (PlayerPrefs.GetInt("TotalPoints")) - CarUnlockingPoints[currentVehicle]);
        TotalCoins.text = (PlayerPrefs.GetInt("TotalPoints")).ToString();
    }
    public void CheckLockLevels()
    {
       
        if (PlayerPrefs.GetInt("UnlockedSimpleLevels") >= 9)
        {
            ExtremeLvlBTN.SetActive(true);
            ExtremeLockBTN.SetActive(false);
           
        }
        if (PlayerPrefs.GetInt("UnlockedExtremeLevels") >= 8)
        {
            ProLevelBTN.SetActive(true);
            ProLockBTN.SetActive(false);
        }
        
        if (PlayerPrefs.GetInt("UnlockedParkingLevels") == 21)
        {
            ElevatedDriveLevelBTN.SetActive(true);
            ElevatedDriveLockBTN.SetActive(false);
           
        }

        if (PlayerPrefs.GetInt("UnlockedSimpleLevels") == 0)
        {
            EasyLevelBTNs[1].transform.GetChild(2).gameObject.SetActive(true);
        }
        for (int i = 2; i <= PlayerPrefs.GetInt("UnlockedSimpleLevels"); i++)
        {
            EasyLevelBTNs[i].interactable = true;
            if (i == PlayerPrefs.GetInt("UnlockedSimpleLevels"))
            {
                EasyLevelBTNs[i].transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                EasyLevelBTNs[i].transform.GetChild(2).gameObject.SetActive(false);
            }
            if (i != PlayerPrefs.GetInt("UnlockedSimpleLevels"))
            {
                EasyLevelBTNs[i].gameObject.GetComponent<Image>().sprite = levelComplete[3];

            }
            else
            {
                EasyLevelBTNs[i].transform.localScale = new Vector3(1.2f, 1.2f, 1);
            }
            EasyLevelLocks[i].SetActive(false);
        }


        if (PlayerPrefs.GetInt("UnlockedExtremeLevels") == 0)
        {
            MediumLevelBTNs[1].transform.GetChild(2).gameObject.SetActive(true);
        }
        for (int i = 2; i <= PlayerPrefs.GetInt("UnlockedExtremeLevels"); i++)
        {
            MediumLevelBTNs[i].interactable = true;
            if (i == PlayerPrefs.GetInt("UnlockedExtremeLevels"))
            {
                MediumLevelBTNs[i].transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                MediumLevelBTNs[i].transform.GetChild(2).gameObject.SetActive(false);
            }
            if (i != PlayerPrefs.GetInt("UnlockedExtremeLevels"))
            {
                MediumLevelBTNs[i].gameObject.GetComponent<Image>().sprite = levelComplete[3];
            }
            else
            {
                MediumLevelBTNs[i].transform.localScale = new Vector3(1.2f, 1.2f, 1);
            }
            MediumLevelLocks[i].SetActive(false);
        }

        for (int i = 2; i <= PlayerPrefs.GetInt("UnlockedProLevels"); i++)
        {
            HardLevelBTNs[i].interactable = true;
            if (i != PlayerPrefs.GetInt("UnlockedProLevels"))
            {
                HardLevelBTNs[i].gameObject.GetComponent<Image>().sprite = levelComplete[3];
            }
            else
            {
                HardLevelBTNs[i].transform.localScale = new Vector3(1.2f, 1.2f, 1);
            }
            HardLevelLocks[i].SetActive(false);
        }

        if (PlayerPrefs.GetInt("UnlockedParkingLevels") == 0)
        {
            ParkingBtns[19].transform.GetChild(1).gameObject.SetActive(true);
        }
        for (int i = 20; i <= PlayerPrefs.GetInt("UnlockedParkingLevels"); i++)
        {
            //Debug.Log ("PlayerPrefs.GetInt(UnlockedParkingLevels) : " + PlayerPrefs.GetInt("UnlockedParkingLevels"));
            ParkingBtns[i].interactable = true;
            if (i == PlayerPrefs.GetInt("UnlockedParkingLevels"))
            {
                ParkingBtns[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                ParkingBtns[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            if (i != PlayerPrefs.GetInt("UnlockedParkingLevels"))
            {
                ParkingBtns[i].gameObject.GetComponent<Image>().sprite = levelComplete[3];
            }
            else
            {
                ParkingBtns[i].transform.localScale = new Vector3(1.2f, 1.2f, 1);

            }
            ParkingLocks[i].SetActive(false);

        }

        if (PlayerPrefs.GetInt("UnlockedSimpleLevels") > 1)
        {
            EasyLevelBTNs[1].gameObject.GetComponent<Image>().sprite = levelComplete[3];

        }

        else if (PlayerPrefs.GetInt("UnlockedSimpleLevels") < 2)
        {
            EasyLevelBTNs[1].transform.localScale = new Vector3(1.2f, 1.2f, 1);

        }

        if (PlayerPrefs.GetInt("UnlockedExtremeLevels") > 1)
        {
            MediumLevelBTNs[1].gameObject.GetComponent<Image>().sprite = levelComplete[3];
        }
        else if (PlayerPrefs.GetInt("UnlockedExtremeLevels") < 2)
        {
            MediumLevelBTNs[1].transform.localScale = new Vector3(1.2f, 1.2f, 1);

        }

        if (PlayerPrefs.GetInt("UnlockedProLevels") > 1)
        {
            HardLevelBTNs[1].gameObject.GetComponent<Image>().sprite = levelComplete[3];

        }
        else if (PlayerPrefs.GetInt("UnlockedProLevels") < 2)
        {
            HardLevelBTNs[1].transform.localScale = new Vector3(1.2f, 1.2f, 1);

        }

        if (PlayerPrefs.GetInt("UnlockedParkingLevels") > 1)
        {
            ParkingBtns[19].gameObject.GetComponent<Image>().sprite = levelComplete[3];

        }

        else if (PlayerPrefs.GetInt("UnlockedParkingLevels") < 2)
        {
            ParkingBtns[19].transform.localScale = new Vector3(1.2f, 1.2f, 1);

        }

    }

    public IEnumerator StartScene()
    {
        //LoadingPanel.SetActive(true);
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene("GamePlay");
    }

 
    public void activateCameraScript()
    {
        cameraScript.enabled = true;
    }
    public void DeactivateCameraScript()
    {
        cameraScript.enabled = false;
        Camera.position = DefaultCameraPosition2.position;
        Camera.rotation = DefaultCameraPosition2.rotation;
    }
    public void EnableCarTouchScript()
    {
        //Player.GetComponent<OpenCarSelection>().enabled = false;
    }
    public void DisableCarTouchScript()
    {
        //Player.GetComponent<OpenCarSelection>().enabled = false;
    }

    public void DisableCarTouchScriptNew()
    {
        //Player.GetComponent<OpenCarSelection>().enabled = false;
    }

    [HideInInspector]
    public int currentBodyColor, CurrentWindowColor, currentBodyTexture, currentNeonColor, currentSpoiler,currentRim,currentCamber;
    [HideInInspector]
    public bool bodycolorchanged, windowcolorchanged, bodytexturechanged, neoncolorchanged,camberchanged,rimchanged,spoilerchanges;
    public Text ColorPurchaseText;
    int newAllValue;

    public void BuyColor()
    {

        if (mycash >= 1000)
        { 
            //PlayerPrefs.SetInt("BuyColor" + newAllValue, 1);          
            //PlayerPrefs.SetInt("Color" + currentVehicle, newAllValue);
            PlayerPrefs.SetInt("BuyColor" + currentBodyColor, newAllValue);
            //PlayerPrefs.SetInt("Texture" + kar, 0);
            buyColorBtn.SetActive(false);
            UpdateColor();
            mycash -= 1000;
            UpdatePoints();
            PlayerPrefs.SetInt("set", 1);         
            PlayerPrefs.SetInt("ColorSelected", 1);
        }
    }

    void UpdateColor()
    {
        if (!PlayerPrefs.HasKey("BuyColor" + 0))
        {
            PlayerPrefs.SetInt("BuyColor" + 0, 1);
        }
        for (int j = 0; j < colorLocks.Length; j++)
        {
            if (PlayerPrefs.GetInt("BuyColor" + j) > 0)
            {
                colorLocks[j].SetActive(false);
            }
            else
            {
                colorLocks[j].SetActive(true);
            }
        }
    }

    public void ChangeBodyColor(int colorIndex)
    {
        currentBodyColor = colorIndex;
        newAllValue = colorIndex;
        
        //PlayerPrefs.SetInt("car"+currentVehicle+"body",colorIndex);
        if (newAllValue == 0 || newAllValue == 1 || newAllValue == 2)
        {

            PlayerPrefs.SetInt("Color" + currentVehicle, newAllValue);
            Debug.Log("defaultcolor" + newAllValue);
            CarBodyMaterial[currentVehicle].mainTexture = null;
            CarBodyMaterial[currentVehicle].color = colors[colorIndex];
            buyColorBtn.SetActive(false);
            //PlayerPrefs.SetInt("set", 0);
            
        }
        else if (PlayerPrefs.GetInt("BuyColor" + colorIndex) > 0)
        {
            buyColorBtn.SetActive(false);
            Debug.Log("Selectedcolor" + newAllValue);
            CarBodyMaterial[currentVehicle].mainTexture = null;
            CarBodyMaterial[currentVehicle].color = colors[colorIndex];
            PlayerPrefs.SetInt("Color" + currentVehicle, colorIndex);
            PlayerPrefs.SetInt("set", 1);
            PlayerPrefs.SetInt("Texture" + kar, 0);                   
        }
        else 
        {
           
            Debug.Log("unselected" + PlayerPrefs.GetInt("Color" + currentVehicle));
            CarBodyMaterial[currentVehicle].mainTexture = null;
            CarBodyMaterial[currentVehicle].color = colors[colorIndex];
            Debug.Log("BUYBTNXXXXXXXXXXXXXXXX");
            buyColorBtn.SetActive(true);
        }      
     
        
        if (UsePaintParticles && PaintParticles != null)
        {

            PaintParticles.startColor = colors[colorIndex];
            PaintParticles.Play();
        }
      
        updateColorPurchaseText();
    }
    public void CheckColorBack()
    {

        for (int k = 0; k < CarBodyMaterial.Length; k++)
        {
            if (PlayerPrefs.GetInt("Color" + currentVehicle) == k)
            {
                if (PlayerPrefs.GetInt("set") == 0)
                {
                    CarBodyMaterial[currentVehicle].color = colors[PlayerPrefs.GetInt("Color" + currentVehicle)];
                }
                if (PlayerPrefs.GetInt("set") == 1)
                {
                    CarBodyMaterial[currentVehicle].color = colors[PlayerPrefs.GetInt("BuyColor" + currentBodyColor)];
                }
            }
        }
    }


 
    public void BuyTexture()
    {
        if (mycash >= 3000)
        {
            PlayerPrefs.SetInt("BuyTexture" + newAllValue, 1);
            PlayerPrefs.SetInt("Texture" + currentVehicle, newAllValue);
            PlayerPrefs.SetInt("set", 2);
            buyTextureBtn.SetActive(false);
            UpdateTexture();
            mycash -= 3000;
            UpdatePoints();
        }
    }
    void UpdateTexture()
    {
        if (!PlayerPrefs.HasKey("BuyTexture" + 0))
        {
            PlayerPrefs.SetInt("BuyTexture" + 0, 1);
        }
        for (int j = 0; j < textureLocks.Length; j++)
        {
            if (PlayerPrefs.GetInt("BuyTexture" + j) == 1)
            {
                textureLocks[j].SetActive(false);
            }
            else
            {
                textureLocks[j].SetActive(true);
            }
        }
    }
    public void ChangeBodyTexture(int TextureIndex)
    {
        currentBodyTexture = TextureIndex;
        newAllValue = TextureIndex;     
        //PlayerPrefs.SetInt("car"+currentVehicle+"body",colorIndex);
        if (PlayerPrefs.GetInt("BuyTexture" + TextureIndex) == 1)
        {
            PlayerPrefs.SetInt("Texture" + currentVehicle, TextureIndex);   //Texture index currentvehicle change here
            PlayerPrefs.SetInt("set", 2);
            buyTextureBtn.SetActive(false);
        }
        else
        {
            buyTextureBtn.SetActive(true);
        }
        if (TextureIndex == 0)
        {
            CarBodyMaterial[currentVehicle].color = colors[PlayerPrefs.GetInt("color" + currentVehicle)];           
            PlayerPrefs.SetInt("Texture" + currentVehicle, 0);
            //painterdecal.LoadLivery(PlayerPrefs.GetInt(PlayerPrefs.GetInt("car") + "Livery") - 99);
            CarBodyMaterial[currentVehicle].mainTexture = null;
        }
        else
        {
            CarBodyMaterial[currentVehicle].mainTexture = Playertextures[TextureIndex];
        }
      
        updateColorPurchaseText();
    }

    public void CheckTextureBack()
    {
        for (int k = 0; k < CarBodyMaterial.Length; k++)
        {
            if (PlayerPrefs.GetInt("Texture" + currentVehicle) == k)
            {
                CarBodyMaterial[kar].mainTexture = Playertextures[PlayerPrefs.GetInt("Texture" + currentVehicle)];
            }
        }
    }

    public void BuyNeon()
    {

        if (mycash >= 1500)
        {
            PlayerPrefs.SetInt("BuyNeon" + newAllValue, 1);
            PlayerPrefs.SetInt("Neon" + currentVehicle.ToString(), newAllValue);
            buyNeonBtn.SetActive(false);
            UpdateNeon();
            mycash -= 1500;
            UpdatePoints();
        }
    }
    void UpdateNeon()
    {
        if (!PlayerPrefs.HasKey("BuyNeon" + 0))
        {
            PlayerPrefs.SetInt("BuyNeon" + 0, 1);
        }
        for (int j = 0; j < neonLocks.Length; j++)
        {
            if (PlayerPrefs.GetInt("BuyNeon" + j) == 1)
            {
                neonLocks[j].SetActive(false);
            }
            else
            {
                neonLocks[j].SetActive(true);
            }
        }
    }
    public void ChangeNeon (int NeonIndex)
    {
        currentNeonColor = NeonIndex;
        newAllValue = NeonIndex;
        
        if (PlayerPrefs.GetInt("BuyNeon" + NeonIndex) == 1)
        {
            PlayerPrefs.SetInt("Neon" + currentVehicle.ToString(), NeonIndex);
            buyNeonBtn.SetActive(false);
        }
        else
        {
            buyNeonBtn.SetActive(true);
        }
        if (NeonIndex == 0)
        {
            neon[currentVehicle].SetActive(false);
            rendNeon[currentVehicle].color = NeonColors[NeonIndex];
            buyNeonBtn.SetActive(false);
        }
        else
        {
            neon[currentVehicle].SetActive(true);
            rendNeon[currentVehicle].color = NeonColors[NeonIndex];
        }

     
    }
    public void CheckneonBack()
    {
        for (int k = 0; k < rendNeon.Length; k++)
        {
            if (PlayerPrefs.GetInt("Neon" + currentVehicle) == k)
            {               
                if (neon != null)
                 neon[currentVehicle].SetActive(true);
                rendNeon[currentVehicle].color = NeonColors[PlayerPrefs.GetInt("Neon" + currentVehicle)];             
            }
        }
    }
    public void BuyWindow()
    {

        if (mycash >= 1000)
        {
            PlayerPrefs.SetInt("BuyWindow" + newAllValue, 1);
            PlayerPrefs.SetInt("Window" + kar, newAllValue);
            buyWindowBtn.SetActive(false);
            UpdateWindow();
            mycash -= 1000;
            UpdatePoints();
        }
    }
    void UpdateWindow()
    {
        if (!PlayerPrefs.HasKey("BuyWindow" + 0))
        {
            PlayerPrefs.SetInt("BuyWindow" + 0, 1);
        }
        for (int j = 0; j < windowLocks.Length; j++)
        {
            if (PlayerPrefs.GetInt("BuyWindow" + j) == 1)
            {
                windowLocks[j].SetActive(false);
            }
            else
            {
                windowLocks[j].SetActive(true);
            }
        }
    }
    public void ChangeWindowTintColor(int colorIndex)
    {
        CurrentWindowColor = colorIndex;
        newAllValue = colorIndex;       
        // PlayerPrefs.SetInt("car" + currentVehicle + "window", colorIndex);
        if (PlayerPrefs.GetInt("BuyWindow" + colorIndex) == 1)
        {
            PlayerPrefs.SetInt("Window" + kar, colorIndex);
            buyWindowBtn.SetActive(false);
        }
        else
        {
            buyWindowBtn.SetActive(true);
        }
        CarWindowMAterial[currentVehicle].color = windowColors[colorIndex];
       
    }
    public void CheckWindowBack()
    {
        for (int k = 0; k < CarWindowMAterial.Length; k++)
        {
            if (PlayerPrefs.GetInt("Window" + kar) == k)
            {              
                CarWindowMAterial[currentVehicle].color = windowColors[PlayerPrefs.GetInt("Window" + kar)];
                
            }
        }
    }
    public void BuyRim()
    {

        if (mycash >= 2000)
        {
            PlayerPrefs.SetInt("BuyRim" + newAllValue, 1);
            PlayerPrefs.SetInt("Rim" + kar, newAllValue);
            PlayerPrefs.SetInt("Rim", newAllValue);
            buyRimBtn.SetActive(false);
            UpdateRim();
            mycash -= 2000;
            UpdatePoints();
        }
    }
    void UpdateRim()
    {
        if (!PlayerPrefs.HasKey("BuyRim" + 0))
        {
            PlayerPrefs.SetInt("BuyRim" + 0, 1);
        }
        for (int j = 0; j < rimLocks.Length; j++)
        {
            if (PlayerPrefs.GetInt("BuyRim" + j) == 1)
            {
                rimLocks[j].SetActive(false);
            }
            else
            {
                rimLocks[j].SetActive(true);
            }
        }
    }
    public void ChangeRims(int value)
    {
        currentRim = value;
        newAllValue = value;
                
        if (PlayerPrefs.GetInt("BuyRim" + value) == 1)
        {
            PlayerPrefs.SetInt("Rim", value);
            PlayerPrefs.SetInt("Rim" + kar, value);
            buyRimBtn.SetActive(false);
        }
        else
        {
            buyRimBtn.SetActive(true);
        }
        rend[currentVehicle].mainTexture = textures[value];
    }


    public void CheckRimBack()
    {
        for (int k = 0; k < rend.Length; k++)
        {
            if (PlayerPrefs.GetInt("Rim") == k)
            {
                rend[kar].mainTexture = textures[PlayerPrefs.GetInt("Rim")];
            }
        }
    }

   

    void ToStore()
    {
        Storepanel.SetActive(true);
    }

    public void setCurrentColors()
    {                
        custom.LoadSpoiler();
        custom.Load();
        CheckTextureBack();
        CheckColorBack();
        CheckneonBack();
        CheckWindowBack();
        CheckRimBack();
    }




    public void updateColorPurchaseText()
    {
        //if (bodycolorchanged && windowcolorchanged)
        //{
        //    ColorPurchaseText.text = "15000";
        //}
        //else if (bodycolorchanged)
        //{
        //    ColorPurchaseText.text = "10000";
        //}
        //else if (windowcolorchanged)
        //{
        //    ColorPurchaseText.text = "5000";
        //}


    }
    public void ApplyColorsBtn()
    {
        int totalPurchase = 0;
        if (bodycolorchanged && windowcolorchanged)
        {
            totalPurchase = 15000;
        }
        else if (bodycolorchanged)
        {
            totalPurchase = 10000;
        }
        else if (windowcolorchanged)
        {
            totalPurchase = 5000;
        }

        //if (PlayerPrefs.GetInt("TotalPoints") >= totalPurchase)
        //{
            PlayerPrefs.SetInt("car" + currentVehicle + "window", CurrentWindowColor);
            PlayerPrefs.SetInt("car" + currentVehicle + "body", currentBodyColor);
            PlayerPrefs.SetInt("car" + currentVehicle + "body", currentBodyTexture);
            PlayerPrefs.SetInt("car" + currentVehicle + "neon", currentNeonColor);          
            PlayerPrefs.SetInt("car" + currentVehicle + "rim", currentRim);          
            PlayerPrefs.SetInt("TotalPoints", (PlayerPrefs.GetInt("TotalPoints")) - totalPurchase);

            applyBtn.SetActive(false);
            bodycolorchanged = false;
            windowcolorchanged = false;
            bodytexturechanged = false;
            neoncolorchanged = false;
            rimchanged = false;
            UpdatePoints();
            //insufficeintCoinsPanel.SetActive(true);
            // VehiclePurchasingInfoText.text = "Car Purchased..!!";
        //}
        //else
       // {
        //    BuyBTN.SetActive(false);
        //    insufficeintCoinsPanel.SetActive(true);
        //    //  VehiclePurchasingInfoText.text = "Insufficient Points..!!";
        //    UpdatePoints();
        //}
    

    }

    public void CheckColorPurchased()
    {
        
        CheckTextureBack();
        CheckColorBack();
        CheckneonBack();
        CheckWindowBack();
        CheckRimBack();
        custom.Load();
        custom.LoadSpoiler();

    }
    public void PAintBTN(GameObject other)
    {
        //EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable=false;
        //other.GetComponent<Button>().interactable = true;

    }
    public void PAintDBTN(GameObject other)
    {

        //other.GetComponent<Button>().interactable = false;

    }
    public void PAintoppositeBTN(GameObject other)
    {

        //other.GetComponent<Button>().interactable = true;

    }
    public void updateBodyhighlightBTN(int no)
    {
        for (int i = 0; i < bodyPaintHL.Length; i++)
        {
            bodyPaintHL[i].SetActive(false);
        }
        bodyPaintHL[no].SetActive(true);
    }

    public void updatewindowHighlightBTN(int no)
    {
        for (int i = 0; i < windowaPaintHL.Length; i++)
        {
            windowaPaintHL[i].SetActive(false);
        }
        windowaPaintHL[no].SetActive(true);
    }

    public void setWeather(int weatherno)
    {
        for (int i = 0; i < hlobject.Length; i++)
        {
            hlobject[i].SetActive(false);
        }
        hlobject[weatherno].SetActive(true);
        PlayerPrefs.SetInt("EnvNo", weatherno);
        
    }

    public void GraphicSettingsBTN(int qualityNo)
    {
        QualitySettings.SetQualityLevel(qualityNo);
        PlayerPrefs.SetInt("GraphicsQuality", qualityNo);
        for (int i = 0; i < qualityBTNs.Length; i++)
        {
            qualityBTNs[i].sprite = blueBAck;
        }
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = greenBack;
    }

    public void ChangeControlsBTN(int controlval)
    {
        Controls = controlval;
        if (Controls == 0)
        {
            for (int i = 0; i < ControlImage.Length; i++)
            {
                ControlImage[i].sprite = controlsSprite[1];
            }
            ControlImage[Controls].sprite = greenBack;
            rccSettings.useAccelerometerForSteering = false;
            rccSettings.useSteeringWheelForSteering = false;
            PlayerPrefs.SetInt("Controls", 0);

        }
        else if (Controls == 1)
        {
            for (int i = 0; i < ControlImage.Length; i++)
            {
                ControlImage[i].sprite = controlsSprite[1];
            }
            ControlImage[Controls].sprite = greenBack;
            rccSettings.useAccelerometerForSteering = false;
            rccSettings.useSteeringWheelForSteering = true;
            PlayerPrefs.SetInt("Controls", 1);
        }
        else if (Controls == 2)
        {
            for (int i = 0; i < ControlImage.Length; i++)
            {
                ControlImage[i].sprite = controlsSprite[1];
            }
            ControlImage[Controls].sprite = greenBack;
            rccSettings.useAccelerometerForSteering = true;
            rccSettings.useSteeringWheelForSteering = false;
            PlayerPrefs.SetInt("Controls", 2);
        }
    }
    public void controlsChange()
    {
        if (Controls < 2)
        {
            Controls++;
        }
        else
        {
            Controls = 0;
        }
    }
    public void ChangeSensitivity()
    {
        rccSettings.UIButtonSensitivity = SensitivitySlider.value * 10;
        PlayerPrefs.SetInt("ControlsSensitivity", (int)rccSettings.UIButtonSensitivity);
    }
    bool MusicON, effectON;
    public void SetmusicVolume()
    {
        MusicON = !MusicON;
        if (MusicON)
        {
            PlayerPrefs.SetFloat("MusicValue", 0.35f);
            GameMusic.volume = 0.35f;
            GamemusicImg.sprite = ONsprit;
        }
        else
        {
            PlayerPrefs.SetFloat("MusicValue", 0);
            GameMusic.volume = 0;
            GamemusicImg.sprite = OFFsprit;
        }
    }
    public void SeteffectVolume()
    {
        //        PlayerPrefs.SetFloat("EffectValue", EffectsSlider.value);
        effectON = !effectON;
        if (effectON)
        {
            PlayerPrefs.SetInt("EffectValue", 1);
            effectMusic.volume = 1;
            EffectMusicImg.sprite = ONsprit;
            //			Debug.Log ("EffectValue" + PlayerPrefs.GetInt ("EffectValue"));
        }
        else
        {
            PlayerPrefs.SetInt("EffectValue", 0);
            effectMusic.volume = 0;
            EffectMusicImg.sprite = OFFsprit;
            //			Debug.Log ("EffectValue-else" + PlayerPrefs.GetInt ("EffectValue"));
        }
    }
    public void PaintBtn()
    {
        updateBodyhighlightBTN(PlayerPrefs.GetInt("car" + currentVehicle + "body"));
        updatewindowHighlightBTN(PlayerPrefs.GetInt("car" + currentVehicle + "window"));
        
    }
    public void weatherSetting()
    {
        setWeather(PlayerPrefs.GetInt("EnvNo"));
        
    }

    public Slider SensitivitySlider, MusicSlider, EffectsSlider;
    public int[] Bodycolorno, WindowTintNo,Neon,Spoiler,Camber,Rim;


    public void SetValues()
    {

        UpdatePoints();
        Controls = PlayerPrefs.GetInt("Controls");
        ChangeControlsBTN(Controls);

        if (PlayerPrefs.GetInt("GraphicsQuality") == 3)
        {
            qualityBTNs[1].sprite = greenBack;
            QualitySettings.SetQualityLevel(3);
        }
        else if (PlayerPrefs.GetInt("GraphicsQuality") == 5)
        {
            qualityBTNs[2].sprite = greenBack;
            QualitySettings.SetQualityLevel(5);
        }
        else if (PlayerPrefs.GetInt("GraphicsQuality") == 0)
        {
            qualityBTNs[0].sprite = greenBack;
            QualitySettings.SetQualityLevel(0);
        }
        SensitivitySlider.value = (PlayerPrefs.GetInt("ControlsSensitivity") / 10f); //for control sensitivity
        effectMusic.volume = PlayerPrefs.GetInt("EffectValue");
        rccSettings.UIButtonSensitivity = SensitivitySlider.value * 10;   
        currentVehicle = PlayerPrefs.GetInt("SelectedVehicle");
    
    }

    public GameObject Removeads, buyall, BuyAllCar;
    public void CheckPurchaseBTNS()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            Removeads.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Unlockall") == 1 || (PlayerPrefs.GetInt("car1") == 1) && PlayerPrefs.GetInt("car2") == 1 && PlayerPrefs.GetInt("car3") == 1 && PlayerPrefs.GetInt("car4") == 1 && PlayerPrefs.GetInt("car5") == 1 && PlayerPrefs.GetInt("car6") == 1 && PlayerPrefs.GetInt("car7") == 1 && PlayerPrefs.GetInt("car8") == 1 && PlayerPrefs.GetInt("car9") == 1 && PlayerPrefs.GetInt("car10") == 1 && PlayerPrefs.GetInt("car11") == 1 && PlayerPrefs.GetInt("car12") == 1 && PlayerPrefs.GetInt("car13") == 1 && PlayerPrefs.GetInt("car14") == 1 && PlayerPrefs.GetInt("car15") == 1 && PlayerPrefs.GetInt("car16") == 1 && PlayerPrefs.GetInt("car17") == 1 && PlayerPrefs.GetInt("car18") == 1 && PlayerPrefs.GetInt("car19") == 1 && PlayerPrefs.GetInt("car20") == 1 && PlayerPrefs.GetInt("car21") == 1 && PlayerPrefs.GetInt("UnlockedSimpleLevels") == 20 && PlayerPrefs.GetInt("UnlockedExtremeLevels") == 18 && PlayerPrefs.GetInt("UnlockedProLevels") == 18 && PlayerPrefs.GetInt("UnlockedParkingLevels") == 48)
        {
            buyall.SetActive(false);
        }

    }
    public GameObject[] Hideobjects, showObjects;
    public GameObject applyBtn;
    public void openCarSelection()
    {       
        activateCameraScript();
        CarSelection.SetActive(true);
        //LeaderBoard.SetActive(false);
        //restoreBtn.SetActive(false);
        applyBtn.SetActive(false);
        CheckVehicle();          
        CheckColorPurchased();         
        for (int i = 0; i < Hideobjects.Length; i++)
        {
            Hideobjects[i].SetActive(false);
        }
        for (int i = 0; i < showObjects.Length; i++)
        {

            showObjects[i].SetActive(true);
        }
        PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);
        
    }

    public void hidePurchaseBTN()
    {

        for (int i = 1; i < PurchaseBTN.Length; i++)
        {

            PurchaseBTN[i].SetActive(false);
        }

    }
    public void GiveCoins()
    {
        PlayerPrefs.SetInt("TotalPoints", (PlayerPrefs.GetInt("TotalPoints")) + 5000);
        UpdatePoints();
    }
    public void checkfreeCoins()
    {
        Adobj.RewardStatus = 1;
        Adobj.ShowRewardBasedVideo();

    }
    public void opensuv1()
    {
        PlayerPrefs.SetInt("car1", 1);
        CheckPurchaseBTNS();
        checkVehicle2();

    }
    public void opensuv2()
    {
        PlayerPrefs.SetInt("car2", 1);
        CheckPurchaseBTNS();
        checkVehicle2();

    }
    public void opensuv3()
    {
        PlayerPrefs.SetInt("car3", 1);
        CheckPurchaseBTNS();
        checkVehicle2();

    }
    public void opensuv4()
    {
        PlayerPrefs.SetInt("car4", 1);
        CheckPurchaseBTNS();
        checkVehicle2();

    }
    public void opensuv5()
    {
        PlayerPrefs.SetInt("car5", 1);
        CheckPurchaseBTNS();
        checkVehicle2();

    }
    public void opensuv6()
    {
        PlayerPrefs.SetInt("car6", 1);
        CheckPurchaseBTNS();
        checkVehicle2();

    }
    public void opensuv7()
    {
        PlayerPrefs.SetInt("car7", 1);
        CheckPurchaseBTNS();
        checkVehicle2();

    }
    public void opensuv8()
    {
        PlayerPrefs.SetInt("car8", 1);
        CheckPurchaseBTNS();
        checkVehicle2();

    }
    public void opensuv9()
    {
        PlayerPrefs.SetInt("car9", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }

    public void opensuv10()
    {
        PlayerPrefs.SetInt("car10", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void opensuv11()
    {
        PlayerPrefs.SetInt("car11", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void opensuv12()
    {
        PlayerPrefs.SetInt("car12", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void opensuv13()
    {
        PlayerPrefs.SetInt("car13", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void opensuv14()
    {
        PlayerPrefs.SetInt("car14", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void opensuv15()
    {
        PlayerPrefs.SetInt("car15", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void opensuv16()
    {
        PlayerPrefs.SetInt("car16", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void opensuv17()
    {
        PlayerPrefs.SetInt("car17", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void opensuv18()
    {
        PlayerPrefs.SetInt("car18", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void opensuv19()
    {
        PlayerPrefs.SetInt("car19", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void opensuv20()
    {
        PlayerPrefs.SetInt("car20", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void opensuv21()
    {
        PlayerPrefs.SetInt("car21", 1);
        CheckPurchaseBTNS();
        checkVehicle2();
    }
    public void UnlockAllVehicles()
    {
        PlayerPrefs.SetInt("car1", 1);
        PlayerPrefs.SetInt("car2", 1);
        PlayerPrefs.SetInt("car3", 1);
        PlayerPrefs.SetInt("car4", 1);
        PlayerPrefs.SetInt("car5", 1);
        PlayerPrefs.SetInt("car6", 1);
        PlayerPrefs.SetInt("car7", 1);
        PlayerPrefs.SetInt("car8", 1);
        PlayerPrefs.SetInt("car9", 1);
        PlayerPrefs.SetInt("car10", 1);
        PlayerPrefs.SetInt("car11", 1);
        PlayerPrefs.SetInt("car12", 1);
        PlayerPrefs.SetInt("car13", 1);
        PlayerPrefs.SetInt("car14", 1);
        PlayerPrefs.SetInt("car15", 1);
        PlayerPrefs.SetInt("car16", 1);
        PlayerPrefs.SetInt("car17", 1);
        PlayerPrefs.SetInt("car18", 1);
        PlayerPrefs.SetInt("car19", 1);
        PlayerPrefs.SetInt("car20", 1);
        PlayerPrefs.SetInt("car21", 1);
        PlayerPrefs.SetInt("car22", 1);
        BuyAllCar.SetActive(false);
    }
    public void Givefourkcoins()
    {
        PlayerPrefs.SetInt("TotalPoints", (PlayerPrefs.GetInt("TotalPoints")) + 4000);
        UpdatePoints();

    }
    public void fourkcall()
    {
        inappobj.BuyFourhundredK();
    }
    public void Buysuv1()
    {
        inappobj.BuySUVone();
    }
    public void Buysuv2()
    {
        inappobj.BuySUVtwo();
    }
    public void BuyUnlockallvehicle()
    {
        inappobj.BuyAllCars_click();
    }
    public void BuyUnlockeverything()
    {
        inappobj.BuyUnlockAll();
    }
    public void BuyRemoveAds()
    {
        inappobj.Buyremoveads();
    }

    public Text NameText, placeholder;
    public GameObject WelcomePanel;
    public void SaveName()
    {
        if (NameText.text == "")
        {
            placeholder.text = "Please Enter Username";
        }
        else
        {
            PlayerPrefs.SetString("UserName", NameText.text);
            PlayerPrefs.SetInt("FirstTime", 1);
            WelcomePanel.SetActive(false);
            Upperpanel.SetActive(true);
            LowerPanel.SetActive(true);
            Showname();
            //EnableCarTouchScript();
        }
    }
    public void SkipBtn()
    {

        PlayerPrefs.SetString("UserName", "Guest");
        PlayerPrefs.SetInt("FirstTime", 1);
        WelcomePanel.SetActive(false);
        Upperpanel.SetActive(true);
        LowerPanel.SetActive(true);
        Showname();
        //EnableCarTouchScript();
    }

  
    public void Showname()
    {
        ShowName.text = PlayerPrefs.GetString("UserName");
    }

    public void QuitYes()
    {
        Application.Quit();
    }

    public void GetVehicle()
    {
        if (PlayerPrefs.GetInt("Giftcar1") == 1)
        {
            currentVehicle = 1;
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);  //GiftCAR
            PlayerVehicles[currentVehicle].SetActive(true);
            PlayerPrefs.SetInt("car" + currentVehicle, 1);           
            BuyBTN.SetActive(false);
            PlayBTN.gameObject.SetActive(true);
            backbtnGarage.SetActive(true);
            PurchaseBTN[currentVehicle].SetActive(false);
        }
        if (PlayerPrefs.GetInt("Giftcar7") == 1)
        {
            currentVehicle = 7;
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);   //GIFTCAR
            PlayerVehicles[currentVehicle].SetActive(true);
            PlayerPrefs.SetInt("car" + currentVehicle, 1);
            BuyBTN.SetActive(false);
            PlayBTN.gameObject.SetActive(true);
            backbtnGarage.SetActive(true);
            PurchaseBTN[currentVehicle].SetActive(false);
        }
        if (PlayerPrefs.GetInt("Giftcar14") == 1)
        {
            currentVehicle = 14;
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);   //GIFTCAR
            PlayerVehicles[currentVehicle].SetActive(true);
            PlayerPrefs.SetInt("car" + currentVehicle, 1);
            BuyBTN.SetActive(false);
            PlayBTN.gameObject.SetActive(true);
             backbtnGarage.SetActive(true);
            PurchaseBTN[currentVehicle].SetActive(false);
        }
        if (PlayerPrefs.GetInt("Getcar3") == 1)
        {
            currentVehicle = 2;
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);
            PlayerVehicles[currentVehicle].SetActive(true);
        }
        if (PlayerPrefs.GetInt("Getcar4") == 1)
        {
            currentVehicle = 3;
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);
            PlayerVehicles[currentVehicle].SetActive(true);
           
        }
        if (PlayerPrefs.GetInt("Getcar5") == 1)
        {
            currentVehicle = 4;
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);
            PlayerVehicles[currentVehicle].SetActive(true);
          
        }
        if (PlayerPrefs.GetInt("Getcar6") == 1)
        {
            currentVehicle = 5;
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);
            PlayerVehicles[currentVehicle].SetActive(true);
          
        }
        if (PlayerPrefs.GetInt("Getcar7") == 1)
        {
            currentVehicle = 6;
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);
            PlayerVehicles[currentVehicle].SetActive(true);
           
        }
    
        if (PlayerPrefs.GetInt("Getcar8") == 1)
        {
            currentVehicle = 8;
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);
            PlayerVehicles[currentVehicle].SetActive(true);
          
        }
        if (PlayerPrefs.GetInt("Getcar9") == 1)
        {
            currentVehicle = 9;
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);
            PlayerVehicles[currentVehicle].SetActive(true);
       
        }
        if (PlayerPrefs.GetInt("Getcar10") == 1)
        {
            currentVehicle = 10;
            PlayerPrefs.SetInt("SelectedVehicle", currentVehicle);
            PlayerVehicles[currentVehicle].SetActive(true);      
        }     
        PlayerPrefs.SetInt("car" + currentVehicle, 1);
    }

    public GameObject buyColorBtn;
    public GameObject buyTextureBtn;
    public GameObject buyRimBtn;
    public GameObject buyWindowBtn;
    public GameObject buyNeonBtn;
    public GameObject[] rimLocks;
    public GameObject[] textureLocks;
    public GameObject[] windowLocks;
    public GameObject[] neonLocks;
    public GameObject[] colorLocks;

  
    

  

  

   
    
}

