using MSCLoader;
using UnityEngine;

namespace CustomizableNeeds
{
    public class CustomizableNeedsGUI : MonoBehaviour
    {
        public bool guiDisplaying = false;


        float sliderValue = 0.0f;

        public void OnGUI()
    {
        if (guiDisplaying == false)
            return;

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

    void DrawBox() {
        Coords boxDimensions = new Coords(850f, 210f); // keep box dimensions within (1280, 720) pixels
        Coords boxPosition = new Coords(Screen.width / 2 - boxDimensions.x / 2, 20);
        Rect boxRect = new Rect(boxPosition.x, boxPosition.y, boxDimensions.x, boxDimensions.y);

        GUI.Box(boxRect, "Customizable Needs");
    }

    void DrawLabel(float posX, float posY, string labelText, int fontSize = 12) {
        GUIStyle labelStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
        labelStyle.fontSize = fontSize;

        Coords labelDimensions = new Coords(250f, 25f);
        Coords labelPosition = new Coords(posX, posY);
        Rect labelRect = new Rect(labelPosition.x, labelPosition.y, labelDimensions.x, labelDimensions.y); ;

        GUI.Label(labelRect, labelText, labelStyle);
    }


    //hardcode positions only way!
    // keep all positions within (1280, 720) pixels
    void DrawIncreaseRates() {

        float centerX = Screen.width / 2;

        //BACKGROUND BOX
        DrawBox();

        //SECTION LABEL
        DrawLabel(centerX - 50, 50, "Increase Rates", 15);

        //COLUMN LABELS
        DrawLabel(centerX - 325, 75, "Need", 13);
        DrawLabel(centerX - 50, 75, "Rate", 13);
        DrawLabel(centerX + 150, 75, "Estimated time from empty to full", 13);

        //ROW 1
        DrawLabel(centerX - 325, 100, "Thirst", 12);
        DrawLabel(centerX - 50, 100, "Rate", 12);
        DrawLabel(centerX + 150, 100, "Estimated time from empty to full", 12);

    }


    void DrawDecreaseRates() {
        float areaWidth = 850f;
        float areaHeight = 210f;

        float area1PositionX = Screen.width / 2 - areaWidth / 2;
        float area1PositionY = 300;

        


        // slider style
        GUIStyle sliderStyle = new GUIStyle(GUI.skin.GetStyle("HorizontalSlider"));
        sliderStyle.margin = new RectOffset(0, 0, 13, 0);

        //label style
        GUIStyle labelStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
        labelStyle.margin = new RectOffset(0, 0, 12, 0);
        //labelStyle.padding = new RectOffset(50, 0, 0, 0);

        // box over entire area
        GUI.Box(new Rect(area1PositionX, area1PositionY - 20, areaWidth, areaHeight), "Customizable Needs");

        GUILayout.BeginArea(new Rect(area1PositionX, area1PositionY, areaWidth, areaHeight));

        labelStyle.fontSize = 15;
        GUILayout.Label("Decrease Rates:", labelStyle);

        labelStyle.fontSize = 13;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Need", labelStyle);
        GUILayout.Label("Rate", labelStyle);
        GUILayout.Label("Value", labelStyle);
        GUILayout.EndHorizontal();


        labelStyle.fontSize = 12;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Need 1", labelStyle);
        decreaseValue = GUILayout.HorizontalSlider(decreaseValue, 0.0F, 1.0F, sliderStyle, GUI.skin.GetStyle("horizontalSliderThumb"), GUILayout.MaxWidth(240f));
        //labelStyle.padding = new RectOffset(83, 0, 0, 0);
        GUILayout.Label(decreaseValue.ToString(), labelStyle);
        //labelStyle.padding = new RectOffset(50, 0, 0, 0);
        GUILayout.EndHorizontal();



        GUILayout.EndArea();
    }


        //calculates the time needed to fill attribute bar starting from 0 to 100, in real and game time
        void CalculateTimeNeeded() {
            float increaseRate = 1.20f; // per 45 seconds. whatever attribute we want to calculate such as hunger, stress, etc. look in wiki for values
            float modifyRate = 0.08f; // must be between 0.0f and 1.0f. this is our modified rate. set to gui slider value. 
            float incrementsNeededToFill = 100 / (increaseRate * modifyRate); // if 100 is full, this gets the number of increments by the rate needed to reach full starting from 0
            float realSeconds = incrementsNeededToFill * 45; // game increments attribute by the increment value every 45 seconds
            float gameSeconds = realSeconds * 13.33f; // 45 real seconds equal 10 minutes in game. 10 min = 600 seconds. 600/45 = 13.33. 1 real second = 13.33 game seconds

            ModConsole.Print("time from empty to full - Real: " + "D: " + (int)(realSeconds / 86400) + " H: " + (int)((realSeconds / 3600) % 24) + " M: " + (int)((realSeconds / 60) % 60) + " S: " + (int)(realSeconds % 60));
            ModConsole.Print("time from empty to full - Game: " + "D: " + (int)(gameSeconds / 86400) + " H: " + (int)((gameSeconds / 3600) % 24) + " M: " + (int)((gameSeconds / 60) % 60) + " S: " + (int)(gameSeconds % 60));
        }
    }
}