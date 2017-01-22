using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public bool pauseDuringTutorial;
    public int tutorialDurationSeconds = 5;
    public Image tutorialPopover;
    public Boat boat;
    public BoatCargo boatCargo;

    private HashSet<string> shownTutorials = new HashSet<string>();

    void Start ()
    {
        tutorialPopover.gameObject.SetActive (false);

        boat.BoatCollidedEvent += MaybeShowBoatCollided;
        boatCargo.CargoConsumedEvent += MaybeShowBoatCargoConsumed;
        boatCargo.OutOfCargoEvent += MaybeShowOutOfCargo;
    }
	
    void MaybeShowBoatCollided ()
    {
        MaybeShowTutorial ("Tutorial_Hit");
    }

    void MaybeShowBoatCargoConsumed ()
    {
        MaybeShowTutorial ("Tutorial_Hungry");
    }

    void MaybeShowOutOfCargo ()
    {
        MaybeShowTutorial ("Tutorial_Starving");
    }

    void MaybeShowTutorial (string spriteName)
    {
        if (shownTutorials.Contains (spriteName)) {
            return;
        }

        shownTutorials.Add (spriteName);

        tutorialPopover.overrideSprite = Resources.Load<Sprite> (spriteName);
        tutorialPopover.gameObject.SetActive (true);

        StartCoroutine (HideTutorialPopover());
    }

    IEnumerator HideTutorialPopover ()
    {
        if (pauseDuringTutorial) {
            Time.timeScale = 0;
        }

        yield return new WaitForSecondsRealtime (tutorialDurationSeconds);

        if (pauseDuringTutorial) {
            Time.timeScale = 1;
        }

        tutorialPopover.gameObject.SetActive (false);
    }
}
