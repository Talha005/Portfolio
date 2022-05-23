using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrafficManager : MonoBehaviour
{
    public GameObject Tutorial1, Tutorial2, TutorialColliderSlider;
    bool increase;
    void OnTriggerEnter(Collider col)
    {
      

        if (col.gameObject.tag == "TuTCollider")
        {
            Time.timeScale = 0;
            Tutorial1.SetActive(true);
            Tutorial2.SetActive(true);
        }
        if (col.gameObject.tag == "Slidertut")
        {
            Debug.Log(col.gameObject.name);
            TutorialColliderSlider.SetActive(true);
            //Time.timeScale = 0;
        }
       
    }

    public void Resume()
    {
        if (increase)
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
