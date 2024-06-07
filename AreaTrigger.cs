using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaTrigger : MonoBehaviour
{
    /// <summary>
    /// will be used to expose trigger events for external uses, allow for a modular approach to events
    /// </summary>
    public UnityEvent onEnter, onExit;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            onEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            onExit?.Invoke();
    }

}
