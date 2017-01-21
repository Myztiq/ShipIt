using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerManager : MonoBehaviour
{
    public float hungerTickSeconds = 15f;
    public Slider hungerSlider;
    public Image hungerSliderFillImage;
    public Text countText;
    public BoatCargo boatCargo;
    public GameManager gameManager;

    public Color normalSliderColor;
    public Color warningSliderColor;
    public Color dangerSliderColor;

    int tick = 0;

    void Start ()
    {
        UpdateCountUI (gameManager.startingCargoCount);

        boatCargo.CargoAddedEvent += UpdateCountUI;
        boatCargo.CargoRemovedEvent += UpdateCountUI;
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateSliderProgress ();
        MaybeTickHunger ();
        UpdateSliderBackgroundColor (boatCargo.CargoCount);
    }

    void UpdateSliderProgress ()
    {
        var val = 1f - (Time.time % hungerTickSeconds) / hungerTickSeconds;
        hungerSlider.value = val;
    }

    void MaybeTickHunger ()
    {
        int currentTick = (int)(Time.time / hungerTickSeconds);
        if (currentTick > tick) {
            Debug.Log ("Time to eat");
            tick = currentTick;
            boatCargo.ConsumeCargo ();
        }
    }

    void UpdateSliderBackgroundColor (int cargoCount)
    {
        Color color;
        switch (cargoCount) {
        case 1:
            color = warningSliderColor;
            break;
        case 0:
            color = dangerSliderColor;
            break;
        default:
            color = normalSliderColor;
            break;
        }

        hungerSliderFillImage.color = color;
    }

    void UpdateCountUI (int count)
    {
        countText.text = "x" + count;
    }
}
