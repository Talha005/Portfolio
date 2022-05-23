using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpwan : MonoBehaviour 
{
    public GameObject[] AiObject;
    void start()
    {
     
    }

    void OnTriggerEnter(Collider other)
    {
       
       

        if (other.gameObject.tag == "Locomotive")
        {
            
            for (int i = 0; i < AiObject.Length; i++)
            {
                AiObject[i].SetActive(true);
            }

        }
    }
}
