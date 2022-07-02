using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Player Character
/// </summary>
public class Player : MonoBehaviour
{
	[Header("Prefab")]
	public PlayerBullet m_prefab_player_bullet;
    public GameObject bullet;
	public GameObject destructionFX;
	[Header("Parameter")]
	public float m_move_speed = 1;
	public Transform firepoint;
	public GameObject regularFire;
	
    [HideInInspector] public StageLoop SL;
    [HideInInspector] public Enemy enemy;
    
	[System.Serializable]
	public class Guns
	{
		public GameObject rightGun, leftGun, centralGun;
	    [HideInInspector]public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX;
	}
	//------------------------------------------------------------------------------
	public Guns guns;
    [Range(1, 4)]
    public int weaponPower = 1; 
    [HideInInspector] public int maxweaponPower = 4;
    public static Player instance;
    private void Start()
    {
		guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
		guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
		guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
		SL = FindObjectOfType<StageLoop>();
        enemy = FindObjectOfType<Enemy>();
	}
	public void StartRunning()
	{
		StartCoroutine(MainCoroutine());
	}

    //
    private IEnumerator MainCoroutine()
	{
		while (true)
		{
			//moving
			{
				if (Input.GetKey(KeyCode.LeftArrow))
				{
					transform.position += new Vector3(-1, 0, 0) * m_move_speed * Time.deltaTime;
                    
                }
				if (Input.GetKey(KeyCode.RightArrow))
				{
					transform.position += new Vector3(1, 0, 0) * m_move_speed * Time.deltaTime;
                   
                }
				if (Input.GetKey(KeyCode.UpArrow))
				{
					transform.position += new Vector3(0, 1, 0) * m_move_speed * Time.deltaTime;
				}
				if (Input.GetKey(KeyCode.DownArrow))
				{
					transform.position += new Vector3(0, -1, 0) * m_move_speed * Time.deltaTime;
				}
				float xPos = Mathf.Clamp(transform.position.x, -9f, 9f);
				float yPos = Mathf.Clamp(transform.position.y, -5f, 5f);
				transform.position = new Vector3(xPos, yPos, 0);
			}

			//shoot
			{
				if (Input.GetKeyDown(KeyCode.Z))
				{
					SL.bulletfire.Play();
                    //Fire();
                    MakeAShot();
                    //PlayerBullet bullet = Instantiate(m_prefab_player_bullet, transform.parent);
                    //bullet.transform.position = transform.position;


                }
			}

			yield return null;
		}
	}

	public void Fire()
	{
		Instantiate(regularFire, firepoint.position, Quaternion.identity);
		Instantiate(regularFire, firepoint.position, Quaternion.Euler(0,0,45));
		Instantiate(regularFire, firepoint.position, Quaternion.Euler(0, 0, 135));
	}

	public void Destruction()
	{
		Instantiate(destructionFX, transform.position, Quaternion.identity); 
		Destroy(gameObject);
	}

	void MakeAShot()
	{
		switch (weaponPower) 
		{
			case 1:	
				MakeShot(bullet, guns.centralGun.transform.position, Vector3.zero);
				guns.centralGunVFX.Play();
				break;
			case 2:
				MakeShot(bullet, guns.rightGun.transform.position, Vector3.zero);
				guns.leftGunVFX.Play();
				MakeShot(bullet, guns.leftGun.transform.position, Vector3.zero);
				guns.rightGunVFX.Play();
				break;
			case 3:
				
				MakeShot(bullet, guns.centralGun.transform.position, Vector3.zero);
				MakeShot(bullet, guns.rightGun.transform.position, new Vector3(0, 0, -5));
				guns.leftGunVFX.Play();
				MakeShot(bullet, guns.leftGun.transform.position, new Vector3(0, 0, 5));
				guns.rightGunVFX.Play();
				break;
			case 4:
				
				MakeShot(bullet, guns.centralGun.transform.position, Vector3.zero);
				MakeShot(bullet, guns.rightGun.transform.position, new Vector3(0, 0, -5));
				guns.leftGunVFX.Play();
				MakeShot(bullet, guns.leftGun.transform.position, new Vector3(0, 0, 5));
				guns.rightGunVFX.Play();
				MakeShot(bullet, guns.leftGun.transform.position, new Vector3(0, 0, 15));
				MakeShot(bullet, guns.rightGun.transform.position, new Vector3(0, 0, -15));
				break;
		}
	}

	void MakeShot(GameObject lazer, Vector3 pos, Vector3 rot) 
	{
		Instantiate(lazer, pos, Quaternion.Euler(rot));
	}

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Destruction();
			SL.gameover.Play();
            SL.Failed.SetActive(true);           
        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destruction();
            SL.gameover.Play();
            SL.Failed.SetActive(true);
        }
    }
}
