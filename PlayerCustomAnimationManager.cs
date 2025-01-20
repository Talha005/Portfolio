using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomAnimationManager : MonoBehaviour
{
    public GameObject Player;
    Vector3 initial;

    private void Start()
    {
        initial = Player.transform.position;

        StartCoroutine(AnimatePlayer());
    }

    Vector3 GetRandomPosition()
    {
        float RandomX = Random.Range(-1f, 1f);
        float RandomY = Random.Range(1.5f, 2.5f);

        return new Vector3(RandomX, RandomY, 0f);
    }

    private IEnumerator AnimatePlayer()
    {
        while (true)
        {
            LeanTween.moveLocal(Player, GetRandomPosition(), 0.6f);
            yield return new WaitForSeconds(0.6f);

            LeanTween.move(Player, initial, 0.6f);
            yield return new WaitForSeconds(0.6f);
        }    
    }
}
