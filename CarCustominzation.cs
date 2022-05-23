using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarCustominzation : MonoBehaviour
{
	public float FrontCamber;
	public float BackCamber;

	public Cars[] car;
	public MainMenuManager menu;
	public GameObject[] spoilerLock;
	public GameObject[] camberFontLock;
	public GameObject[] camberBackLock;
	public GameObject buySpoilerBtn;
	public GameObject buyCamberBtnFront;
	public GameObject buyCamberBtnBack;
	public RCC_Customization CarCustom;
	int newvalues = 0;
	// Start is called before the first frame update
	void Start()
    {		
		Load();
		LoadSpoiler();
		//DeleteSpoiler();
		menu = FindObjectOfType<MainMenuManager>();
		CarCustom = FindObjectOfType<RCC_Customization>();
	}

    public void BuyFrontCamber()
    {
        if (menu.mycash >= 100)
        {
            PlayerPrefs.SetInt("BuyFrontCamber" + newvalues, 1);
			PlayerPrefs.SetInt("FrontCam", newvalues);
			PlayerPrefs.SetFloat(car[PlayerPrefs.GetInt("SelectedVehicle")].name + "_FrontCamber", FrontCamber);
			buyCamberBtnFront.SetActive(false);
            Load();
            menu.mycash -= 100;
			menu.UpdatePoints();
        }
    }
    public void SetFrontCamber(int val)
	{
		newvalues = val;
		
		 if(val == 0) 
			{
			FrontCamber = 0;
		
			}
		else if (val == 1)
		{
			FrontCamber = 10;
			
		}
		else if (val == 2)
		{
			FrontCamber = 20;
			
		}
		else if (val == 3)
		{
			FrontCamber = -10;
			
		}
		else if (val == 4)
		{
			FrontCamber = -20;
			
		}
		//PlayerPrefs.SetInt("FrontCam", val);
		//snap
		if (FrontCamber > -2f && FrontCamber < 2f) FrontCamber = 0;			
		 car[PlayerPrefs.GetInt("SelectedVehicle")].WheelFL.localRotation = Quaternion.Euler(0, 0, -(FrontCamber));
		 car[PlayerPrefs.GetInt("SelectedVehicle")].WheelFR.localRotation = Quaternion.Euler(0, 0, FrontCamber);
		 		
		if (PlayerPrefs.GetInt("BuyFrontCamber" + val) == 1)
        {
            PlayerPrefs.SetFloat(car[PlayerPrefs.GetInt("SelectedVehicle")].name + "_FrontCamber", FrontCamber);
			buyCamberBtnFront.SetActive(false);
			buyCamberBtnBack.SetActive(false);
    }
		else 
		{
			buyCamberBtnFront.SetActive(true);
			buyCamberBtnBack.SetActive(false);
		}
	}
	
	public void BuyBackCamber()
	{
        if (menu.mycash >= 2000)
        {
            PlayerPrefs.SetInt("BuybackCamber" + newvalues, 1);
			PlayerPrefs.SetInt("BackCam", newvalues);
			PlayerPrefs.SetFloat(car[PlayerPrefs.GetInt("SelectedVehicle")].name + "_RearCamber", BackCamber);
			buyCamberBtnBack.SetActive(false);
            Load();
            menu.mycash -= 2000;
			menu.UpdatePoints();
        }
    }
	public void SetBackCamber(int val)
	{
		newvalues = val;
		
		if (val == 0)
		{
			BackCamber = 0;
			
		}
		else if (val == 1)
		{
			BackCamber = 10;
		
		}
		else if (val == 2)
		{
			BackCamber = 20;
	
		}
		else if (val == 3)
		{
			BackCamber = -10;
		
		}
		else if (val == 4)
		{
			BackCamber = -20;
			
		}	
	    //PlayerPrefs.SetInt("BackCam", val);
		
		//snap
		if (BackCamber > -2f && BackCamber < 2f) BackCamber = 0;
	    car[PlayerPrefs.GetInt("SelectedVehicle")].WheelRL.localRotation = Quaternion.Euler(0, 0, -(BackCamber));
		car[PlayerPrefs.GetInt("SelectedVehicle")].WheelRR.localRotation = Quaternion.Euler(0, 0, BackCamber);
		Debug.Log("BAckcamber=" + BackCamber);
		if (PlayerPrefs.GetInt("BuybackCamber" + val) == 1)
		{
			PlayerPrefs.SetFloat(car[PlayerPrefs.GetInt("SelectedVehicle")].name + "_RearCamber", BackCamber);
			buyCamberBtnBack.SetActive(false);
			buyCamberBtnFront.SetActive(false);
        }
        else
        {
            buyCamberBtnBack.SetActive(true);
            buyCamberBtnFront.SetActive(false);
        }
    }
	public void Load()
	{
		
		PlayerPrefs.SetInt("BuybackCamber" + 0, 1);
		PlayerPrefs.SetInt("BuyFrontCamber" + 0, 1);
		for (int j = 0; j < camberFontLock.Length; j++)
		{

			if (PlayerPrefs.GetInt("BuyFrontCamber" + j) == 1)
			{

				camberFontLock[j].SetActive(false);
			}
			else
			{

				camberFontLock[j].SetActive(true);
			}
		}
		for (int k = 0; k < camberBackLock.Length; k++)
		{
			if (PlayerPrefs.GetInt("BuybackCamber" + k) == 1)
			{
				camberBackLock[k].SetActive(false);
			}
			else
			{

				camberBackLock[k].SetActive(true);
			}
		}
		//load details
		FrontCamber = PlayerPrefs.GetFloat(car[PlayerPrefs.GetInt("SelectedVehicle")].name + "_FrontCamber");
		BackCamber = PlayerPrefs.GetFloat(car[PlayerPrefs.GetInt("SelectedVehicle")].name + "_RearCamber");

		//show camber		
		car[PlayerPrefs.GetInt("SelectedVehicle")].WheelFL.localRotation = Quaternion.Euler(0, 0, -(FrontCamber));
		car[PlayerPrefs.GetInt("SelectedVehicle")].WheelFR.localRotation = Quaternion.Euler(0, 0, FrontCamber);
		car[PlayerPrefs.GetInt("SelectedVehicle")].WheelRL.localRotation = Quaternion.Euler(0, 0, -(BackCamber));
		car[PlayerPrefs.GetInt("SelectedVehicle")].WheelRR.localRotation = Quaternion.Euler(0, 0, BackCamber);
	}	

	public void SetSpoiler(int val) 
	{
		newvalues = val;		
        for (int i = 0; i < car[PlayerPrefs.GetInt("SelectedVehicle")].spoiler.Length; i++)
        {
			car[PlayerPrefs.GetInt("SelectedVehicle")].spoiler[i].SetActive(false);
			Debug.Log("SpoilerSEtfalse =");
		}	
		car[PlayerPrefs.GetInt("SelectedVehicle")].spoiler[val].SetActive(true);
		
		if (PlayerPrefs.GetInt("Spoiler" + val) == 1)
		{
			PlayerPrefs.SetInt(PlayerPrefs.GetInt("SelectedVehicle") + "Spoiler", val);
			buySpoilerBtn.SetActive(false);
		}
		else 
		{
			buySpoilerBtn.SetActive(true);
		}
		Debug.Log("ValueSpoiler =" + val);
	}

	public void BuySpoiler() 
   {
      if (menu.mycash >= 2500)
      {
        PlayerPrefs.SetInt("Spoiler" + newvalues, 1);
		PlayerPrefs.SetInt(PlayerPrefs.GetInt("SelectedVehicle") + "Spoiler", newvalues);	
        buySpoilerBtn.SetActive(false);
        LoadSpoiler();
        menu.mycash -= 2500;
		menu.UpdatePoints();
      }
   }
	public void LoadSpoiler()
	{
		
		for (int j = 0; j < spoilerLock.Length; j++)
        {
			if (PlayerPrefs.GetInt("Spoiler" + j) == 1 )
			{
				spoilerLock[j].SetActive(false);		
			}
            else
			{
				spoilerLock[j].SetActive(true);				
			}
        }
        if (!PlayerPrefs.HasKey(PlayerPrefs.GetInt("SelectedVehicle") + "Spoiler"))
        {
          PlayerPrefs.SetInt(PlayerPrefs.GetInt("SelectedVehicle") + "Spoiler", -1);
        }
        if (PlayerPrefs.GetInt(PlayerPrefs.GetInt("SelectedVehicle") + "Spoiler") != -1)
		{
		  car[PlayerPrefs.GetInt("SelectedVehicle")].spoiler[PlayerPrefs.GetInt(PlayerPrefs.GetInt("SelectedVehicle") + "Spoiler")].SetActive(true);			
		}
		
	}
	public void DeleteSpoiler()
	{
		for (int i = 0; i < car[PlayerPrefs.GetInt("SelectedVehicle")].spoiler.Length; i++)
		{
			car[PlayerPrefs.GetInt("SelectedVehicle")].spoiler[i].SetActive(false);
		}
		//PlayerPrefs.SetInt(car[PlayerPrefs.GetInt("SelectedVehicle")] + "Spoiler", -1);
		PlayerPrefs.SetInt(PlayerPrefs.GetInt("SelectedVehicle") + "Spoilerdel", -1);
		
	}
	[System.Serializable]
	public class Cars
	{
		public string name;
		public Transform WheelFL;
		public Transform WheelFR;
		public Transform WheelRL;
		public Transform WheelRR;
		//public Transform Tuning;
		[Header("Default Position")]
		public Vector3 StockWheelFL;
		public Vector3 StockWheelFR;
		public Vector3 StockWheelRL;
		public Vector3 StockWheelRR;
		//public Vector3 StockTuning;

		public GameObject[] spoiler;
	}

	
}
