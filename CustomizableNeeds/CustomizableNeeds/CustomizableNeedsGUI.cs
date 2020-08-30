using MSCLoader;
using UnityEngine;

namespace CustomizableNeeds
{
    public bool guiDisplaying = false;


    public float thirstIncreaseRate;
    public float hungerIncreaseRate;
    public float stressIncreaseRate;
    public float urineIncreaseRate;
    public float fatigueIncreaseRate;
    public float dirtinessIncreaseRate;


    public float thirstDecreaseRate;
    public float hungerDecreaseRate;
    public float stressDecreaseRate;
    public float urineDecreaseRate;
    public float fatigueDecreaseRate;
    public float dirtinessDecreaseRate;

    private void Start()
    {
        LoadRates();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {

                if (guiDisplaying)
                {
                    SaveRates();
                }


                guiDisplaying = !guiDisplaying;
            }

    }

    void LoadRate(ref float rate, string rateName)
    {
        if (!PlayerPrefs.HasKey(rateName))
        {
            PlayerPrefs.SetFloat(rateName, 1.0f);
        }

        rate = PlayerPrefs.GetFloat(rateName);
    }

    void LoadRates()
    {
        //load increase rates
        LoadRate(ref thirstIncreaseRate, "CUSTOMIZABLENEEDS_thirstIR");
        LoadRate(ref hungerIncreaseRate, "CUSTOMIZABLENEEDS_hungerIR");
        LoadRate(ref stressIncreaseRate, "CUSTOMIZABLENEEDS_stressIR");
        LoadRate(ref urineIncreaseRate, "CUSTOMIZABLENEEDS_urineIR");
        LoadRate(ref fatigueIncreaseRate, "CUSTOMIZABLENEEDS_fatigueIR");
        LoadRate(ref dirtinessIncreaseRate, "CUSTOMIZABLENEEDS_dirtinessIR");

        //load decrease rates
        LoadRate(ref thirstDecreaseRate, "CUSTOMIZABLENEEDS_thirstDR");
        LoadRate(ref hungerDecreaseRate, "CUSTOMIZABLENEEDS_hungerDR");
        LoadRate(ref stressDecreaseRate, "CUSTOMIZABLENEEDS_stressDR");
        LoadRate(ref urineDecreaseRate, "CUSTOMIZABLENEEDS_urineDR");
        LoadRate(ref fatigueDecreaseRate, "CUSTOMIZABLENEEDS_fatigueDR");
        LoadRate(ref dirtinessDecreaseRate, "CUSTOMIZABLENEEDS_dirtinessDR");
    }

    void SaveRate(ref float rate, string rateName)
    {
        if (PlayerPrefs.GetFloat(rateName) != rate)
            PlayerPrefs.SetFloat(rateName, rate);
    }

    void SaveRates()
    {
        //save increase rates
        SaveRate(ref thirstIncreaseRate, "CUSTOMIZABLENEEDS_thirstIR");
        SaveRate(ref hungerIncreaseRate, "CUSTOMIZABLENEEDS_hungerIR");
        SaveRate(ref stressIncreaseRate, "CUSTOMIZABLENEEDS_stressIR");
        SaveRate(ref urineIncreaseRate, "CUSTOMIZABLENEEDS_urineIR");
        SaveRate(ref fatigueIncreaseRate, "CUSTOMIZABLENEEDS_fatigueIR");
        SaveRate(ref dirtinessIncreaseRate, "CUSTOMIZABLENEEDS_dirtinessIR");

        //Save decrease rates
        SaveRate(ref thirstDecreaseRate, "CUSTOMIZABLENEEDS_thirstDR");
        SaveRate(ref hungerDecreaseRate, "CUSTOMIZABLENEEDS_hungerDR");
        SaveRate(ref stressDecreaseRate, "CUSTOMIZABLENEEDS_stressDR");
        SaveRate(ref urineDecreaseRate, "CUSTOMIZABLENEEDS_urineDR");
        SaveRate(ref fatigueDecreaseRate, "CUSTOMIZABLENEEDS_fatigueDR");
        SaveRate(ref dirtinessDecreaseRate, "CUSTOMIZABLENEEDS_dirtinessDR");
    }


    public void OnGUI()
    {
        if (guiDisplaying == false)
            return;

        //BACKGROUND BOX
        DrawBox();

        DrawIncreaseRates();

        DrawDecreaseRates();


    }

    public struct Coords
    {
        public Coords(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float x;
        public float y;
    }

