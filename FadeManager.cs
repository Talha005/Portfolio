using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    LoadingScreenUI loadingScreenUI;

    private void Awake()
    {
        loadingScreenUI = GetComponent<LoadingScreenUI>();
    }

    // will fade from black to the game scene
    public void FadeOutOnSceneStart()
    {
        Fade(loadingScreenUI.LoadingImage, 0, 0.01f, false);
        Fade(fadeImage, 0, loadingScreenUI.FadeOutOnSceneStartDuration, false);
    }

    // will fade from the game scene to black 
    public void FadeInOnSceneEnd()
    {
        Fade(loadingScreenUI.LoadingImage, 0, 0.01f, false);
        Fade(fadeImage, 1, loadingScreenUI.FadeOutOnSceneStartDuration, false);
    }

    // generic function that fades the alpha of an image to 1 within the given duration
    public void FadeIn(Image image, float duration)
    {
        Fade(image, 1, duration * 0.5f, false);
    }


    // generic function that fades the alpha of an image to 0 within the given duration
    public void FadeOut(Image image, float duration)
    {
        Fade(image, 0, duration * 0.5f, false);
    }

    /// <summary>
    /// This method changes the image alpha to the passed value with the provided duration
    /// </summary>
    public void Fade(Image image, float alpha, float duration, bool ignoreTimeScale) {
        image.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
    }
}
