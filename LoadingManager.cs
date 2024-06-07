using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private static LoadingManager mInstance;

    [SerializeField]
    private float sceneLoadDelayTime = 3f;

    public float SceneLoadDelayTime {
        get { return sceneLoadDelayTime; }
        set { sceneLoadDelayTime = value; }
    }

    public static LoadingManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject go = new GameObject();
                go.name = "LoadingManager";
                mInstance = go.AddComponent<LoadingManager>();
                DontDestroyOnLoad(go);
            }
            return mInstance;
        }
    }
     

    // ------------------------- public methods ----------------------------

    public void LoadScene(int index)
    {
        LoadingScreenUI.StartLoadingScreenEvent?.Invoke();
        StartCoroutine(LoadSceneCoroutine(index));
    }

    public void LoadScene(int index, float delayTime)
    {
        LoadingScreenUI.StartLoadingScreenEvent?.Invoke();
        StartCoroutine(LoadSceneCoroutine(index, delayTime));
    }

    public void LoadScene(string sceneName)
    {
        LoadingScreenUI.StartLoadingScreenEvent?.Invoke();
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    public void LoadScene(string sceneName, float delayTime)
    {
        LoadingScreenUI.StartLoadingScreenEvent?.Invoke();
        StartCoroutine(LoadSceneCoroutine(sceneName, delayTime));
    }

    public string GetCurrentSceneName()
    { 
        string currentScene = SceneManager.GetActiveScene().name;

        return currentScene;
    }

    // ------------------------- private methods ---------------------------

    private IEnumerator LoadSceneCoroutine(int index) {
        yield return new WaitForSeconds(SceneLoadDelayTime);
        SceneManager.LoadScene(index); 
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(SceneLoadDelayTime);
        SceneManager.LoadScene(sceneName); 
    }

    private IEnumerator LoadSceneCoroutine(int index, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(index); 
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName); 
    }



}
