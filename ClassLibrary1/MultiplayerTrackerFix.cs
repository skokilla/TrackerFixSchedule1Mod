using MelonLoader;
using HarmonyLib;
using Il2CppScheduleOne.UI.Phone.Messages;
using Il2CppScheduleOne.Quests;
using UnityEngine;





namespace MultiplayerTrackerFix
{

    public static class trackerflip // bool to check if you are the one who clicked the button
    {
        public static bool tracker = false;
        public static float LastAcceptTime = 0;
    }

    [HarmonyPatch(typeof(WindowSelectorButton), ("Awake"))]
    public class Patch_Messages_WindowSelectorButton_Awake : MelonMod
    {
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("MultiplayerTrackerFix loaded!");
        }
        public static void Prefix(WindowSelectorButton __instance)
        {

            __instance.OnSelected.AddListener((UnityEngine.Events.UnityAction)(() =>  // when the button to select a time for a contract or "deal" is pressed do the code below
            {
                trackerflip.tracker = true; //flips tracker bool
               // debug line MelonLogger.Msg("Contract accepted"); // logs message
            }));

        }

    }



    [HarmonyPatch(typeof(QuestManager), ("CreateContract_Local"))]
    public class Patch_CreateContract_Local : MelonMod
    {
       

        public static void Prefix(ref bool tracked)
        {
           
            
                tracked = trackerflip.tracker; // sets the tracked state depending on if you are the one who clicked the button
               // MelonLogger.Msg("Contract Manually accepted");//debugger
            

                
            trackerflip.tracker = false; //resets
            //MelonLogger.Msg("Contract was created"); //debug line

        }

    }

    
    

}