    void DrawBox()
    {
        Coords boxDimensions = new Coords(850f, 500f); // keep box dimensions within (1280, 720) pixels
        Coords boxPosition = new Coords(Screen.width / 2 - boxDimensions.x / 2, 20);
        Rect boxRect = new Rect(boxPosition.x, boxPosition.y, boxDimensions.x, boxDimensions.y);

        GUI.Box(boxRect, "CustomizableNeeds v1.0 by Heb27");
    }

    void DrawLabel(float posX, float posY, string labelText, int fontSize = 12)
    {
        GUIStyle labelStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
        labelStyle.fontSize = fontSize;

        Coords labelDimensions = new Coords(375f, 25f);
        Coords labelPosition = new Coords(posX, posY);
        Rect labelRect = new Rect(labelPosition.x, labelPosition.y, labelDimensions.x, labelDimensions.y); ;

        GUI.Label(labelRect, labelText, labelStyle);
    }

    void DrawSlider(float posX, float posY, ref float value)
    {
        Coords sliderDimensions = new Coords(250f, 10f);
        Coords sliderPosition = new Coords(posX, posY + 4);
        Rect labelRect = new Rect(sliderPosition.x, sliderPosition.y, sliderDimensions.x, sliderDimensions.y); ;

        value = GUI.HorizontalSlider(labelRect, value, 0.0f, 1.0f);
        FloorRate(ref value);
    }


    //hardcode positions only way!
    // keep all positions within (1280, 720) pixels
    void DrawIncreaseRates()
    {

        float centerX = Screen.width / 2;

        //SECTION LABEL
        DrawLabel(centerX - 50, 50, "Increase Rates", 15);

        //COLUMN LABELS
        DrawLabel(centerX - 375, 75, "Need", 13);
        DrawLabel(centerX - 125, 75, "Rate", 13);
        DrawLabel(centerX + 125, 75, "Estimated time from empty to full", 13);



        //RATES
        //

        int y = 80;

        string calculatedTimeString;

        //THIRST
        y += 20;
        DrawLabel(centerX - 375, y, "Thirst", 12);
        DrawSlider(centerX - 250, y, ref thirstIncreaseRate);
        calculatedTimeString = CalculateTimeNeeded("Thirst", thirstIncreaseRate);
        DrawLabel(centerX + 50, y, calculatedTimeString, 12);

        //HUNGER
        y += 20;
        DrawLabel(centerX - 375, y, "Hunger", 12);
        DrawSlider(centerX - 250, y, ref hungerIncreaseRate);
        calculatedTimeString = CalculateTimeNeeded("Hunger", hungerIncreaseRate);
        DrawLabel(centerX + 50, y, calculatedTimeString, 12);

        //STRESS
        y += 20;
        DrawLabel(centerX - 375, y, "Stress", 12);
        DrawSlider(centerX - 250, y, ref stressIncreaseRate);
        calculatedTimeString = CalculateTimeNeeded("Stress", stressIncreaseRate);
        DrawLabel(centerX + 50, y, calculatedTimeString, 12);

        //URINE
        y += 20;
        DrawLabel(centerX - 375, y, "Urine", 12);
        DrawSlider(centerX - 250, y, ref urineIncreaseRate);
        calculatedTimeString = CalculateTimeNeeded("Urine", urineIncreaseRate);
        DrawLabel(centerX + 50, y, calculatedTimeString, 12);

        //FATIGUE
        y += 20;
        DrawLabel(centerX - 375, y, "Fatigue", 12);
        DrawSlider(centerX - 250, y, ref fatigueIncreaseRate);
        calculatedTimeString = CalculateTimeNeeded("Fatigue", fatigueIncreaseRate);
        DrawLabel(centerX + 50, y, calculatedTimeString, 12);

        //DIRTINESS
        y += 20;
        DrawLabel(centerX - 375, y, "Dirtiness", 12);
        DrawSlider(centerX - 250, y, ref dirtinessIncreaseRate);
        calculatedTimeString = CalculateTimeNeeded("Dirtiness", dirtinessIncreaseRate);
        DrawLabel(centerX + 50, y, calculatedTimeString, 12);

    }


