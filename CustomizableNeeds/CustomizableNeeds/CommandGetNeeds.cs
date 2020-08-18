using MSCLoader;
using HutongGames.PlayMaker;
using System;

namespace CustomizableNeeds
{
    public class CommandGetNeeds : ConsoleCommand
    {
        // What the player has to type into the console to execute your commnad
        public override string Name => "CommandGetNeeds";

        // The help that's displayed for your command when typing help
        public override string Help => "Command Description";

        // The function that's called when executing command
        public override void Run(string[] args)
        {
            foreach (string arg in args) {
                ModConsole.Print(arg);
            }

            //Do something when command is executed
            ModConsole.Print("Player Urine: " + FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine").Value); // 0 - empty, 100 - full
        }

    }
}
