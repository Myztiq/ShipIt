using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    public Animator cameraAnimator;
    public Animator victoryAnimator;
    public Animator failureAnimator;

    public Text messageText;
    public Image victoryImage;

    float shownTime;
    bool isShowing;

    public void FadeIn (string message, bool victory)
    {
        shownTime = Time.unscaledTime;

        messageText.text = message;
        gameObject.SetActive (true);

        cameraAnimator.SetTrigger("Show Frost");
        if (victory) {
            victoryAnimator.SetTrigger ("Show");
        } else {
            failureAnimator.SetTrigger ("Show");
        }

        isShowing = true;
    }

//    public void FadeOut()
//    {
//        uiAnimator.SetBool ("Show", false);
//        cameraAnimator.SetBool("Show Frost", false);
//    }

    void Update ()
    {
        MaybeRestartLevel ();
    }

    void MaybeRestartLevel ()
    {
        var timeSinceShown = Time.unscaledTime - shownTime;
        if (isShowing && timeSinceShown > 4f && Input.GetMouseButtonDown (0)) {
            Debug.Log ("About to restart level");
//            FadeOut ();
            Application.LoadLevel (Application.loadedLevel);
            Time.timeScale = 1;
        }
    }
}
