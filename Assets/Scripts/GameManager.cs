using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int startingCargoCount = 3;
    public string victoryMessage = "Icebergs. Whales. Starvation.\nYou have conquered them all.";
    public string starvationMessage = "You starved";
	public AudioSource winGameSound;
	public AudioSource loseGameSound;

    public BoatCargo boatCargo;
    public EndGameUI endGameUI;

    [HideInInspector]
    public int PlayerCargoCount { get { return boatCargo.CargoCount; } }

    public float cargoPointsMultiplier = 1;

    void Start()
    {
        boatCargo.StarvedEvent += EndGameWithLoss;
    }

    public string PointsString ()
    {
        var points = PlayerCargoCount * cargoPointsMultiplier;	
        return points.ToString ();
    }

    public void EndGameWithWin ()
    {
		GameObject.Find ("BackgroundMusic").GetComponent<AudioSource>().Stop();
		GameObject.Find ("StarvingMusic").GetComponent<AudioSource>().Stop();
		winGameSound.Play ();
		Time.timeScale = 0;
		endGameUI.FadeIn (victoryMessage, true);
    }

    void EndGameWithLoss ()
    {
		GameObject.Find ("BackgroundMusic").GetComponent<AudioSource>().Stop();
		GameObject.Find ("StarvingMusic").GetComponent<AudioSource>().Stop();
		loseGameSound.Play ();
		Time.timeScale = 0;
		endGameUI.FadeIn (starvationMessage, false);
    }
}
