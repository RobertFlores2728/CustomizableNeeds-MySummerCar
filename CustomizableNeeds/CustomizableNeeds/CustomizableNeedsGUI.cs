﻿using MSCLoader;
using UnityEngine;

namespace CustomizableNeeds
{
    public class CustomizableNeedsGUI : MonoBehaviour
    {
        public bool guiDisplaying = false;


        float sliderValue = 0.0f;

        public void OnGUI() {
            if (guiDisplaying == false)
                return;

            float areaWidth = 500f;
            float areaHeight = 2000f;

            // slider style
            GUIStyle sliderStyle = GUI.skin.GetStyle("HorizontalSlider");
            sliderStyle.margin = new RectOffset(0, 0, 10, 0);

            //label style
            GUIStyle labelStyle = GUI.skin.GetStyle("label");
            labelStyle.margin = new RectOffset(0, 0, 3, 0);
            labelStyle.fontSize = 12;

            // box over entire area
            GUI.Box(new Rect(Screen.width / 2 - areaWidth / 2, 0, areaWidth, areaHeight), "Customizable Needs");

            GUILayout.BeginArea(new Rect(Screen.width / 2 - areaWidth / 2, 20, areaWidth, areaHeight));


            GUILayout.BeginHorizontal();


            GUILayout.BeginVertical();
            GUILayout.Label("Real - D: 0 H: 0 M: 0 | Game - D: 0 H: 0 M: 0");
            GUILayout.Label("Real - D: 0 H: 0 M: 0 | Game - D: 0 H: 0 M: 0");
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            sliderValue = GUILayout.HorizontalSlider(sliderValue, 0.0F, 1.0F);
            sliderValue = GUILayout.HorizontalSlider(sliderValue, 0.0F, 1.0F);
            sliderValue = GUILayout.HorizontalSlider(sliderValue, 0.0F, 1.0F);
            sliderValue = GUILayout.HorizontalSlider(sliderValue, 0.0F, 1.0F);
            sliderValue = GUILayout.HorizontalSlider(sliderValue, 0.0F, 1.0F);
            sliderValue = GUILayout.HorizontalSlider(sliderValue, 0.0F, 1.0F);
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Label("Real - D: 0 H: 0 M: 0 | Game - D: 0 H: 0 M: 0");
            GUILayout.Label("Real - D: 0 H: 0 M: 0 | Game - D: 0 H: 0 M: 0");
            GUILayout.EndVertical();


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