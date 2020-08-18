using MSCLoader;
using UnityEngine;
using HutongGames.PlayMaker;

namespace CustomizableNeeds
{
    public class CustomizableNeeds : Mod
    {
        public override string ID => "CustomizableNeeds"; //Your mod ID (unique)
        public override string Name => "CustomizableNeeds"; //You mod name
        public override string Author => "Your Username"; //Your Username
        public override string Version => "1.0"; //Version


        //Need Variables
        float thirst = 0;
        float hunger = 0;
        float stress = 0;
        float urine = 0;
        float fatigue = 5;
        float dirtiness = 0;

        GameObject player;


        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => false;

        public override void OnNewGame()
        {
            // Called once, when starting a New Game, you can reset your saves here
        }

        public override void OnLoad()
        {
            // Called once, when mod is loading after game is fully loaded
            ConsoleCommand.Add(new CommandGetNeeds());

            
            
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
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
        }

        public override void Update()
        {
            // Update is called once per frame
            SetPlayMakerValues();
        }


        public void SetPlayMakerValues() {
            FsmVariables.GlobalVariables.FindFsmFloat("PlayerThirst").Value = thirst;
            FsmVariables.GlobalVariables.FindFsmFloat("PlayerHunger").Value = hunger;
            FsmVariables.GlobalVariables.FindFsmFloat("PlayerStress").Value = stress;
            FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine").Value = urine;
            FsmVariables.GlobalVariables.FindFsmFloat("PlayerFatigue").Value = fatigue;
            FsmVariables.GlobalVariables.FindFsmFloat("PlayerDirtiness").Value = dirtiness;
        }


        public bool CheckPYMKDirtinessDecreased() {


            return false;
        }
    }
}
