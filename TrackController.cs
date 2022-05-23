using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public bool Stop = false;
    public void StopTrain()
    {
        Stop = !Stop;
    }  
}