    void DrawDecreaseRates()
    {

        float centerX = Screen.width / 2;

        //SECTION LABEL
        DrawLabel(centerX - 50, 275, "Decrease Rates", 15);



        //COLUMN LABELS
        DrawLabel(centerX - 375, 300, "Need", 13);
        DrawLabel(centerX - 125, 300, "Rate", 13);
        DrawLabel(centerX + 125, 300, "Value", 13);



        //RATES
        //

        int y = 305;

        //THIRST
        y += 20;
        DrawLabel(centerX - 375, y, "Thirst", 12);
        DrawSlider(centerX - 250, y, ref thirstDecreaseRate);
        DrawLabel(centerX + 125, y, Math.Round(thirstDecreaseRate, 3).ToString(), 12);

        //HUNGER
        y += 20;
        DrawLabel(centerX - 375, y, "Hunger", 12);
        DrawSlider(centerX - 250, y, ref hungerDecreaseRate);
        DrawLabel(centerX + 125, y, Math.Round(hungerDecreaseRate, 3).ToString(), 12);

        //STRESS
        y += 20;
        DrawLabel(centerX - 375, y, "Stress", 12);
        DrawSlider(centerX - 250, y, ref stressDecreaseRate);
        DrawLabel(centerX + 125, y, Math.Round(stressDecreaseRate, 3).ToString(), 12);

        //URINE
        y += 20;
        DrawLabel(centerX - 375, y, "Urine", 12);
        DrawSlider(centerX - 250, y, ref urineDecreaseRate);
        DrawLabel(centerX + 125, y, Math.Round(urineDecreaseRate, 3).ToString(), 12);

        //FATIGUE
        y += 20;
        DrawLabel(centerX - 375, y, "Fatigue", 12);
        DrawSlider(centerX - 250, y, ref fatigueDecreaseRate);
        DrawLabel(centerX + 125, y, Math.Round(fatigueDecreaseRate, 3).ToString(), 12);

        //DIRTINESS
        y += 20;
        DrawLabel(centerX - 375, y, "Dirtiness", 12);
        DrawSlider(centerX - 250, y, ref dirtinessDecreaseRate);
        DrawLabel(centerX + 125, y, Math.Round(dirtinessDecreaseRate, 3).ToString(), 12);

    }

    //calculates the time needed to fill attribute bar starting from 0 to 100, in real and game time
    string CalculateTimeNeeded(string type, float sliderValue)
    {
        if (sliderValue == 0.0f)
            return "Never";

        float increaseRate; // per 45 seconds. whatever attribute we want to calculate such as hunger, stress, etc. look in wiki for values

        switch (type)
        {
            case "Thirst":
                increaseRate = 1.83f;
                break;
            case "Hunger":
                increaseRate = 1.42f;
                break;
            case "Stress":
                increaseRate = 1.20f;
                break;
            case "Urine":
                increaseRate = 0.24f;
                break;
            case "Fatigue":
                increaseRate = 0.98f;
                break;
            case "Dirtiness":
                increaseRate = 0.27f;
                break;
            default:
                return "error!";
        }

        float modifyRate = sliderValue; // must be between 0.0f and 1.0f. this is our modified rate. set to gui slider value. 
        float incrementsNeededToFill = 100 / (increaseRate * modifyRate); // if 100 is full, this gets the number of increments by the rate needed to reach full starting from 0
        float realSeconds = incrementsNeededToFill * 45; // game increments attribute by the increment value every 45 seconds
        float gameSeconds = realSeconds * 13.33f; // 45 real seconds equal 10 minutes in game. 10 min = 600 seconds. 600/45 = 13.33. 1 real second = 13.33 game seconds


        string guiString = "REAL = D: " + (int)(realSeconds / 86400) + " H: " + (int)((realSeconds / 3600) % 24) + " M: " + (int)((realSeconds / 60) % 60) + " S: " + (int)(realSeconds % 60);
        guiString += "    GAME = D: " + (int)(gameSeconds / 86400) + " H: " + (int)((gameSeconds / 3600) % 24) + " M: " + (int)((gameSeconds / 60) % 60) + " S: " + (int)(gameSeconds % 60);

        return guiString;


        //ModConsole.Print("time from empty to full - Real: " + "D: " + (int)(realSeconds / 86400) + " H: " + (int)((realSeconds / 3600) % 24) + " M: " + (int)((realSeconds / 60) % 60) + " S: " + (int)(realSeconds % 60));
        //ModConsole.Print("time from empty to full - Game: " + "D: " + (int)(gameSeconds / 86400) + " H: " + (int)((gameSeconds / 3600) % 24) + " M: " + (int)((gameSeconds / 60) % 60) + " S: " + (int)(gameSeconds % 60));
    }

    void FloorRate(ref float sliderValue)
    {
        if (sliderValue < 0.001f)
            sliderValue = 0.0f;
    }


}
