using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Cinemachine; 
using Core.Player;

public class DialogueManager : MonoBehaviour
{
     
    public Flowchart flowChart;
    public GameObject playerGO;
    public CinemachineFreeLook playerCamera;
    public GameObject controlsCanvas;

    Rigidbody playerRB;
    float defaultFov = 60f;
    float dialogueFOV = 40f; 

    private void Start()
    { 
            playerRB = playerGO.GetComponent<Rigidbody>();  
    }

    public void TriggerBlock(string blockName)
    {
        flowChart.ExecuteIfHasBlock(blockName);
    }

    public void PrepBallyForDialogue()
    {

        playerGO.GetComponent<BallController>().isInDialogue = true;
        playerRB.freezeRotation = true;   
        LeanTween.value(playerCamera.gameObject, playerCamera.m_Lens.FieldOfView, dialogueFOV, 3f)
    .setOnUpdate(UpdateFieldOfView)
    .setEase(LeanTweenType.easeOutQuad);

        controlsCanvas.SetActive(false);
    }

    public void PrepBallyForPlay()
    {
        playerGO.GetComponent<BallController>().isInDialogue = false;
        playerRB.freezeRotation = false; 
        LeanTween.value(playerCamera.gameObject, playerCamera.m_Lens.FieldOfView, defaultFov, 3f)
    .setOnUpdate(UpdateFieldOfView)
    .setEase(LeanTweenType.easeOutQuad);

        controlsCanvas.SetActive(true);
    }

    public void LookAtObject(Transform objectToLookFrom, Transform objectToLookAt, float YOffset)
    {
        // Calculate the direction from Jena to the target (Bally)
        Vector3 directionToLook = objectToLookAt.position - objectToLookFrom.position;

        // Calculate the rotation that looks in the directionToLook
        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);

        // Apply the rotation to Jena with the additional X-axis offset
        objectToLookFrom.rotation = Quaternion.Euler(objectToLookFrom.rotation.x, targetRotation.eulerAngles.y + YOffset, targetRotation.eulerAngles.z);
    }

    private void UpdateFieldOfView(float value)
    { 
        playerCamera.m_Lens.FieldOfView = value;
    }

}
