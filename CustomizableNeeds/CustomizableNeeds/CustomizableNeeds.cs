using MSCLoader;
using UnityEngine.UI;
using HutongGames.PlayMaker;
using UnityEngine;

namespace CustomizableNeeds
{
    public class CustomizableNeeds : Mod
    {
        public override string ID => "CustomizableNeeds"; //Your mod ID (unique)
        public override string Name => "CustomizableNeeds"; //You mod name
        public override string Author => "Your Username"; //Your Username
        public override string Version => "1.0"; //Version


        //last update PlayMaker need float values
        float previousThirst;
        float previousHunger;
        float previousStress;
        float previousUrine;
        float previousFatigue;
        float previousDirtiness;


        //gui
        CustomizableNeedsGUI gui;


        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => false;

        public override void OnNewGame()
        {
            // Called once, when starting a New Game, you can reset your saves here
        }

        public override void OnLoad()
        {

            gui = new CustomizableNeedsGUI();
            gui.LoadRates();


        }

        public override void ModSettings()
        {
            // All settings should be created here. 
            // DO NOT put anything else here that settings.
        }

        public override void OnSave()
        {
            // Called once, when save and quit
            // Serialize your save file here.
            gui.SaveRates();
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
            gui.OnGUI();
        }

        public override void Update()
        {
            // Update is called once per frame

            CheckIfPlayMakerValuesIncreased();
            CheckIfPlayMakerValuesDecreased();

            if (Input.GetKey(KeyCode.LeftShift)) {
                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    if (gui.IsDisplaying()) {
                        gui.SaveRates();
                    }

                    gui.ToggleGUI();
                }
            }



            GetNeedValuesEndUpdate();
        }


        /*Game increases needs every 45 seconds. if previous needs are less than current needs, needs have increased by game.
         * subtract by percentage of original value desired to get the effect of a smaller increase
         */
        public void CheckIfPlayMakerValuesIncreased() {

            CheckIfPlayMakerValueIncreased("PlayerThirst", previousThirst, gui.thirstIncreaseRate);
            CheckIfPlayMakerValueIncreased("PlayerHunger", previousHunger, gui.hungerIncreaseRate);
            CheckIfPlayMakerValueIncreased("PlayerStress", previousStress, gui.stressIncreaseRate);
            CheckIfPlayMakerValueIncreased("PlayerUrine", previousUrine, gui.urineIncreaseRate);
            CheckIfPlayMakerValueIncreased("PlayerFatigue", previousFatigue, gui.fatigueIncreaseRate);
            CheckIfPlayMakerValueIncreased("PlayerDirtiness", previousDirtiness, gui.dirtinessIncreaseRate);
        }

        void CheckIfPlayMakerValueIncreased(string playMakerValue, float previousValue, float increaseRate) {
            if (previousValue < FsmVariables.GlobalVariables.FindFsmFloat(playMakerValue).Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat(playMakerValue).Value - previousValue;

                FsmVariables.GlobalVariables.FindFsmFloat(playMakerValue).Value -= difference * (1.0f - increaseRate);
            }
        }


        void CheckIfPlayMakerValuesDecreased()
        {
            CheckIfPlayMakerValueDecreased("PlayerThirst", previousThirst, gui.thirstDecreaseRate);
            CheckIfPlayMakerValueDecreased("PlayerHunger", previousHunger, gui.hungerDecreaseRate);
            CheckIfPlayMakerValueDecreased("PlayerStress", previousStress, gui.stressDecreaseRate);
            CheckIfPlayMakerValueDecreased("PlayerUrine", previousUrine, gui.urineDecreaseRate);
            CheckIfPlayMakerValueDecreased("PlayerFatigue", previousFatigue, gui.fatigueDecreaseRate);
            CheckIfPlayMakerValueDecreased("PlayerDirtiness", previousDirtiness, gui.dirtinessDecreaseRate);
        }

        void CheckIfPlayMakerValueDecreased(string playMakerValue, float previousValue, float decreaseRate)
        {
            if (previousValue > FsmVariables.GlobalVariables.FindFsmFloat(playMakerValue).Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat(playMakerValue).Value - previousValue;

                FsmVariables.GlobalVariables.FindFsmFloat(playMakerValue).Value -= difference * (1.0f - decreaseRate);
            }
        }


        void GetNeedValuesEndUpdate() {
            previousThirst = FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst").Value;
            previousHunger = FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger").Value;
            previousStress = FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress").Value;
            previousUrine = FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine").Value;
            previousFatigue = FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue").Value;
            previousDirtiness = FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness").Value;
        }
    }
}
