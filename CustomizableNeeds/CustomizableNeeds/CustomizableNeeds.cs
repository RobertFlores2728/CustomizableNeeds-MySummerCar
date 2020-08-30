﻿using MSCLoader;
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


        //last update need values
        float previousThirst;
        float previousHunger;
        float previousStress;
        float previousUrine;
        float previousFatigue;
        float previousDirtiness;


        //1.0f = subtract all that was incremented last frame, meaning values never increase
        //these are inverted meaning that a factor 0f 1.0 will be displayed as 0.0 in our gui
        //because a factor of 1.0 would mean that the attribute be decreased by all that it increased last frame
        float thirstIncreaseRate = 0.5f;
        float hungerIncreaseRate = 0.5f;
        float stressIncreaseRate = 0.5f;
        float urineIncreaseRate = 0.5f;
        float fatigueIncreaseRate = 0.5f;
        float dirtinessIncreaseRate = 0.5f;


        //1.0f = add all that was decremented last frame, meaning values never decrease
        //these are inverted meaning that a factor 0f 1.0 will be displayed as 0.0 in our gui
        //because a factor of 1.0 would mean that the attribute be increased by all that it decreased last frame
        float thirstDecreaseRate = 0.5f;
        float hungerDecreaseRate = 0.5f;
        float stressDecreaseRate = 0.5f;
        float urineDecreaseRate = 0.5f;
        float fatigueDecreaseRate = 0.5f;
        float dirtinessDecreaseRate = 0.5f;





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
                    if (gui.guiDisplaying) {
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
            if (previousThirst < FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst").Value - previousThirst;

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst").Value -= difference * (1.0f - gui.thirstIncreaseRate);
            }

            if (previousHunger < FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger").Value - previousHunger;

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger").Value -= difference * (1.0f - gui.hungerIncreaseRate);
            }

            if (previousStress < FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress").Value - previousStress;

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress").Value -= difference * (1.0f - gui.stressIncreaseRate);
            }

            if (previousUrine < FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine").Value - previousUrine;

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine").Value -= difference * (1.0f - gui.urineIncreaseRate);
            }

            if (previousFatigue < FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue").Value - previousFatigue;

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue").Value -= difference * (1.0f - gui.fatigueIncreaseRate);
            }

            if (previousDirtiness < FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness").Value - previousDirtiness;

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness").Value -= difference * (1.0f - gui.dirtinessIncreaseRate);
            }
        }


        public void CheckIfPlayMakerValuesDecreased()
        {
            if (previousThirst > FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst").Value - previousThirst; // this will be negative, which is why we subtract in next line to mean adding

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst").Value -= difference * (1.0f - gui.thirstDecreaseRate);
            }

            if (previousHunger > FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger").Value - previousHunger;

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger").Value -= difference * (1.0f - gui.hungerDecreaseRate);
            }

            if (previousStress > FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress").Value - previousStress;

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress").Value -= difference * (1.0f - gui.stressDecreaseRate);
            }

            if (previousUrine > FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine").Value - previousUrine;

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine").Value -= difference * (1.0f - gui.urineDecreaseRate);
            }

            if (previousFatigue > FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue").Value - previousFatigue;

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue").Value -= difference * (1.0f - gui.fatigueDecreaseRate);
            }

            if (previousDirtiness > FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness").Value)
            {
                float difference = FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness").Value - previousDirtiness;

                FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness").Value -= difference * (1.0f - gui.dirtinessDecreaseRate);
            }
        }


        public void GetNeedValuesEndUpdate() {
            previousThirst = FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst").Value;
            previousHunger = FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger").Value;
            previousStress = FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress").Value;
            previousUrine = FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine").Value;
            previousFatigue = FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue").Value;
            previousDirtiness = FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness").Value;
        }
    }
}
