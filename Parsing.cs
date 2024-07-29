using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Terraria;
using Terraria.ModLoader;

namespace NovaQuest
{
    public class Parsing
    {
        /*
        Sur Windows : C:\Users\<VotreNomUtilisateur>\Documents\My Games\Terraria\ModLoader\Mods\NovaQuest\
        Sur macOS : ~/Library/Application Support/Terraria/ModLoader/Mods/NovaQuest/
        Sur Linux : ~/.local/share/Terraria/ModLoader/Mods/NovaQuest/
        */
        public static void LoadQuests(ref List<QuestDef> quests)
        {
            string homePath = Environment.GetEnvironmentVariable("HOME");
            string modSourcesDirectory = Path.Combine(homePath, "Library", "Application Support", "Terraria", "tModLoader", "ModSources", "NovaQuest");
            string filePath = Path.Combine(modSourcesDirectory, "test.json");
            LoadQuestsFromJson(filePath, ref quests);
        }
        public static List<QuestDef> LoadQuestsFromJson(string filePath, ref List<QuestDef> quests)
        {
            string json = File.ReadAllText(filePath);
            quests = JsonConvert.DeserializeObject<List<QuestDef>>(json);
            return (quests);
        }
    }
}