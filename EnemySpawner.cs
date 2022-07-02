using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Enemy SpawnPoint
/// </summary>
public class EnemySpawner : MonoBehaviour
{
	[Header("Prefab")]
	public Enemy m_prefab_enemy;
    [Header("Parameter")]
	public float m_spawn_interval = 2;
    public int speed = 10;  
    [HideInInspector] public StageLoop SL;
    //------------------------------------------------------------------------------
    public void Start()
    {
        SL = FindObjectOfType<StageLoop>();
        
    }
    public void StartRunning()
	{
		StartCoroutine(MainCoroutine());
	}

	private IEnumerator MainCoroutine()
	{
		while (true)
		{
			//spawn enemy
			if (m_prefab_enemy)
			{
				Enemy enemy = Instantiate(m_prefab_enemy, transform.parent);
				enemy.transform.position = transform.position;
			}

			yield return new WaitForSeconds(m_spawn_interval);
		}
	}

    public void StartRunning2()
    {
        StartCoroutine(MainCoroutine2());
    }

    private IEnumerator MainCoroutine2()
    {
        while (true)
        {
            //spawn enemy
            if (m_prefab_enemy)
            {
                Enemy enemy = Instantiate(m_prefab_enemy, transform.parent);
                enemy.transform.position = transform.position;              
            }

            yield return new WaitForSeconds(m_spawn_interval);
        }
    }
    //------------------------------------------------------------------------------

    private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 2.0f);
	}
}
