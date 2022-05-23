using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackChange : MonoBehaviour
{

    public Sprite Green, Red;
    public GameObject GreenTrack, RedTrack;
    Image img;
    bool TrainTurn = false;

    void Start()
    {
        img = GetComponent<Image>();
    }
    public void TurnRightTrain()
    {
        TrainTurn = !TrainTurn;
        if (TrainTurn)
        {

            img.sprite = Green;
            GreenTrack.SetActive(true);
            RedTrack.SetActive(false);
        }
        else
        {
            img.sprite = Red;
            RedTrack.SetActive(true);
            GreenTrack.SetActive(false);
        }
    }    

}

