using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeftRightTrackChange : MonoBehaviour
{

    // Use this for initialization
    [HideInInspector]
    public int lefttrainchangeint, righttrainchangeint;
    public GameObject greenleft, greenright;
    public GameObject greenleftturn, greenrightturn;
    public GameObject leftbtnmat, rightbtnmat;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print(lefttrainchangeint);
        print(righttrainchangeint);
    }
    public void lefttraindir()
    {

        if (lefttrainchangeint == 0)
        {
            print("Turnleffttttt");
            lefttrainchangeint = 1;
            righttrainchangeint = 0;
            greenleft.SetActive(true);
            greenright.SetActive(false);
            leftbtnmat.GetComponent<Image>().color = Color.green;
        }
        else if (lefttrainchangeint == 1)
        {
            lefttrainchangeint = 0;
            righttrainchangeint = 0;
            greenleft.SetActive(false);
            leftbtnmat.GetComponent<Image>().color = Color.red;
        }



    }
    public void Righttraindir()
    {


        if (righttrainchangeint == 0)
        {
            righttrainchangeint = 1;
            lefttrainchangeint = 0;
            greenleft.SetActive(false);
            greenright.SetActive(true);
            rightbtnmat.GetComponent<Image>().color = Color.green;
        }
        else if (righttrainchangeint == 1)
        {
            righttrainchangeint = 0;
            lefttrainchangeint = 0;
            greenright.SetActive(false);
            rightbtnmat.GetComponent<Image>().color = Color.red;
        }



    }
    /*public void indicatoroff()
    {
        lefttrainchangeint = 0;
        righttrainchangeint = 0;
        greenleft.SetActive(false);
        greenright.SetActive(false);
        rightbtnmat.GetComponent<Image>().color = Color.white;
        leftbtnmat.GetComponent<Image>().color = Color.white;
    }*/

}