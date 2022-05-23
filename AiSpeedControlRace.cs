using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;


public class AiSpeedControlRace : MonoBehaviour
{
    public GamemanagerRace gmr;
    public TrainData TD;
    public TrainManagerRace TM;
    void start()
    {
        gmr = FindObjectOfType<GamemanagerRace>();
        TD = FindObjectOfType<TrainData>();
        TM = FindObjectOfType<TrainManagerRace>();
    }

    public void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Crash")
        {
            gmr.CrashAudio.Play();          
        }
    }
}