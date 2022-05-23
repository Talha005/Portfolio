using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiconRace : MonoBehaviour
{

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AiLocomotive")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
