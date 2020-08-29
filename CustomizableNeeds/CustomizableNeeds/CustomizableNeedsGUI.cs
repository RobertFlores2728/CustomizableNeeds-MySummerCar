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

		void DrawBox() {
			Coords boxDimensions = new Coords(850f, 500f); // keep box dimensions within (1280, 720) pixels
			Coords boxPosition = new Coords(Screen.width / 2 - boxDimensions.x / 2, 20);
			Rect boxRect = new Rect(boxPosition.x, boxPosition.y, boxDimensions.x, boxDimensions.y);

			GUI.Box(boxRect, "CustomizableNeeds v1.0 by Heb27");
		}

		void DrawLabel(float posX, float posY, string labelText, int fontSize = 12) {
			GUIStyle labelStyle = new GUIStyle(GUI.skin.GetStyle("Label"));
			labelStyle.fontSize = fontSize;

			Coords labelDimensions = new Coords(375f, 25f);
			Coords labelPosition = new Coords(posX, posY);
			Rect labelRect = new Rect(labelPosition.x, labelPosition.y, labelDimensions.x, labelDimensions.y); ;

			GUI.Label(labelRect, labelText, labelStyle);
		}

		void DrawSlider(float posX, float posY, ref float value)
		{
			Coords sliderDimensions = new Coords(250f, 25f);
			Coords sliderPosition = new Coords(posX, posY + 4);
			Rect labelRect = new Rect(sliderPosition.x, sliderPosition.y, sliderDimensions.x, sliderDimensions.y); ;

			value = GUI.HorizontalSlider(labelRect, value, 0.0f, 1.0f);
		}


		//hardcode positions only way!
		// keep all positions within (1280, 720) pixels
		void DrawIncreaseRates() {

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

			//THIRST
			y += 20;
			DrawLabel(centerX - 375, y, "Thirst", 12);
			DrawSlider(centerX - 250, y, ref increaseValue1);
			DrawLabel(centerX + 50, y, "REAL= D: 00 H: 00 M: 00 S: 00    GAME= D: 00 H: 00 M: 00 S: 00", 12);

			//HUNGER
			y += 20;
			DrawLabel(centerX - 375, y, "Hunger", 12);
			DrawSlider(centerX - 250, y, ref increaseValue2);
			DrawLabel(centerX + 50, y, "REAL= D: 00 H: 00 M: 00 S: 00    GAME= D: 00 H: 00 M: 00 S: 00", 12);

			//THIRST
			y += 20;
			DrawLabel(centerX - 375, y, "Thirst", 12);
			DrawSlider(centerX - 250, y, ref increaseValue1);
			DrawLabel(centerX + 50, y, "REAL= D: 00 H: 00 M: 00 S: 00    GAME= D: 00 H: 00 M: 00 S: 00", 12);

			//HUNGER
			y += 20;
			DrawLabel(centerX - 375, y, "Hunger", 12);
			DrawSlider(centerX - 250, y, ref increaseValue2);
			DrawLabel(centerX + 50, y, "REAL= D: 00 H: 00 M: 00 S: 00    GAME= D: 00 H: 00 M: 00 S: 00", 12);

			//THIRST
			y += 20;
			DrawLabel(centerX - 375, y, "Thirst", 12);
			DrawSlider(centerX - 250, y, ref increaseValue1);
			DrawLabel(centerX + 50, y, "REAL= D: 00 H: 00 M: 00 S: 00    GAME= D: 00 H: 00 M: 00 S: 00", 12);

			//HUNGER
			y += 20;
			DrawLabel(centerX - 375, y, "Hunger", 12);
			DrawSlider(centerX - 250, y, ref increaseValue2);
			DrawLabel(centerX + 50, y, "REAL= D: 00 H: 00 M: 00 S: 00    GAME= D: 00 H: 00 M: 00 S: 00", 12);

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
			DrawSlider(centerX - 250, y, ref increaseValue1);
			DrawLabel(centerX + 125, y, "1.0", 12);
			
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