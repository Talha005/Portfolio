using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCounter : MonoBehaviour {

    // Use this for initialization
    public GameObject Counter;
    int numtrain = 4;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Locomotive")
        {
               numtrain++;

            if (numtrain == 4)
            {
               // gameplaypanel[0].SetActive(true);
              
            }
            else
            {
            }
        }
    }
    void Delayd()
    {
        //score.SetActive(false);
        Destroy(this.gameObject);
    }
    void OnDisable()
    {

        if (IsInvoking("Delayd"))
            CancelInvoke("Delayd");
    }
}
