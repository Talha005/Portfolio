using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum LoadingScreenUIStates
{
    Invalid = -1,
    SceneStart,
    SceneEnd,
    FadingIn,
    FadingOut,
    ShowingImage,
    Dark,
}

public class LoadingScreenUI : MonoBehaviour
{
    public static UnityEvent<LoadingScreenUIStates, LoadingScreenUIStates> loadingScreenStateChanged = new UnityEvent<LoadingScreenUIStates, LoadingScreenUIStates>();

    [Header("Refrences")]
    public LoadingScreenUIStates state = LoadingScreenUIStates.Invalid;
    public Slider slider;
    public Image LoadingImage;
    public GameObject LoadingContainer;
    public TMP_Text LoadingTip;
    public TMP_Text LoadingTitle;

    [Header("Loading Screen States Durations")]
    public float FadeOutOnSceneStartDuration = .25f;
    public float imageDisplayDuration = 4f;
    public float fadeDuration = 0.5f;
    public float darkDuration = 2f;

    private bool isPaused = false;

    public List<LoadingImageInfos> commonLoadingImages = new List<LoadingImageInfos>();

    private List<LoadingImageInfos> currentLoadingImages;
    //private List<string> currentLoadingTips;
    //private List<string> currentLoadingTitles;
    private int currentImageIndex = 0;
    //private int currentTipIndex = 0;
    private bool shouldSkipGameplayFadeout = false;
    private FadeManager fadeManager;

    bool hasFinished = false;

    public static UnityEvent StartLoadingScreenEvent = new UnityEvent();
    public static UnityEvent EndLoadingScreenEvent = new UnityEvent();

    public Image LoadingCircle;

