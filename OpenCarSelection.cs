using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCarSelection : MonoBehaviour {
    public MainMenuManager mainMenuScript;
  
    public void Start()
    {

        mainMenuScript = FindObjectOfType<MainMenuManager>();
       
    }
    void OnMouseDown()
    {
        if (enabled)
        {
            mainMenuScript.openCarSelection();
            enabled = false;
        }
    }
  
}
