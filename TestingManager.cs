using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingManager : MonoBehaviour
{

    public Transform[] levelSpawns;
    public GameObject player;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            player.transform.position = levelSpawns[0].transform.position;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            player.transform.position = levelSpawns[1].transform.position;

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            player.transform.position = levelSpawns[2].transform.position;

        }

    }


}
