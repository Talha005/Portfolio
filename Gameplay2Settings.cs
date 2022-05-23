using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Utility;
public class Gameplay2Settings : MonoBehaviour
{
    [HideInInspector]
    public GameObject Player;
    public GameObject[] PlayerVehicles;
    [HideInInspector]
    public RCC_CarControllerV3 playerCarController;
    Rigidbody PlayerRB;
    [HideInInspector]
    public PlayerCarScript PlayerScript;
    public LevelDataScript[] LevlesScript;
    [HideInInspector]
    public int currentLevel;
    [HideInInspector]
    public Transform spawnPoint;
    public SmoothFollow PlayerCamera;
    public lookAtDest ArrowScript;
    bool MissionCompletebool;
    GameObject ProgressBar;
    int CurrentCamera;
    public Image ParkingProgressBarImage;
    public Text ParkingProgressBarText;
    float Progress = 0f;
    int CurrentParkingArea = 0;
    GameObject[] ParkingArea;
    public GameObject StopMark, ReverseMark;
    public  bool changeCamHeight;
   public bool firstTimeGearChange; 
   bool damageStart=true;
	void Start () 
    {
        Time.timeScale = 1f;
        setEnviornment();
        SpawnPlayerVehicle();
        GetValues();
        ProgressBar = ParkingProgressBarImage.transform.parent.gameObject;
       
	}
    void Update()
    {
        if (!MissionCompletebool)
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
                    Progress += Time.timeScale * 4f;
                    ParkingProgressBarImage.fillAmount = Progress / 100;
                    ParkingProgressBarText.text = ((int)Progress).ToString() + " <size=24><b>%</b></size>";
                }
                else if (Progress >= 100f)
                {
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
                            Invoke("MissionComplete", 1f);
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
    }
    public void SpawnPlayerVehicle()
    {
        Player = Instantiate(PlayerVehicles[PlayerPrefs.GetInt("SelectedVehicle")], spawnPoint.position, spawnPoint.rotation);
        Player.name = "Player";
        playerCarController = Player.GetComponent<RCC_CarControllerV3>();
        PlayerScript = Player.GetComponent<PlayerCarScript>();
        PlayerRB=Player.GetComponent<Rigidbody>();
    }
    public void GetValues()
    {
        PlayerCamera.target = Player.GetComponent<PlayerCarScript>().CameraTarget;
        gameObject.GetComponent<CameraRotate>().CarCamObj = PlayerCamera.target;
        PlayerCamera.distance = Player.GetComponent<RCC_CameraConfig>().distance;
        PlayerCamera.height = Player.GetComponent<RCC_CameraConfig>().height;
    } 
    public void setEnviornment()
    {
		Debug.Log (PlayerPrefs.GetInt("CurrentLevel")+"--");
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        LevlesScript[currentLevel].gameObject.SetActive(true);
        spawnPoint = LevlesScript[currentLevel].spawnPoint;
        ParkingArea = new GameObject[LevlesScript[currentLevel].ParkingSlot.Length];
        for (int i = 0; i < ParkingArea.Length; i++)
        {
            ParkingArea[i] = LevlesScript[currentLevel].ParkingSlot[i];
        }
    }
    public void ChangeCamera()
    {
        if (PlayerCamera.target.localEulerAngles.y != 0f && playerCarController.direction == 1)
        {
            PlayerCamera.target.localEulerAngles = Vector3.zero;
        }
        else if (PlayerCamera.target.localEulerAngles.y != 180f && playerCarController.direction == -1)
        {
            PlayerCamera.target.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
       else if (CurrentCamera == 1)
        {
            PlayerCamera.transform.parent = null;
            PlayerCamera.enabled = true;
            CurrentCamera=0;
        }
        else if (CurrentCamera == 0)
        {
            PlayerCamera.transform.parent = PlayerScript.InnerCameraPosition;
            PlayerCamera.transform.localPosition = Vector3.zero;
            PlayerCamera.transform.localEulerAngles = Vector3.zero;
            PlayerCamera.enabled = false;
            CurrentCamera = 1;
        }
    }
    public void GetDamage()
    {
        if (damageStart)
        {
           
        }
    }
    void StartDamage()
    {
        damageStart = true;
    }

}

