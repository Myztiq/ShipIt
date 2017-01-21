using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int startingCargoCount = 3;
    public BoatCargo boatCargo;
    public GameObject endGameUI;

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

    void EndGameWithLoss ()
    {
        endGameUI.SetActive (true);
        Time.timeScale = 0;
    }
}
