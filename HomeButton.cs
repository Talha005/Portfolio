using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    [SerializeField] private string MainMenuSceneName;

    public void OnclickPlayButton()
    { 
        SceneLoader.Instance.LoadScene(MainMenuSceneName);
    }

}
