using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIUtilities
{
    // Fade in the canvas
    public static void FadeIn(CanvasGroup canvasGroup, float duration, float from = 0, float delay = 0)
    {
        //LeanTween.cancelAll();
        LeanTween.cancel(canvasGroup.gameObject, false);

        canvasGroup.alpha = from; // Start with a transparent canvas

        // Use DOTween to tween the alpha property over the specified duration and delay
        canvasGroup.LeanAlpha(1, duration).setDelay(delay);

    }

    // Fade out the canvas
    public static void FadeOut(CanvasGroup canvasGroup, float duration, float delay = 0)
    {
        canvasGroup.alpha = 1; // Start with a fully opaque canvas

        // Use DOTween to tween the alpha property over the specified duration and delay
        canvasGroup.LeanAlpha(0, duration).setDelay(delay);
    }

    public static void ScaleAnimation(GameObject targetObject, Vector3 scaleFrom, Vector3 scaleTo, float duration, float delay = 0, LeanTweenType easeType = LeanTweenType.easeOutBack)
    {
        // Scale from the current scale to the specified scale amount
        LeanTween.scale(targetObject, scaleFrom, duration)
            .setDelay(delay)
            .setEase(easeType).setOnComplete(() =>
            {

                LeanTween.scale(targetObject,scaleTo, duration).setEase(easeType);

            });
    }

}
