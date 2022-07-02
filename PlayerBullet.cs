using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Bullet
/// </summary>
public class PlayerBullet : MonoBehaviour
{
	[Header("Parameter")]
	public float m_move_speed = 2;
	public float m_life_time = 6;
	public int damage;
    public float speed =4;
    public int placement;
    private Rigidbody myRigidbody;
	public bool enemyBullet;
	public bool destroyedByCollision;
    [HideInInspector]public Player PlayerMine;
    [HideInInspector] public StageLoop SL;
    private void Start()
    {
        PlayerMine = FindObjectOfType<Player>();
        SL = FindObjectOfType<StageLoop>();
    }
    void Update()
    {
        switch (placement)
        {
            case 0: transform.position += new Vector3(0, 1, 0) * m_move_speed * Time.deltaTime;
                break;
            case 1:
                transform.position += new Vector3(speed, 1, 0) * m_move_speed * Time.deltaTime;
                break;
            case 2:
                transform.position += new Vector3(-speed, 1, 0) * m_move_speed * Time.deltaTime;
                break;
            default:
                break;
        }
        
        m_life_time -= Time.deltaTime;
        if (m_life_time <= 0)
        {
            DeleteObject();
        }
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