    public bool randomImages = false;
    int rndImageNum;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        fadeManager = GetComponent<FadeManager>();
        StartLoadingScreenEvent.AddListener(() => StartLoadingScreen());
        EndLoadingScreenEvent.AddListener(() => StopLoadingScreen());
    }

    private void Start()
    {
        LeanTween.rotateAround(LoadingCircle.gameObject, Vector3.forward, -360, 2f).setLoopClamp();
    }

    private void OnDestroy()
    {
        StartLoadingScreenEvent.RemoveListener(() => StartLoadingScreen());
        EndLoadingScreenEvent.RemoveListener(() => StartLoadingScreen());
    }

    private void ChangeLoadingState(LoadingScreenUIStates newState)
    {
        LoadingScreenUIStates oldState = state;
        state = newState;

        switch (state)
        {
            case LoadingScreenUIStates.FadingIn:
                fadeManager.FadeIn(LoadingImage, fadeDuration);
                break;
            case LoadingScreenUIStates.ShowingImage:
                ShowImage();
                break;
            case LoadingScreenUIStates.FadingOut:
                fadeManager.FadeOut(LoadingImage, fadeDuration);
                break;
            case LoadingScreenUIStates.Dark:
                SetDarkBackground();
                break;
            case LoadingScreenUIStates.SceneStart:
                fadeManager.FadeOutOnSceneStart();
                break;
            case LoadingScreenUIStates.SceneEnd:
                fadeManager.FadeInOnSceneEnd();
                break;
        }

        loadingScreenStateChanged.Invoke(oldState, newState);
    }



    // ensure that the loading image is visiable
    private void ShowImage()
    {
        fadeManager.FadeIn(LoadingImage, 0.4f);
    }

    // ensure that the background is black by hiding the image display
    private void SetDarkBackground()
    {
        fadeManager.FadeOut(LoadingImage, 0.4f);
    }

    // set a random image for the loading image sprite
    private void SetNextBackgroundImage()
    {
        if (randomImages)
        {
            rndImageNum = Random.Range(0, currentLoadingImages.Count);
            LoadingImage.sprite = currentLoadingImages[rndImageNum].sprite;
        }
        else
        {
            LoadingImage.sprite = currentLoadingImages[currentImageIndex].sprite;
            ++currentImageIndex;
            if (currentImageIndex >= currentLoadingImages.Count)
            {
                currentImageIndex = 0;
            }
        }
    }

    // will run when a scene starts loading
    public void StartLoadingScreen(bool skipGameplayFadeout = false)
    {
        currentLoadingImages = commonLoadingImages;
        //currentLoadingTips = getSceneLoadingTips();
        //currentLoadingTitles = getSceneLoadingTitles();
        currentImageIndex = 0;
        shouldSkipGameplayFadeout = skipGameplayFadeout;

        if (!skipGameplayFadeout)
        {
            fadeManager.FadeOut(fadeManager.fadeImage, 0.01f);
        }
        else
        {
            fadeManager.fadeImage.color = Color.black;
        }
        //LoadingTip.gameObject.SetActive(true);
        //LoadingTitle.gameObject.SetActive(true);
        //GetfirstTip();
        LeanTween.alpha(LoadingCircle.gameObject.GetComponent<RectTransform>(), 0, 0.01f).setOnComplete( () => {
            LeanTween.alpha(LoadingCircle.gameObject.GetComponent<RectTransform>(), 1, fadeDuration);
        });
        SetDarkBackground();
        LoadingContainer.SetActive(true);
        SetNextBackgroundImage();
        StartCoroutine(StateHandler());
        StartCoroutine(ChangeSliderValue());
    }



    // will start the phase of stoping the loading screen and showing the game scene
    public void StopLoadingScreen()
    {
        LoadingTip.gameObject.SetActive(false);
        LoadingTitle.gameObject.SetActive(false);
        LeanTween.alpha(LoadingCircle.gameObject.GetComponent<RectTransform>(), 0, fadeDuration);
        StopAllCoroutines();
        StartCoroutine(EndLoadingScreen());
    }

    // the Coroutine that is used to excute the phase of the loadaing screen end
    private IEnumerator EndLoadingScreen()
    {
        //fade to black from loading screen
        slider.gameObject.SetActive(false);

        ChangeLoadingState(LoadingScreenUIStates.FadingOut);
        yield return new WaitForSeconds(fadeDuration);

        // hide the image 
        fadeManager.Fade(LoadingImage, 0, 0.01f, false);

        // make sure that this have a player
        if (!hasFinished)
        {
            //wait untill if
            //can be used if the played did not spawn yet
        }

        //than fade from black to the scene

        ChangeLoadingState(LoadingScreenUIStates.SceneStart);
        yield return new WaitForSeconds(FadeOutOnSceneStartDuration);
        LoadingContainer.SetActive(false);
    }

    //private void Start()
    //{
    //    StartLoadingScreen();
    //}

    IEnumerator ChangeSliderValue()
    {
        while (true)
        {
            //add logic for slider value
            //slider.value = LoadingManager.Instance.GetLoadingProgress();
            yield return null;
        }
    }

    public void SetPause(bool pause)
    {
        isPaused = pause;
    }

    /// <summary>
    /// this function will handel the timings and the states of the loading screen
    /// </summary>
    /// <returns></returns>
    IEnumerator StateHandler()
    {
        while (isPaused) yield return null;

        if (!shouldSkipGameplayFadeout)
        {
            ChangeLoadingState(LoadingScreenUIStates.SceneEnd);
            yield return new WaitForSeconds(FadeOutOnSceneStartDuration);
        }

        //slider.gameObject.SetActive(true);
        while (true)
        {
            while (isPaused) yield return null;
            ChangeLoadingState(LoadingScreenUIStates.FadingIn);
            yield return new WaitForSeconds(fadeDuration);

            while (isPaused) yield return null;
            ChangeLoadingState(LoadingScreenUIStates.ShowingImage);
            yield return new WaitForSeconds(imageDisplayDuration);

            while (isPaused) yield return null;
            ChangeLoadingState(LoadingScreenUIStates.FadingOut);
            yield return new WaitForSeconds(fadeDuration);

            while (isPaused) yield return null;
            ChangeLoadingState(LoadingScreenUIStates.Dark);
            yield return new WaitForSeconds(darkDuration / 2);
            SetNextBackgroundImage();
            yield return new WaitForSeconds(darkDuration / 2);
        }
    }

    /// <summary>
    /// custom class to hold the structure of the custom sprites
    /// </summary>
    [System.Serializable]
    public class SceneLoadingImagesSetup
    {
        public SceneLoadingImagesBindings destinationSceneSetup;
        public List<SceneLoadingImagesBindings> originSceneSpecificSetup = new List<SceneLoadingImagesBindings>();
    }

    [System.Serializable]
    public class SceneLoadingImagesBindings
    {
        public List<LoadingImageInfos> loadingImages = new List<LoadingImageInfos>();
    }

    [System.Serializable]
    public class LoadingImageInfos
    {
        public Sprite sprite;
        public string title;
        public string description;
    }


    /// <summary>
    /// get all tips from the next scene to load
    /// </summary>
    /// <returns></returns>
    public List<string> getSceneLoadingTips()
    {
        List<string> tipsList = new List<string>();
        foreach (LoadingImageInfos tip in currentLoadingImages)
        {
            if (tip.description.Trim() != "")
            {
                tipsList.Add(tip.description);
            }
        }
        return tipsList;
    }

    public List<string> getSceneLoadingTitles()
    {
        List<string> titlesList = new List<string>();
        foreach (LoadingImageInfos tip in currentLoadingImages)
        {
            if (tip.title.Trim() != "")
            {
                titlesList.Add(tip.title);
            }
        }
        return titlesList;
    }

    //void GetfirstTip() {
    //    currentTipIndex = 0;
    //    LoadingTip.text = currentLoadingTips[currentTipIndex];
    //    LoadingTitle.text = currentLoadingTitles[currentTipIndex];
    //}

    //void GetLastTip() {
    //    currentTipIndex = currentLoadingTips.Count - 1;
    //    LoadingTip.text = currentLoadingTips[currentTipIndex];
    //    LoadingTitle.text = currentLoadingTitles[currentTipIndex];
    //}

    /// <summary>
    /// get the next tip in the list
    /// </summary>
    //public void NextTip()
    //{
    //    if (currentTipIndex + 1 < currentLoadingTips.Count)
    //    {
    //        currentTipIndex++;
    //        LoadingTip.text = currentLoadingTips[currentTipIndex];
    //        LoadingTitle.text = currentLoadingTitles[currentTipIndex];
    //    }
    //    else
    //    {
    //        GetfirstTip();
    //    }
    //}

    /// <summary>
    /// get the Previous tip in the list
    /// </summary>
    //public void PreviousTip()
    //{
    //    if (currentTipIndex - 1 >= 0)
    //    {
    //        currentTipIndex--;
    //        LoadingTip.text = currentLoadingTips[currentTipIndex];
    //        LoadingTitle.text = currentLoadingTitles[currentTipIndex];
    //    }
    //    else
    //    {
    //        GetLastTip();
    //    }
    //}

    /// <summary>
    /// enumerator to loop through the list of tips
    /// </summary>
    /// <returns></returns>
    //IEnumerator LoopTips()
    //{
    //    while (true)
    //    {
    //        LoadingTip.text = currentLoadingTips[currentTipIndex];
    //        if (currentTipIndex + 1 < currentLoadingTips.Count)
    //        {
    //            currentTipIndex++;
    //        }
    //        else
    //        {
    //            currentTipIndex = 0;
    //        }
    //        yield return new WaitForSeconds(3);
    //    }
    //}

}
