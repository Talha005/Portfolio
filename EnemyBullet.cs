using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector] public Player PlayerMine;
    [HideInInspector] public StageLoop SL;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMine = FindObjectOfType<Player>();
        SL = FindObjectOfType<StageLoop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteObject()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DeleteObject();
            PlayerMine.Destruction();
            SL.gameover.Play();
            SL.Failed.SetActive(true);
        }
    }
}
