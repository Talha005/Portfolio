using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Player;
using UnityEngine.SceneManagement;
 

public class GameManager : MonoBehaviour
{

    [Header("Player Object Attributes")]
    public GameObject Player; 

    [Header("Ball Controller Attributes")]
    public BallController BallController;

    [Header("Sinking In Water Event Attributes")]
    public ParticleSystem SinkingInWaterVFX;
    public ParticleSystem BubbleRisingInWaterVFX;
    public GameObject SinkingInWaterUI;

    [Header("Finished Level Event Attributes")]
    public ParticleSystem FinishedLevelCofettiShowerVFX;
    public ParticleSystem FinishedLevelCofettiBlastVFX;
    public GameObject FinishedLevelUI;
    public Zone currentZone;
    public Level currentLevel;


    #region Singelton Region

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    private void Start()
    {
       // LoadingScreenUI.EndLoadingScreenEvent?.Invoke();
    }

    #region GamePlay Events

    public void OnPlayerFallInWater(Vector3 collisionPoint)
    {
        StartCoroutine(PlayerFallInWaterCorotinue(collisionPoint));
    }

    private IEnumerator PlayerFallInWaterCorotinue(Vector3 collisionPoint)
    {
        //Instantiate VFX.
        Instantiate(SinkingInWaterVFX, collisionPoint, Quaternion.identity);
        Instantiate(BubbleRisingInWaterVFX, Player.transform.position - new Vector3(0f,1f,0f), Quaternion.identity);

        //FreezePlayersMovement
        yield return new WaitForSeconds(0.3f);
        BallController.dead = true;
        //FreezePlayersMovement();

        //Pause game & Show UI.
        yield return new WaitForSeconds(3f); 
        SinkingInWaterUI.SetActive(true);
    }

    [ContextMenu("Win")]
    public void OnPlayerFinishedLevel()
    {
        StartCoroutine(PlayerFinishedLevelCorotinue());
    }

    private IEnumerator PlayerFinishedLevelCorotinue()
    {
        FinishedLevelCofettiShowerVFX.Play();
        FinishedLevelCofettiBlastVFX.Play();
        //  FreezePlayersMovement();
        SetPlayerData();
        BallController.dead = true;

        yield return new WaitForSeconds(3f); 
        FinishedLevelUI.SetActive(true);
        yield return new WaitForSeconds(3f);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Overworld Map");

    }

    void SetPlayerData()
    {
        Debug.Log("Setplayerdata"+ SavingSytemManager.Instance.currentGameSlot.CurrentLevel.levelIndex);
        //Get the level next to current one and set it.
        //Level level = SavingSytemManager.Instance.currentGameSlot.CurrentZone.levelsList[SavingSytemManager.Instance.currentGameSlot.CurrentLevel.levelIndex];
        Level level = currentLevel;
        level.SaveLevel();
        SavingSytemManager.Instance.currentGameSlot.CurrentLevel = level;
        SavingSytemManager.Instance.currentGameSlot.lastLevelReachedIndex = level.levelIndex;

        SavingSytemManager.Instance.SaveCurrentGameSlot(SavingSytemManager.Instance.currentGameSlot);
    }

    #endregion

    #region Player Controlling Events

    private void FreezePlayersMovement()
    {
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    #endregion

    #region Scene Management Events

    public void RestartLevel()
    {
        SceneLoader.Instance.GoToScene(SceneManager.GetActiveScene().name);
        //LoadingManager.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex); 

    }

    public void LoadScene(string sceneName)
    { 
        if (!sceneName.Equals(""))
        {
            SceneLoader.Instance.GoToScene(sceneName);
           // LoadingManager.Instance.LoadScene(sceneName);
        }
    }

    #endregion

}
