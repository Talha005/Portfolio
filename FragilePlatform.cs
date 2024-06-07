using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    public float disableDelay = 2f;     
    public float enableDelay = 3f;       
    private bool canBeDisabled = true;  

    private void OnCollisionEnter(Collision collision)
    {
        if (canBeDisabled && collision.gameObject.CompareTag("Player"))
        {
            Invoke("DisablePlatform", disableDelay);
        }
    }

    private void DisablePlatform()
    {
        gameObject.SetActive(false);

        Invoke("EnablePlatform", enableDelay);
    }

    private void EnablePlatform()
    {
        gameObject.SetActive(true);
        canBeDisabled = true;
    }
}
