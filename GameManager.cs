using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMAnager : MonoBehaviour {
    public RCC_Settings rccSettings;
    public int currentLevel;
    public GameObject LevelFailPanel, LevelPausePanel, LevelCompletePanel,HitsPanel,TimePanel;
    public GameObject[] OptionsObjects;
    bool useSteeringWheel; // for RCC_UISteeringWheelController check.
    public int hits;
    public Text hitsLabel;
    public bool useGear;
	void Start () {
        Time.timeScale = 1f;
        GetLevel();
        UpdateHits();
        SetControls();
        if (useGear)
        {

            rccSettings.autoReverse = false;
        }
        else
        {
            rccSettings.autoReverse = true;
        
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GetLevel()
    {

        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }
    public void MissionComplete()
    {
        HideObjects();
        LevelCompletePanel.SetActive(true);
    }
    public void SetControls() //Set controls buttons of the vehicles.
    {
        if (PlayerPrefs.GetInt("Controls") == 1)
        {
            rccSettings.useAccelerometerForSteering = false;
            rccSettings.useSteeringWheelForSteering = true;
            useSteeringWheel = true;
        }
        else if (PlayerPrefs.GetInt("Controls") == 2)
        {
            rccSettings.useSteeringWheelForSteering = false;
            rccSettings.useAccelerometerForSteering = true;
            useSteeringWheel = false;
        }
        else
        {
            rccSettings.useSteeringWheelForSteering = false;
            rccSettings.useAccelerometerForSteering = false;
            useSteeringWheel = false;
        }
    }
    public void HideObjects()
    {
        for (int i = 0; i < OptionsObjects.Length; i++)
        {
            OptionsObjects[i].SetActive(false);

        }
        RCC_Settings.instance.useSteeringWheelForSteering = false;
    }
    public void ShowObjects()
    {
        for (int i = 0; i < OptionsObjects.Length; i++)
        {
            OptionsObjects[i].SetActive(true);
        }
        if (useSteeringWheel)
        {
            RCC_Settings.instance.useSteeringWheelForSteering = true;
        }
    }
    public void MissionFail()
    {
        HideObjects();

        LevelFailPanel.SetActive(true);
    }
    public void PauseButton()
    {
        Time.timeScale = 0;
        HideObjects();
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        ShowObjects();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {

        PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
        // PlayerPrefs.SetInt("InGame", 1);


    }
    public void UpdateHits()
    {
        if (hits >= 0)
            hitsLabel.text = hits.ToString();
        else
            hitsLabel.text = "0";
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
