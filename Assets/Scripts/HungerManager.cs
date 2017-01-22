using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerManager : MonoBehaviour
{
    public float hungerTickSeconds = 15f;
    public int consumeUIDurationSeconds = 3;
    public float barPerCargo = 0.25f;
    public Slider hungerSlider;
    public Image hungerSliderFillImage;
    public Text countText;
    public GameObject consumeUI;
    public BoatCargo boatCargo;
    public GameManager gameManager;

    public Color normalSliderColor;
    public Color warningSliderColor;
    public Color dangerSliderColor;

    int tick = 0;
    float sliderValue;

    void Start ()
    {
        var cargoCount = gameManager.startingCargoCount;

        UpdateCountUI (cargoCount);

        UpdateSliderValue(barPerCargo); // 1 unit for when you have no cargo

        boatCargo.CargoAddedEvent += AddCargoToBar;
        boatCargo.CargoAddedEvent += UpdateCountUI;
        boatCargo.CargoRemovedEvent += UpdateCountUI;
        boatCargo.CargoEjectedEvent += RemoveCargoFromBar;
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
        int cargoForFullBar = 4;
        float barPerCargo = 1f / cargoForFullBar;
        float barPerSecond = barPerCargo / hungerTickSeconds;

        var decrement = Time.deltaTime * barPerSecond;
        UpdateSliderValue (sliderValue - decrement);
//        Debug.Log ("Slider: " + sliderValue);
    }

    void MaybeTickHunger ()
    {
        int currentTick = (int)(Time.time / hungerTickSeconds);
        if (currentTick > tick) {       
            tick = currentTick;
            boatCargo.ConsumeCargo ();
		}
    }

    void UpdateSliderBackgroundColor (int cargoCount)
    {
        Color color = normalSliderColor;
        if (cargoCount == 1) {
            color = warningSliderColor;
        } else if (cargoCount < 1) {
            color = dangerSliderColor;
        }

        hungerSliderFillImage.color = color;
    }

    void UpdateSliderValue (float val)
    {
        sliderValue = val;
        hungerSlider.value = val;
    }

    void AddCargoToBar (int cargoCount)
    {
        UpdateSliderValue (sliderValue + barPerCargo);
    }

    void RemoveCargoFromBar (int cargoCount)
    {
        UpdateSliderValue (sliderValue - barPerCargo);
    }

    void UpdateCountUI (int count)
    {
        countText.text = "x" + count;
    }

    IEnumerator HideConsumeUIWithDelay ()
    {
        yield return new WaitForSeconds (consumeUIDurationSeconds);
        consumeUI.SetActive (false);
    }
}
