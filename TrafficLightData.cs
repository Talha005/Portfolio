using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrafficLightData : MonoBehaviour {
    public Sprite green, Red;
     Image img;
    bool Stop = false;

    void Start()
    {
        img = GetComponent<Image>();

    }
    public void StopTrain()
    {
        Stop = !Stop;
        if (Stop)
        {

            img.sprite = Red;
        }
        else
        {
            img.sprite = green;           
        }
    }
}
