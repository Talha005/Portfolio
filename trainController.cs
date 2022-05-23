using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.Curvy.Examples;
public class trainController : MonoBehaviour
{
    public List<SplineController> Buggies;
    float TrainSpeed;
    // Use this for initialization
    bool stopped;
    bool change;
    GamemanagerStation gm;
    public GameObject Explode;
    

    void Start()
    {
        TrainSpeed = Buggies[0].Speed;
        gm = FindObjectOfType<GamemanagerStation>();
    }

    void OnTriggerStay(Collider col)
    {
        //Debug.Log(col.gameObject.name);

        if (col.CompareTag("detect"))

        {
            if (col.GetComponent<TrackController>().Stop)
            {
                if (!stopped)
                {
                    stopped = true;
                    StopTrain();
                }

            }
            else
            {
                if (stopped)
                {

                    stopped = false;
                    ResumeTrain();
                }

            }
        }
    }


    public void DeactivateTrain()
    {
        for (int i = 0; i < Buggies.Count; i++)
        {
            Buggies[i].gameObject.SetActive(false);

        }

    }
    public void StopTrain()
    {
        for (int i = 0; i < Buggies.Count; i++)
        {
            Buggies[i].Speed = 0f;

        }
       
    }

    public void ResumeTrain()
    {
        for (int i = 0; i < Buggies.Count; i++)
        {
            Buggies[i].Speed = TrainSpeed;          
        }
    }

    public void ChangeTrainSpeed(bool increase)
    {
        if (!stopped)
        {
            if (increase)
            {
                for (int i = 0; i < Buggies.Count; i++)
                {
                    Buggies[i].GetComponent<SplineController>().Speed += 25;
                }
            }
            else
            {
                for (int i = 0; i < Buggies.Count; i++)
                {
                    Buggies[i].GetComponent<SplineController>().Speed -= 25;
                }
            }
        }
    }
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "AiLocomotive")
        {

            if (!Buggies.Contains(col.gameObject.GetComponent<SplineController>()))
            {
                Explode.SetActive(true);
                gm.levelFailed();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
       
        if (col.CompareTag("JunctionCollide"))

        {       
            if (col.GetComponent<JunctionController>().usejunction)
            {
                if (!change)
                {
                    change = true;
                    JunctionOn();                 
                }
            }
            else
            {
                if (change)
                {
                    change = false;
                    JunctionOff();
                }
            }
        }
    }

  
    public void JunctionOn()
    {
        for (int i = 0; i < Buggies.Count; i++)
        {
           
            Buggies[i].connectionBehavior = SplineControllerConnectionBehavior.RandomSpline;
            Buggies[i].allowDirectionChange = true;
            Buggies[i].rejectCurrentSpline = true;
           
        }     
    }

    public void JunctionOff()
    {
        for (int i = 0; i < Buggies.Count; i++)
        {
            print("OOOFFFFFFFF");
            Buggies[i].connectionBehavior = SplineControllerConnectionBehavior.CurrentSpline;
            Buggies[i].allowDirectionChange = false;
            Buggies[i].rejectCurrentSpline = false;
        }
    }

   
}
