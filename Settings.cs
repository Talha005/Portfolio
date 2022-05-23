using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Settings : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
//        MusicSlider.value = PlayerPrefs.GetFloat("MusicValue");
		if (PlayerPrefs.GetFloat("MusicValue") == 0.35f) 
        {
			MusicON=true;
			GameMusic.volume = PlayerPrefs.GetFloat("MusicValue");
			GamemusicImg.sprite = ONsprit;
		} 
        else 
        {
			GameMusic.volume = PlayerPrefs.GetFloat("MusicValue");
			GamemusicImg.sprite = OFFsprit;
		}
        Invoke("GetCarSound",0.6f);
		Invoke ("SetValues", 0.6f);
	}

    public int Controls;
    public Image[] qualityBTNs;
	public Image[] ControlImage;
    public Sprite greenBack, blueBAck;
    public Slider SensitivitySlider, MusicSlider, EffectsSlider,CarSoundSlider;
    public RCC_Settings rccSettings;
    public Sprite[] controlsSprite;
    public InGameManager GameManager;
    public AudioSource engineSoundOn, engineSoundOff, engineSoundIdle, reversingSound, brakeSound, windSound,reverse;

	public AudioSource GameMusic,effectMusic;
	public Image GamemusicImg, EffectMusicImg;
	public Sprite ONsprit, OFFsprit;
    public void SetValues()
    {
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
        
        EffectsSlider.value = PlayerPrefs.GetFloat("EffectValue");
        rccSettings.UIButtonSensitivity = SensitivitySlider.value * 10;

        if (PlayerPrefs.GetInt("VeryFirstTime")==0)
        {
            PlayerPrefs.SetInt("VeryFirstTime",1);

            PlayerPrefs.SetFloat("CarValue",0.3f);
        
        
        }
        CarSoundSlider.value = PlayerPrefs.GetFloat("CarValue");
    }

    public void GetCarSound()
    {
        engineSoundOn = GameManager.playerCarController.engineSoundOn;
        engineSoundOff = GameManager.playerCarController.engineSoundOff;
        engineSoundIdle = GameManager.playerCarController.engineSoundIdle;
        reversingSound = GameManager.playerCarController.reversingSound;
        brakeSound = GameManager.playerCarController.brakeSound;
        windSound = GameManager.playerCarController.windSound;
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
		Controls=controlval;
		if (Controls == 0)
		{
			GameManager.useSteeringWheel = false;
			for (int i = 0; i < ControlImage.Length; i++) 
            {
				ControlImage[i].sprite = controlsSprite[1];
			}
			ControlImage[Controls].sprite = greenBack;
			rccSettings.useAccelerometerForSteering = false;
			rccSettings.useSteeringWheelForSteering = false;
			PlayerPrefs.SetInt("Controls",0);

		}
		else if (Controls == 1)
		{
			GameManager.useSteeringWheel = true;
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
			GameManager.useSteeringWheel = false;
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

	bool MusicON,effectON;
    public void SetmusicVolume()
    {
        //PlayerPrefs.SetFloat("MusicValue", MusicSlider.value);
		MusicON = !MusicON;
		if (MusicON)
        {
			PlayerPrefs.SetFloat ("MusicValue", 0.35f);
			GameMusic.volume = 0.35f;
			GamemusicImg.sprite = ONsprit;
		} 
        else 
        {
			PlayerPrefs.SetFloat ("MusicValue", 0f);
			GameMusic.volume = 0f;
			GamemusicImg.sprite = OFFsprit;
		}
    }

    public void SeteffectVolume()
    {
        //PlayerPrefs.SetFloat("EffectValue", EffectsSlider.value);
		effectON = !effectON;
		if (effectON)
        {
			PlayerPrefs.SetInt ("EffectValue", 1);
			effectMusic.volume = 1;
			EffectMusicImg.sprite = ONsprit;
		} 
        else 
        {
			PlayerPrefs.SetInt ("EffectValue", 0);
			effectMusic.volume = 0;
			EffectMusicImg.sprite = OFFsprit;
		}
    }
    public void SetCarSoundVolume()
    {
        PlayerPrefs.SetFloat("CarValue", CarSoundSlider.value);
        engineSoundOn.volume = CarSoundSlider.value;
        engineSoundOff.volume = CarSoundSlider.value;
        engineSoundIdle.volume = CarSoundSlider.value;
        reversingSound.volume = CarSoundSlider.value;
        brakeSound.volume = CarSoundSlider.value;
        windSound.volume = CarSoundSlider.value;
    }
    public void PauseSounds()
    {
        engineSoundOn.enabled = false;
        engineSoundOff.enabled = false;
        engineSoundIdle.enabled = false;
        reversingSound.enabled = false;
        brakeSound.enabled = false;
        windSound.enabled = false;
    }

    public void ResumeSounds()
    {
        engineSoundOn.enabled = true;
        engineSoundOff.enabled = true;
        engineSoundIdle.enabled = true;
        reversingSound.enabled = true;
        brakeSound.enabled = true;
        windSound.enabled = true;
    }

	public void reverseSound()
    {
		reverse.Play ();
	}

}
